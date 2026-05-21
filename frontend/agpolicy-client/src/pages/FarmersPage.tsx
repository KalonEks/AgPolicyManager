import type { FormEvent } from 'react'
import { useState } from 'react'
import { createFarm, createFarmer, getFarmers, getFarms } from '../api/farmersApi'
import { EmptyState } from '../components/EmptyState'
import { Field } from '../components/Field'
import { PageHeader } from '../components/PageHeader'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'
import type { Farm, Farmer } from '../types/farmer'

interface FarmersPageProps {
  refreshKey: number
  onChanged: () => void
}

const blankFarmer = {
  firstName: '',
  lastName: '',
  email: '',
  phone: '',
  county: '',
  state: '',
}

const blankFarm = {
  farmName: '',
  acres: '',
  county: '',
  state: '',
}

export function FarmersPage({ refreshKey, onChanged }: FarmersPageProps) {
  const { data: farmers, loading, error } = useAsyncData(getFarmers, refreshKey)
  const [farmerForm, setFarmerForm] = useState(blankFarmer)
  const [farmForm, setFarmForm] = useState(blankFarm)
  const [selectedFarmerId, setSelectedFarmerId] = useState('')
  const [farms, setFarms] = useState<Farm[]>([])
  const [message, setMessage] = useState('')
  const [formError, setFormError] = useState('')

  async function submitFarmer(event: FormEvent) {
    event.preventDefault()
    setMessage('')
    setFormError('')

    try {
      const created = await createFarmer(farmerForm)
      setFarmerForm(blankFarmer)
      setSelectedFarmerId(String(created.id))
      setMessage(`Created farmer ${created.firstName} ${created.lastName}.`)
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to create farmer.')
    }
  }

  async function submitFarm(event: FormEvent) {
    event.preventDefault()
    setMessage('')
    setFormError('')

    try {
      const farmerId = Number(selectedFarmerId)
      await createFarm(farmerId, { ...farmForm, acres: Number(farmForm.acres) })
      setFarmForm(blankFarm)
      setMessage('Created farm.')
      setFarms(await getFarms(farmerId))
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to create farm.')
    }
  }

  async function loadFarms(farmerId: string) {
    setSelectedFarmerId(farmerId)
    setFarms([])

    if (!farmerId) {
      return
    }

    try {
      setFarms(await getFarms(Number(farmerId)))
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to load farms.')
    }
  }

  return (
    <section className="page-section">
      <PageHeader title="Farmers and farms" description="Create farmers, then attach farms for quote eligibility." />
      <StatusMessage message={error || formError} tone="error" />
      <StatusMessage message={message} tone="success" />

      <div className="two-column">
        <form className="panel" onSubmit={submitFarmer}>
          <h3>Create farmer</h3>
          <div className="form-grid">
            <Field label="First name">
              <input required value={farmerForm.firstName} onChange={(event) => setFarmerForm({ ...farmerForm, firstName: event.target.value })} />
            </Field>
            <Field label="Last name">
              <input required value={farmerForm.lastName} onChange={(event) => setFarmerForm({ ...farmerForm, lastName: event.target.value })} />
            </Field>
            <Field label="Email">
              <input required type="email" value={farmerForm.email} onChange={(event) => setFarmerForm({ ...farmerForm, email: event.target.value })} />
            </Field>
            <Field label="Phone">
              <input value={farmerForm.phone} onChange={(event) => setFarmerForm({ ...farmerForm, phone: event.target.value })} />
            </Field>
            <Field label="County">
              <input required value={farmerForm.county} onChange={(event) => setFarmerForm({ ...farmerForm, county: event.target.value })} />
            </Field>
            <Field label="State">
              <input required value={farmerForm.state} onChange={(event) => setFarmerForm({ ...farmerForm, state: event.target.value })} />
            </Field>
          </div>
          <button type="submit">Create farmer</button>
        </form>

        <form className="panel" onSubmit={submitFarm}>
          <h3>Create farm</h3>
          <div className="form-grid">
            <Field label="Farmer">
              <select required value={selectedFarmerId} onChange={(event) => void loadFarms(event.target.value)}>
                <option value="">Select farmer</option>
                {(farmers ?? []).map((farmer) => (
                  <option key={farmer.id} value={farmer.id}>
                    {farmerName(farmer)}
                  </option>
                ))}
              </select>
            </Field>
            <Field label="Farm name">
              <input required value={farmForm.farmName} onChange={(event) => setFarmForm({ ...farmForm, farmName: event.target.value })} />
            </Field>
            <Field label="Acres">
              <input required min="0.01" step="0.01" type="number" value={farmForm.acres} onChange={(event) => setFarmForm({ ...farmForm, acres: event.target.value })} />
            </Field>
            <Field label="County">
              <input required value={farmForm.county} onChange={(event) => setFarmForm({ ...farmForm, county: event.target.value })} />
            </Field>
            <Field label="State">
              <input required value={farmForm.state} onChange={(event) => setFarmForm({ ...farmForm, state: event.target.value })} />
            </Field>
          </div>
          <button type="submit">Create farm</button>
        </form>
      </div>

      <div className="panel">
        <h3>Farmers</h3>
        {loading && <p>Loading farmers...</p>}
        {!loading && farmers?.length === 0 && <EmptyState message="No farmers have been created yet." />}
        {farmers && farmers.length > 0 && <FarmersTable farmers={farmers} />}
      </div>

      {selectedFarmerId && (
        <div className="panel">
          <h3>Farms for selected farmer</h3>
          {farms.length === 0 ? <EmptyState message="No farms found for this farmer." /> : <FarmsTable farms={farms} />}
        </div>
      )}
    </section>
  )
}

function FarmersTable({ farmers }: { farmers: Farmer[] }) {
  return (
    <div className="table-wrap">
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>County</th>
            <th>State</th>
          </tr>
        </thead>
        <tbody>
          {farmers.map((farmer) => (
            <tr key={farmer.id}>
              <td>{farmerName(farmer)}</td>
              <td>{farmer.email}</td>
              <td>{farmer.phone || 'None'}</td>
              <td>{farmer.county}</td>
              <td>{farmer.state}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

function FarmsTable({ farms }: { farms: Farm[] }) {
  return (
    <div className="table-wrap">
      <table>
        <thead>
          <tr>
            <th>Farm</th>
            <th>Acres</th>
            <th>County</th>
            <th>State</th>
          </tr>
        </thead>
        <tbody>
          {farms.map((farm) => (
            <tr key={farm.id}>
              <td>{farm.farmName}</td>
              <td>{farm.acres}</td>
              <td>{farm.county}</td>
              <td>{farm.state}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

function farmerName(farmer: Farmer) {
  return `${farmer.firstName} ${farmer.lastName}`
}
