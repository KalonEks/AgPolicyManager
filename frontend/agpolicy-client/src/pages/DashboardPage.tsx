import { getClaims } from '../api/claimsApi'
import { getFarmers } from '../api/farmersApi'
import { getPolicies } from '../api/policiesApi'
import { getQuotes } from '../api/quotesApi'
import { EmptyState } from '../components/EmptyState'
import { PageHeader } from '../components/PageHeader'
import { StatGrid } from '../components/StatGrid'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'

interface DashboardPageProps {
  refreshKey: number
}

export function DashboardPage({ refreshKey }: DashboardPageProps) {
  const { data, loading, error } = useAsyncData(async () => {
    const [farmers, quotes, policies, claims] = await Promise.all([
      getFarmers(),
      getQuotes(),
      getPolicies(),
      getClaims(),
    ])

    return { farmers, quotes, policies, claims }
  }, refreshKey)

  const totalPremium = data?.policies.reduce((sum, policy) => sum + policy.premium, 0) ?? 0

  return (
    <section className="page-section">
      <PageHeader
        title="Dashboard"
        description="Operational snapshot for farmers, quotes, policies, and claims."
      />
      <StatusMessage message={error} tone="error" />
      {loading && <p>Loading dashboard...</p>}
      {data && (
        <>
          <StatGrid
            items={[
              { label: 'Farmers', value: data.farmers.length },
              { label: 'Quotes', value: data.quotes.length },
              { label: 'Active policies', value: data.policies.filter((policy) => policy.status === 'Active').length },
              { label: 'Total premium', value: currency(totalPremium) },
              { label: 'Open claims', value: data.claims.filter((claim) => claim.status === 'Open').length },
            ]}
          />
          {data.quotes.length === 0 && <EmptyState message="Create a farmer, farm, and quote to start the workflow." />}
        </>
      )}
    </section>
  )
}

function currency(value: number) {
  return value.toLocaleString(undefined, { style: 'currency', currency: 'USD' })
}
