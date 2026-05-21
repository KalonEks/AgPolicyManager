interface StatusMessageProps {
  message?: string
  tone?: 'success' | 'error' | 'info'
}

export function StatusMessage({ message, tone = 'info' }: StatusMessageProps) {
  if (!message) {
    return null
  }

  return <p className={`status-message ${tone}`}>{message}</p>
}
