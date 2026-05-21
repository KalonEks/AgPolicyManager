import { useMemo, useState } from 'react'
import { ClaimsPage } from './pages/ClaimsPage'
import { DashboardPage } from './pages/DashboardPage'
import { FarmersPage } from './pages/FarmersPage'
import { PoliciesPage } from './pages/PoliciesPage'
import { QuotesPage } from './pages/QuotesPage'

type PageId = 'dashboard' | 'farmers' | 'quotes' | 'policies' | 'claims'

const navItems: Array<{ id: PageId; label: string }> = [
  { id: 'dashboard', label: 'Dashboard' },
  { id: 'farmers', label: 'Farmers' },
  { id: 'quotes', label: 'Quotes' },
  { id: 'policies', label: 'Policies' },
  { id: 'claims', label: 'Claims' },
]

function App() {
  const [activePage, setActivePage] = useState<PageId>('dashboard')
  const [refreshKey, setRefreshKey] = useState(0)

  const page = useMemo(() => {
    const onChanged = () => setRefreshKey((key) => key + 1)

    switch (activePage) {
      case 'farmers':
        return <FarmersPage refreshKey={refreshKey} onChanged={onChanged} />
      case 'quotes':
        return <QuotesPage refreshKey={refreshKey} onChanged={onChanged} />
      case 'policies':
        return <PoliciesPage refreshKey={refreshKey} />
      case 'claims':
        return <ClaimsPage refreshKey={refreshKey} onChanged={onChanged} />
      default:
        return <DashboardPage refreshKey={refreshKey} />
    }
  }, [activePage, refreshKey])

  return (
    <div className="app-shell">
      <header className="topbar">
        <div>
          <p className="eyebrow">AgPolicy Manager</p>
          <h1>Crop insurance operations</h1>
        </div>
        <nav aria-label="Primary navigation">
          {navItems.map((item) => (
            <button
              key={item.id}
              className={activePage === item.id ? 'active' : ''}
              type="button"
              onClick={() => setActivePage(item.id)}
            >
              {item.label}
            </button>
          ))}
        </nav>
      </header>
      <main>{page}</main>
    </div>
  )
}

export default App
