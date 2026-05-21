import { getPolicies } from '../api/policiesApi'
import { EmptyState } from '../components/EmptyState'
import { PageHeader } from '../components/PageHeader'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'

interface PoliciesPageProps {
  refreshKey: number
}

export function PoliciesPage({ refreshKey }: PoliciesPageProps) {
  const { data: policies, loading, error } = useAsyncData(getPolicies, refreshKey)

  return (
    <section className="page-section">
      <PageHeader title="Policies" description="Active policies created from converted quotes." />
      <StatusMessage message={error} tone="error" />
      <div className="panel">
        {loading && <p>Loading policies...</p>}
        {!loading && policies?.length === 0 && <EmptyState message="No policies have been created yet." />}
        {policies && policies.length > 0 && (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Policy</th>
                  <th>Quote</th>
                  <th>Farmer</th>
                  <th>Farm</th>
                  <th>Crop</th>
                  <th>Acres</th>
                  <th>Premium</th>
                  <th>Status</th>
                  <th>Effective</th>
                  <th>Expires</th>
                </tr>
              </thead>
              <tbody>
                {policies.map((policy) => (
                  <tr key={policy.id}>
                    <td>{policy.id}</td>
                    <td>{policy.quoteId}</td>
                    <td>{policy.farmerName || `Farmer ${policy.farmerId}`}</td>
                    <td>{policy.farmName || `Farm ${policy.farmId}`}</td>
                    <td>{policy.cropType}</td>
                    <td>{policy.insuredAcres}</td>
                    <td>{currency(policy.premium)}</td>
                    <td><span className="badge">{policy.status}</span></td>
                    <td>{date(policy.effectiveDate)}</td>
                    <td>{date(policy.expirationDate)}</td>
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

function date(value: string) {
  return new Date(value).toLocaleDateString()
}

function currency(value: number) {
  return value.toLocaleString(undefined, { style: 'currency', currency: 'USD' })
}
