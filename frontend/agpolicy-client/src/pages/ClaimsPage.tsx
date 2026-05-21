import type { FormEvent } from 'react'
import { useState } from 'react'
import { createClaim, getClaims, updateClaimStatus } from '../api/claimsApi'
import { getPolicies } from '../api/policiesApi'
import { EmptyState } from '../components/EmptyState'
import { Field } from '../components/Field'
import { PageHeader } from '../components/PageHeader'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'

interface ClaimsPageProps {
  refreshKey: number
  onChanged: () => void
}

const claimStatuses = ['Open', 'InReview', 'Approved', 'Denied', 'Closed']

const blankClaim = {
  policyId: '',
  lossDate: '',
  lossReason: '',
  estimatedLossAmount: '',
  notes: '',
}

export function ClaimsPage({ refreshKey, onChanged }: ClaimsPageProps) {
  const { data: claims, loading, error } = useAsyncData(getClaims, refreshKey)
  const { data: policies } = useAsyncData(getPolicies, refreshKey)
  const [form, setForm] = useState(blankClaim)
  const [message, setMessage] = useState('')
  const [formError, setFormError] = useState('')

  async function submitClaim(event: FormEvent) {
    event.preventDefault()
    setMessage('')
    setFormError('')

    try {
      const created = await createClaim({
        policyId: Number(form.policyId),
        lossDate: form.lossDate,
        lossReason: form.lossReason,
        estimatedLossAmount: Number(form.estimatedLossAmount),
        notes: form.notes,
      })
      setForm(blankClaim)
      setMessage(`Claim ${created.id} opened.`)
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to create claim.')
    }
  }

  async function setStatus(id: number, status: string) {
    setMessage('')
    setFormError('')

    try {
      const updated = await updateClaimStatus(id, status)
      setMessage(`Claim ${updated.id} moved to ${updated.status}.`)
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to update claim status.')
    }
  }

  return (
    <section className="page-section">
      <PageHeader title="Claims" description="Open claims against active policies and manage claim status." />
      <StatusMessage message={error || formError} tone="error" />
      <StatusMessage message={message} tone="success" />

      <form className="panel" onSubmit={submitClaim}>
        <h3>Create claim</h3>
        <div className="form-grid wide">
          <Field label="Policy">
            <select required value={form.policyId} onChange={(event) => setForm({ ...form, policyId: event.target.value })}>
              <option value="">Select policy</option>
              {(policies ?? []).map((policy) => (
                <option key={policy.id} value={policy.id}>
                  Policy {policy.id} - {policy.cropType} - {currency(policy.premium)}
                </option>
              ))}
            </select>
          </Field>
          <Field label="Loss date">
            <input required type="date" value={form.lossDate} onChange={(event) => setForm({ ...form, lossDate: event.target.value })} />
          </Field>
          <Field label="Estimated loss">
            <input required min="0" step="0.01" type="number" value={form.estimatedLossAmount} onChange={(event) => setForm({ ...form, estimatedLossAmount: event.target.value })} />
          </Field>
          <Field label="Loss reason">
            <input required value={form.lossReason} onChange={(event) => setForm({ ...form, lossReason: event.target.value })} />
          </Field>
          <Field label="Notes">
            <input value={form.notes} onChange={(event) => setForm({ ...form, notes: event.target.value })} />
          </Field>
        </div>
        <button type="submit">Open claim</button>
      </form>

      <div className="panel">
        <h3>Claims</h3>
        {loading && <p>Loading claims...</p>}
        {!loading && claims?.length === 0 && <EmptyState message="No claims have been opened yet." />}
        {claims && claims.length > 0 && (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Claim</th>
                  <th>Policy</th>
                  <th>Loss date</th>
                  <th>Reason</th>
                  <th>Estimated loss</th>
                  <th>Status</th>
                  <th>Update</th>
                </tr>
              </thead>
              <tbody>
                {claims.map((claim) => (
                  <tr key={claim.id}>
                    <td>{claim.id}</td>
                    <td>{claim.policyId}</td>
                    <td>{new Date(claim.lossDate).toLocaleDateString()}</td>
                    <td>{claim.lossReason}</td>
                    <td>{currency(claim.estimatedLossAmount)}</td>
                    <td><span className="badge">{claim.status}</span></td>
                    <td>
                      <select value={claim.status} onChange={(event) => void setStatus(claim.id, event.target.value)}>
                        {claimStatuses.map((status) => (
                          <option key={status} value={status}>
                            {status}
                          </option>
                        ))}
                      </select>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </section>
  )
}

function currency(value: number) {
  return value.toLocaleString(undefined, { style: 'currency', currency: 'USD' })
}
