import type { FormEvent } from 'react'
import { useState } from 'react'
import { getFarmers, getFarms } from '../api/farmersApi'
import { convertQuote, createQuote, getQuotes } from '../api/quotesApi'
import { EmptyState } from '../components/EmptyState'
import { Field } from '../components/Field'
import { PageHeader } from '../components/PageHeader'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'
import type { Farm } from '../types/farmer'
import type { Quote } from '../types/quote'

interface QuotesPageProps {
  refreshKey: number
  onChanged: () => void
}

const cropTypes = ['Corn', 'Soybeans', 'Wheat', 'Cotton']
const coverageLevels = [50, 65, 75, 85]

const blankQuote = {
  farmerId: '',
  farmId: '',
  cropType: 'Corn',
  coverageLevel: '75',
}

export function QuotesPage({ refreshKey, onChanged }: QuotesPageProps) {
  const { data: quotes, loading, error } = useAsyncData(getQuotes, refreshKey)
  const { data: farmers } = useAsyncData(getFarmers, refreshKey)
  const [farms, setFarms] = useState<Farm[]>([])
  const [form, setForm] = useState(blankQuote)
  const [selectedQuote, setSelectedQuote] = useState<Quote | null>(null)
  const [message, setMessage] = useState('')
  const [formError, setFormError] = useState('')
  const selectedFarm = farms.find((farm) => String(farm.id) === form.farmId)

  async function farmerChanged(farmerId: string) {
    setForm({ ...form, farmerId, farmId: '' })
    setSelectedQuote(null)
    setFarms([])

    if (!farmerId) {
      return
    }

    try {
      setFarms(await getFarms(Number(farmerId)))
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to load farms.')
    }
  }

  async function submitQuote(event: FormEvent) {
    event.preventDefault()
    setMessage('')
    setFormError('')

    try {
      const created = await createQuote({
        farmerId: Number(form.farmerId),
        farmId: Number(form.farmId),
        cropType: form.cropType,
        coverageLevel: Number(form.coverageLevel),
      })
      setSelectedQuote(created)
      setForm({ ...blankQuote, farmerId: form.farmerId })
      setMessage(`Quote ${created.id} created with estimated premium ${currency(created.estimatedPremium)}.`)
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to create quote.')
    }
  }

  async function convertSelectedQuote() {
    if (!selectedQuote) {
      return
    }

    setMessage('')
    setFormError('')

    try {
      const result = await convertQuote(selectedQuote.id)
      setMessage(`${result.message} Policy ${result.policyId} is now active.`)
      setSelectedQuote({ ...selectedQuote, status: 'Converted', convertedPolicyId: result.policyId })
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to convert quote.')
    }
  }

  return (
    <section className="page-section">
      <PageHeader title="Quotes" description="Calculate premiums and convert accepted quotes into active policies." />
      <StatusMessage message={error || formError} tone="error" />
      <StatusMessage message={message} tone="success" />

      <div className="two-column">
        <form className="panel" onSubmit={submitQuote}>
          <h3>Create quote</h3>
          <div className="form-grid">
            <Field label="Farmer">
              <select required value={form.farmerId} onChange={(event) => void farmerChanged(event.target.value)}>
                <option value="">Select farmer</option>
                {(farmers ?? []).map((farmer) => (
                  <option key={farmer.id} value={farmer.id}>
                    {farmer.firstName} {farmer.lastName}
                  </option>
                ))}
              </select>
            </Field>
            <Field label="Farm">
              <select required value={form.farmId} onChange={(event) => setForm({ ...form, farmId: event.target.value })}>
                <option value="">Select farm</option>
                {farms.map((farm) => (
                  <option key={farm.id} value={farm.id}>
                    {farm.farmName} ({farm.acres} acres)
                  </option>
                ))}
              </select>
            </Field>
            <div className="readonly-field">
              <span>Quoted acres</span>
              <strong>{selectedFarm ? selectedFarm.acres : 'Select a farm'}</strong>
            </div>
            <Field label="Crop type">
              <select value={form.cropType} onChange={(event) => setForm({ ...form, cropType: event.target.value })}>
                {cropTypes.map((cropType) => (
                  <option key={cropType} value={cropType}>
                    {cropType}
                  </option>
                ))}
              </select>
            </Field>
            <Field label="Coverage level">
              <select value={form.coverageLevel} onChange={(event) => setForm({ ...form, coverageLevel: event.target.value })}>
                {coverageLevels.map((level) => (
                  <option key={level} value={level}>
                    {level}%
                  </option>
                ))}
              </select>
            </Field>
          </div>
          <button type="submit">Create quote</button>
        </form>

        <div className="panel">
          <h3>Quote details</h3>
          {!selectedQuote && <EmptyState message="Select a quote from the table or create a new one." />}
          {selectedQuote && (
            <div className="detail-list">
              <p><span>Quote</span><strong>{selectedQuote.id}</strong></p>
              <p><span>Crop</span><strong>{selectedQuote.cropType}</strong></p>
              <p><span>Acres</span><strong>{selectedQuote.acres}</strong></p>
              <p><span>Coverage</span><strong>{selectedQuote.coverageLevel}%</strong></p>
              <p><span>Premium</span><strong>{currency(selectedQuote.estimatedPremium)}</strong></p>
              <p><span>Status</span><strong>{selectedQuote.status}</strong></p>
              <button type="button" disabled={selectedQuote.status === 'Converted'} onClick={() => void convertSelectedQuote()}>
                Convert to policy
              </button>
            </div>
          )}
        </div>
      </div>

      <div className="panel">
        <h3>Quote pipeline</h3>
        {loading && <p>Loading quotes...</p>}
        {!loading && quotes?.length === 0 && <EmptyState message="No quotes have been created yet." />}
        {quotes && quotes.length > 0 && (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Quote</th>
                  <th>Farmer</th>
                  <th>Farm</th>
                  <th>Crop</th>
                  <th>Acres</th>
                  <th>Coverage</th>
                  <th>Premium</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>
                {quotes.map((quote) => (
                  <tr key={quote.id} className={selectedQuote?.id === quote.id ? 'selected-row' : ''} onClick={() => setSelectedQuote(quote)}>
                    <td>{quote.id}</td>
                    <td>{quote.farmerName || `Farmer ${quote.farmerId}`}</td>
                    <td>{quote.farmName || `Farm ${quote.farmId}`}</td>
                    <td>{quote.cropType}</td>
                    <td>{quote.acres}</td>
                    <td>{quote.coverageLevel}%</td>
                    <td>{currency(quote.estimatedPremium)}</td>
                    <td><span className="badge">{quote.status}</span></td>
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
