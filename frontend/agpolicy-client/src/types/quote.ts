export interface Quote {
  id: number
  farmerId: number
  farmerName: string
  farmId: number
  farmName: string
  cropType: string
  acres: number
  coverageLevel: number
  estimatedPremium: number
  status: string
  createdAt: string
  convertedPolicyId?: number | null
}

export interface CreateQuoteRequest {
  farmerId: number
  farmId: number
  cropType: string
  acres?: number
  coverageLevel: number
}

export interface ConvertQuoteResponse {
  quoteId: number
  policyId: number
  message: string
}
