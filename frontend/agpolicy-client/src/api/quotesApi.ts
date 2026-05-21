import { apiRequest } from './http'
import type { ConvertQuoteResponse, CreateQuoteRequest, Quote } from '../types/quote'

export function getQuotes(): Promise<Quote[]> {
  return apiRequest<Quote[]>('/quotes')
}

export function getQuote(id: number): Promise<Quote> {
  return apiRequest<Quote>(`/quotes/${id}`)
}

export function createQuote(request: CreateQuoteRequest): Promise<Quote> {
  return apiRequest<Quote>('/quotes', {
    method: 'POST',
    body: JSON.stringify(request),
  })
}

export function convertQuote(id: number): Promise<ConvertQuoteResponse> {
  return apiRequest<ConvertQuoteResponse>(`/quotes/${id}/convert-to-policy`, {
    method: 'POST',
  })
}
