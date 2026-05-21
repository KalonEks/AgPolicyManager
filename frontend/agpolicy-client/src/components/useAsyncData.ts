import { useEffect, useRef, useState } from 'react'

export function useAsyncData<T>(loader: () => Promise<T>, reloadKey: unknown) {
  const [data, setData] = useState<T | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')
  const loaderRef = useRef(loader)

  useEffect(() => {
    loaderRef.current = loader
  }, [loader])

  useEffect(() => {
    let ignore = false

    async function load() {
      setLoading(true)
      setError('')

      try {
        const result = await loaderRef.current()
        if (!ignore) {
          setData(result)
        }
      } catch (err) {
        if (!ignore) {
          setError(err instanceof Error ? err.message : 'Unable to load data.')
        }
      } finally {
        if (!ignore) {
          setLoading(false)
        }
      }
    }

    void load()

    return () => {
      ignore = true
    }
  }, [reloadKey])

  return { data, loading, error }
}
