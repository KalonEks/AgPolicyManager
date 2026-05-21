import { apiRequest } from './http'
import type { Policy } from '../types/policy'

export function getPolicies(): Promise<Policy[]> {
  return apiRequest<Policy[]>('/policies')
}
