# AgPolicy Manager

A crop-insurance-style full-stack practice app built with ASP.NET Core, C#, EF Core, SQLite, React, and TypeScript.

## Features

- Manage farmers
- Manage farms by farmer
- Create crop insurance quotes
- Calculate estimated premiums
- Convert quotes into active policies
- View policies
- Open claims and update claim statuses
- View dashboard totals for farmers, quotes, policies, premium, and open claims

## Tech Stack

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQLite
- React
- TypeScript
- Vite

## Main Workflow

1. Create a farmer.
2. Create a farm for the farmer.
3. Create a quote for the farmer and farm.
4. Review the estimated premium.
5. Convert the quote into an active policy.

The core backend workflow is intentionally implemented in services rather than controllers so validation, premium calculation, and quote-to-policy state transitions stay centralized and testable.

## Premium Calculation

`EstimatedPremium = Acres * BaseRate * CoverageMultiplier`

Crop base rates:

- Corn: `18.50`
- Soybeans: `14.25`
- Wheat: `11.75`
- Cotton: `20.00`

Coverage multipliers:

- 50: `0.75`
- 65: `1.00`
- 75: `1.20`
- 85: `1.45`

## Running the Backend

```bash
cd backend/AgPolicy.Api
dotnet restore
dotnet ef database update
dotnet run
```

Swagger UI is available at the backend URL printed by `dotnet run`, followed by `/swagger`.

## Running the Frontend

```bash
cd frontend/agpolicy-client
npm install
npm run dev
```

The frontend defaults to `http://localhost:5056/api`. Override it with `VITE_API_BASE_URL` if your backend runs on a different port.

## Portable Setup Scripts

From the project root on Windows:

```powershell
powershell -ExecutionPolicy Bypass -File .\setup.ps1
powershell -ExecutionPolicy Bypass -File .\run-dev.ps1
```

From macOS/Linux/Git Bash/WSL:

```bash
chmod +x setup.sh run-dev.sh
./setup.sh
./run-dev.sh
```

Optional setup flags:

- Windows: `-SkipDb`, `-BackendOnly`, `-FrontendOnly`
- Bash: `--skip-db`, `--backend-only`, `--frontend-only`

## API Endpoints

- `GET /api/farmers`
- `GET /api/farmers/{id}`
- `POST /api/farmers`
- `PUT /api/farmers/{id}`
- `DELETE /api/farmers/{id}`
- `GET /api/farmers/{farmerId}/farms`
- `POST /api/farmers/{farmerId}/farms`
- `GET /api/farms/{id}`
- `GET /api/quotes`
- `GET /api/quotes/{id}`
- `POST /api/quotes`
- `POST /api/quotes/{id}/convert-to-policy`
- `GET /api/policies`
- `GET /api/policies/{id}`
- `GET /api/farmers/{farmerId}/policies`
- `GET /api/claims`
- `GET /api/claims/{id}`
- `POST /api/claims`
- `PUT /api/claims/{id}/status`

## Interview Notes

This app demonstrates REST API design, relational modeling with EF Core, SQLite migrations, validation, reusable DTO mapping, workflow state transitions, and a React TypeScript client that calls the backend through modular API clients.
