import { apiRequest } from './http'
import type { CreateFarmRequest, CreateFarmerRequest, Farm, Farmer } from '../types/farmer'
import type { Policy } from '../types/policy'

export function getFarmers(): Promise<Farmer[]> {
  return apiRequest<Farmer[]>('/farmers')
}

export function createFarmer(request: CreateFarmerRequest): Promise<Farmer> {
  return apiRequest<Farmer>('/farmers', {
    method: 'POST',
    body: JSON.stringify(request),
  })
}

export function getFarms(farmerId: number): Promise<Farm[]> {
  return apiRequest<Farm[]>(`/farmers/${farmerId}/farms`)
}

export function createFarm(farmerId: number, request: CreateFarmRequest): Promise<Farm> {
  return apiRequest<Farm>(`/farmers/${farmerId}/farms`, {
    method: 'POST',
    body: JSON.stringify(request),
  })
}

export function getFarmerPolicies(farmerId: number): Promise<Policy[]> {
  return apiRequest<Policy[]>(`/farmers/${farmerId}/policies`)
}
