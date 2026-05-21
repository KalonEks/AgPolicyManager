import { apiRequest } from './http'
import type { Claim, CreateClaimRequest } from '../types/claim'

export function getClaims(): Promise<Claim[]> {
  return apiRequest<Claim[]>('/claims')
}

export function createClaim(request: CreateClaimRequest): Promise<Claim> {
  return apiRequest<Claim>('/claims', {
    method: 'POST',
    body: JSON.stringify(request),
  })
}

export function updateClaimStatus(id: number, status: string): Promise<Claim> {
  return apiRequest<Claim>(`/claims/${id}/status`, {
    method: 'PUT',
    body: JSON.stringify({ status }),
  })
}
