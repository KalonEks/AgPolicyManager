export interface Claim {
  id: number
  policyId: number
  lossDate: string
  lossReason: string
  estimatedLossAmount: number
  status: string
  notes?: string | null
}

export interface CreateClaimRequest {
  policyId: number
  lossDate: string
  lossReason: string
  estimatedLossAmount: number
  notes?: string
}
