interface StatItem {
  label: string
  value: string | number
}

interface StatGridProps {
  items: StatItem[]
}

export function StatGrid({ items }: StatGridProps) {
  return (
    <div className="stat-grid">
      {items.map((item) => (
        <div className="stat-card" key={item.label}>
          <span>{item.label}</span>
          <strong>{item.value}</strong>
        </div>
      ))}
    </div>
  )
}
