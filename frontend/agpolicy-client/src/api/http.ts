const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5056/api'

interface ApiErrorResponse {
  message?: string
}

export async function apiRequest<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...init,
    headers: {
      'Content-Type': 'application/json',
      ...init?.headers,
    },
  })

  if (!response.ok) {
    let message = `Request failed with ${response.status}`

    try {
      const error = (await response.json()) as ApiErrorResponse
      message = error.message || message
    } catch {
      message = response.statusText || message
    }

    throw new Error(message)
  }

  if (response.status === 204) {
    return undefined as T
  }

  return response.json() as Promise<T>
}
