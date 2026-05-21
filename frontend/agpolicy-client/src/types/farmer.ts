export interface Farmer {
  id: number
  firstName: string
  lastName: string
  email: string
  phone?: string | null
  county: string
  state: string
}

export interface CreateFarmerRequest {
  firstName: string
  lastName: string
  email: string
  phone?: string
  county: string
  state: string
}

export interface Farm {
  id: number
  farmerId: number
  farmName: string
  acres: number
  county: string
  state: string
}

export interface CreateFarmRequest {
  farmName: string
  acres: number
  county: string
  state: string
}
