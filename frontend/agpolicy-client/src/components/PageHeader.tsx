interface PageHeaderProps {
  title: string
  description: string
}

export function PageHeader({ title, description }: PageHeaderProps) {
  return (
    <div className="page-header">
      <h2>{title}</h2>
      <p>{description}</p>
    </div>
  )
}
