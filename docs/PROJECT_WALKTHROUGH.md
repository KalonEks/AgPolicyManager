# AgPolicy Manager Project Walkthrough

This document explains the AgPolicy Manager codebase for someone who has coding experience, especially with PHP and Vue, but is new to .NET, C#, and React.

The short version:

- The backend is an ASP.NET Core Web API written in C#.
- The database layer uses Entity Framework Core, which is similar in purpose to Laravel Eloquent, Doctrine, or another ORM.
- The database is SQLite for local development.
- The frontend is a React app written in TypeScript.
- Vite is the frontend dev/build tool, similar to the tooling you may have used around Vue projects.

## Mental Model

If you come from PHP and Vue, think about the app like this:

- ASP.NET Core is playing the role of a PHP backend framework such as Laravel, Symfony, or Slim.
- Controllers are similar to Laravel controllers or route handlers.
- Services are plain C# classes where business logic lives. In a Laravel app, these might be service classes under `app/Services`.
- Entity Framework Core is the ORM. It maps C# classes to database tables, similar to Eloquent models mapping PHP classes to tables.
- DTOs are request/response objects. They are similar to using Laravel Form Requests, API Resources, or explicit arrays for API contracts.
- React components are similar in purpose to Vue components, but React uses JSX/TSX instead of Vue single-file component templates.
- React state with `useState` is similar to Vue reactive state.
- React `useEffect` is often used for lifecycle-like work, similar to Vue `onMounted` or watchers.
- TypeScript interfaces are similar to documenting expected object shapes, but enforced at compile time.

## High-Level Architecture

The backend follows this flow:

```text
HTTP request
  -> Controller
  -> Service
  -> DbContext
  -> SQLite database
```

The frontend follows this flow:

```text
React page
  -> API client function
  -> fetch()
  -> ASP.NET Core API
  -> JSON response
  -> React state
  -> rendered UI
```

The most important business workflow is:

```text
Farmer
  -> Farm
  -> Quote
  -> Convert quote to active policy
  -> Optional claim against policy
```

## Root Folder: `AgPolicyManager/`

The root folder coordinates the full-stack app. It contains the backend, frontend, setup scripts, documentation, and Git configuration.

- **Folder: `backend/`**
  - Contains the ASP.NET Core backend.
  - Tech used:
    - C#
    - ASP.NET Core Web API
    - Entity Framework Core
    - SQLite
    - Swagger/OpenAPI
  - PHP comparison:
    - This is like the Laravel/Symfony/API portion of a PHP project.
    - It owns routes, controllers, database models, validation, and business rules.

- **Folder: `frontend/`**
  - Contains the React frontend.
  - Tech used:
    - React
    - TypeScript
    - Vite
    - Fetch API
    - CSS
  - Vue comparison:
    - This is like a Vue SPA created with Vite.
    - Instead of `.vue` files, React uses `.tsx` files that combine JavaScript/TypeScript logic and JSX markup.

- **Folder: `docs/`**
  - Contains project documentation written for humans.
  - This folder is not used by the app at runtime.

- **Folder: `.git/`**
  - Git internal metadata.
  - You normally do not edit this manually.
  - It tracks commits, branches, object history, and repository state.

- **File: `.gitignore`**
  - Tells Git which files/folders not to track.
  - Important ignored items:
    - `bin/` and `obj/`: .NET build outputs.
    - `node_modules/`: npm dependencies.
    - `dist/`: frontend production build output.
    - `*.db`: local SQLite database files.
    - `*.log`: local dev logs.
  - PHP/Vue comparison:
    - Similar to ignoring `vendor/`, `node_modules/`, `.env`, logs, cache files, and compiled assets.

- **File: `AgPolicyManager.sln`**
  - A .NET solution file.
  - It groups .NET projects together for Visual Studio and `dotnet` tooling.
  - This app currently has one backend project.
  - PHP comparison:
    - PHP does not usually have an exact equivalent.
    - Think of it as an IDE/build-tool workspace file for .NET projects.

- **File: `README.md`**
  - Main project readme.
  - Explains features, tech stack, running instructions, API endpoints, and interview notes.

- **File: `setup.ps1`**
  - Windows PowerShell setup script.
  - Checks for .NET, Node, and npm.
  - Installs/updates `dotnet-ef`.
  - Restores backend NuGet packages.
  - Builds the backend.
  - Applies database migrations unless skipped.
  - Installs frontend npm packages.
  - PHP/Vue comparison:
    - Similar to a script that runs `composer install`, `php artisan migrate`, and `npm install`.

- **File: `setup.sh`**
  - Bash version of the setup script for macOS/Linux/Git Bash/WSL.
  - Same purpose as `setup.ps1`.

- **File: `run-dev.ps1`**
  - Windows script to start the backend and frontend dev servers.
  - Opens separate PowerShell windows.

- **File: `run-dev.sh`**
  - Bash script to start backend and frontend together.
  - Runs both processes in parallel and stops them together.

## Folder: `backend/`

This folder holds backend projects. In this app, it contains one ASP.NET Core API project.

- **Folder: `AgPolicy.Api/`**
  - The actual backend application.
  - This compiles into a .NET web server process.
  - It listens on a local port, accepts HTTP requests, talks to SQLite, and returns JSON.

## Folder: `backend/AgPolicy.Api/`

This is the backend project root. The `.csproj` file defines the project and dependencies. `Program.cs` is the startup file.

- **Folder: `Controllers/`**
  - Controllers define the HTTP API surface.
  - They are responsible for:
    - Receiving HTTP requests.
    - Reading route/body values.
    - Calling services.
    - Returning HTTP responses.
  - They should not contain heavy business logic.
  - PHP comparison:
    - Similar to Laravel controllers.
    - A method like `CreateQuote()` is conceptually similar to a Laravel controller action that receives a `Request`, calls a service/model, and returns JSON.
  - .NET detail:
    - Attributes like `[HttpGet]`, `[HttpPost]`, and `[Route("api/quotes")]` define routing.
    - `ActionResult<T>` means the method can return both data and HTTP status results.

  - **File: `ApiControllerBase.cs`**
    - Shared base class for all controllers.
    - Converts service results into HTTP responses.
    - Example mappings:
      - `Success` -> `200 OK`
      - `Invalid` -> `400 Bad Request`
      - `NotFound` -> `404 Not Found`
      - `Conflict` -> `409 Conflict`
    - Why it exists:
      - Avoids repeating the same response-switching code in every controller.
      - Keeps controllers consistent.

  - **File: `ClaimsController.cs`**
    - Defines claim endpoints.
    - Handles:
      - `GET /api/claims`
      - `GET /api/claims/{id}`
      - `POST /api/claims`
      - `PUT /api/claims/{id}/status`
    - Calls `ClaimService`.
    - Does not directly use EF Core or SQL.

  - **File: `FarmersController.cs`**
    - Defines farmer-related endpoints.
    - Handles:
      - Farmer CRUD.
      - Nested farm routes for a farmer.
      - Nested policy routes for a farmer.
    - Important route examples:
      - `GET /api/farmers`
      - `POST /api/farmers`
      - `POST /api/farmers/{farmerId}/farms`
      - `GET /api/farmers/{farmerId}/policies`
    - Vue/PHP comparison:
      - Similar to a controller that groups related REST endpoints for a resource.

  - **File: `FarmsController.cs`**
    - Handles direct farm lookup by id:
      - `GET /api/farms/{id}`
    - Farm creation is nested under farmers because a farm must belong to a farmer.

  - **File: `PoliciesController.cs`**
    - Handles policy list/detail endpoints:
      - `GET /api/policies`
      - `GET /api/policies/{id}`
    - Policies are not directly created through this controller.
    - A policy is created by converting a quote.

  - **File: `QuotesController.cs`**
    - Handles quote endpoints:
      - `GET /api/quotes`
      - `GET /api/quotes/{id}`
      - `POST /api/quotes`
      - `POST /api/quotes/{id}/convert-to-policy`
    - This controller exposes the main business workflow.

- **Folder: `Data/`**
  - Database infrastructure.
  - Contains EF Core `DbContext` and seed data.
  - PHP comparison:
    - Similar to where you might configure Eloquent models, migrations, seeders, or Doctrine entity manager behavior.
  - EF Core detail:
    - `DbContext` is the main object EF uses to query and save data.
    - `DbSet<T>` represents a table-like collection.

  - **File: `AgPolicyDbContext.cs`**
    - Defines the database context.
    - Contains:
      - `DbSet<Farmer> Farmers`
      - `DbSet<Farm> Farms`
      - `DbSet<Quote> Quotes`
      - `DbSet<CropPolicy> Policies`
      - `DbSet<Claim> Claims`
    - Configures relationships:
      - Farmer has many farms.
      - Farmer has many quotes.
      - Farmer has many policies.
      - Farm has many quotes.
      - Farm has many policies.
      - Policy has many claims.
      - Policy has one quote.
    - Why relationships matter:
      - EF Core uses them to create foreign keys and understand object navigation.
      - This is like telling an ORM that `Farmer hasMany Farm` and `Farm belongsTo Farmer`.

  - **File: `SeedData.cs`**
    - Runs at backend startup.
    - Applies migrations with `Database.MigrateAsync()`.
    - Checks whether any farmers already exist.
    - If the database is empty, creates default demo data:
      - Farmer
      - Farm
      - Converted quote
      - Active policy
      - Open claim
    - Why it is idempotent:
      - It avoids duplicating sample data every time the server starts.

- **Folder: `DTOs/`**
  - DTO means Data Transfer Object.
  - These classes define the JSON shapes used by API requests and responses.
  - Why not just return database models directly?
    - Avoids exposing internal database structure.
    - Gives stable API contracts.
    - Prevents accidental over-posting or leaking navigation properties.
  - PHP comparison:
    - Similar to Laravel API Resources, Form Requests, or explicit validated request arrays.
  - TypeScript comparison:
    - These backend DTOs correspond to frontend interfaces in `src/types`.

  - **File: `ApiErrorResponse.cs`**
    - Standard error response object.
    - Has a `Message` property.
    - Allows frontend code to consistently read backend errors.

  - **File: `ClaimDtos.cs`**
    - Contains claim request and response shapes.
    - Includes:
      - `CreateClaimRequest`
      - `UpdateClaimStatusRequest`
      - `ClaimResponse`

  - **File: `FarmDtos.cs`**
    - Contains farm request and response shapes.
    - Includes:
      - `CreateFarmRequest`
      - `FarmResponse`

  - **File: `FarmerDtos.cs`**
    - Contains farmer request and response shapes.
    - Includes:
      - `CreateFarmerRequest`
      - `UpdateFarmerRequest`
      - `FarmerResponse`

  - **File: `PolicyDtos.cs`**
    - Contains `PolicyResponse`.
    - There is no create-policy request because policies are created through quote conversion.

  - **File: `QuoteDtos.cs`**
    - Contains quote request and response shapes.
    - Includes:
      - `CreateQuoteRequest`
      - `QuoteResponse`
      - `ConvertQuoteResponse`

- **Folder: `Migrations/`**
  - EF Core database migration files.
  - Migrations version the database schema.
  - PHP comparison:
    - Very similar to Laravel migrations.
  - How it works:
    - You change models or DbContext configuration.
    - You run `dotnet ef migrations add SomeName`.
    - EF generates migration code.
    - You run `dotnet ef database update`.
    - EF applies schema changes to SQLite.

  - **File: `20260430203243_InitialCreate.cs`**
    - Main migration file.
    - Contains code to create tables, columns, indexes, and foreign keys.
    - Similar to a Laravel migration with `Schema::create(...)`.

  - **File: `20260430203243_InitialCreate.Designer.cs`**
    - EF Core-generated metadata for the migration.
    - You generally do not edit this manually.

  - **File: `AgPolicyDbContextModelSnapshot.cs`**
    - EF Core’s snapshot of the current model.
    - Used to determine what changed when generating the next migration.
    - Usually not manually edited.

- **Folder: `Models/`**
  - Database entity classes.
  - These classes map to database tables through EF Core.
  - PHP comparison:
    - Similar to Eloquent model classes.
  - C# detail:
    - Properties like `public int Id { get; set; }` define fields.
    - Nullable reference types use `?`, such as `string? Phone`.
    - `List<Farm>` navigation properties represent relationships.

  - **File: `Claim.cs`**
    - Entity for claims.
    - Belongs to a policy.
    - Stores:
      - Policy id
      - Loss date
      - Loss reason
      - Estimated loss amount
      - Status
      - Notes

  - **File: `CropPolicy.cs`**
    - Entity for active crop insurance policies.
    - Created when a quote is converted.
    - Stores:
      - Farmer/farm/quote ids
      - Crop type
      - Coverage level
      - Insured acres
      - Premium
      - Status
      - Effective/expiration dates
      - Related claims

  - **File: `DomainOptions.cs`**
    - Central place for business constants.
    - Contains:
      - Crop base rates.
      - Coverage multipliers.
      - Valid quote statuses.
      - Valid policy statuses.
      - Valid claim statuses.
      - Crop type normalization helper.
    - Why it exists:
      - Prevents hardcoding `"Corn"`, `18.50`, or valid statuses in many places.

  - **File: `Farm.cs`**
    - Entity for farms.
    - Belongs to a farmer.
    - Has related quotes and policies.

  - **File: `Farmer.cs`**
    - Entity for farmers.
    - Owns farms, quotes, and policies.

  - **File: `Quote.cs`**
    - Entity for quotes.
    - Stores proposed insurance information before it becomes a policy.
    - Important fields:
      - `EstimatedPremium`
      - `Status`
      - `ConvertedPolicyId`

- **Folder: `Properties/`**
  - ASP.NET Core project launch settings.
  - Mostly used by local development tools.

  - **File: `launchSettings.json`**
    - Defines backend launch profiles.
    - Sets local URLs such as `http://localhost:5056`.
    - Sets `ASPNETCORE_ENVIRONMENT` to `Development`.

- **Folder: `Services/`**
  - Business logic layer.
  - Services sit between controllers and the database.
  - Why services exist:
    - Controllers stay small.
    - Business rules are reusable.
    - Logic is easier to test.
  - PHP comparison:
    - Similar to `app/Services/*` classes in Laravel or Symfony service classes.

  - **File: `ClaimService.cs`**
    - Handles claim logic.
    - Validates:
      - Policy exists.
      - Loss date exists.
      - Loss reason is provided.
      - Estimated loss is not negative.
      - Claim status is valid.
    - Uses EF Core async methods like `AnyAsync`, `FindAsync`, and `SaveChangesAsync`.

  - **File: `FarmerService.cs`**
    - Handles farmer CRUD.
    - Validates required farmer fields.
    - Blocks deleting a farmer if quotes or policies already exist.
    - Uses `ResponseMapper` to return DTOs instead of EF entities.

  - **File: `FarmService.cs`**
    - Handles farm creation and lookup.
    - Validates:
      - Farmer exists.
      - Farm name is present.
      - Acres are greater than zero.
      - County/state are present.

  - **File: `PolicyService.cs`**
    - Handles policy queries.
    - Policies are read here but created by `QuoteService`.

  - **File: `QuoteService.cs`**
    - Core business workflow service.
    - Handles:
      - Quote creation.
      - Premium calculation.
      - Valid crop type.
      - Valid coverage level.
      - Farm belongs to selected farmer.
      - Quote acres do not exceed farm acres.
      - Quote-to-policy conversion.
      - Duplicate conversion prevention.
    - Important method:
      - `CalculatePremium(string cropType, decimal acres, int coverageLevel)`
    - Formula:
      - `EstimatedPremium = Acres * BaseRate * CoverageMultiplier`

  - **File: `ResponseMapper.cs`**
    - Converts database models to DTO responses.
    - Example:
      - `Farmer` -> `FarmerResponse`
      - `Quote` -> `QuoteResponse`
    - Why it exists:
      - Centralized mapping avoids repeating object construction throughout services.

  - **File: `ServiceResult.cs`**
    - A small result wrapper used by services.
    - Lets service methods say:
      - success
      - invalid request
      - not found
      - conflict
    - Controllers then translate that into HTTP status codes.
    - PHP comparison:
      - Similar to returning a structured result object instead of throwing exceptions or returning mixed arrays everywhere.

  - **File: `Validation.cs`**
    - Shared validation helpers.
    - Keeps repeated checks out of service methods.

- **Folder: `bin/`**
  - Generated .NET build output.
  - Not source code.
  - Safe to delete; rebuilt by `dotnet build`.

- **Folder: `obj/`**
  - Generated .NET intermediate files.
  - Not source code.
  - Safe to delete; rebuilt by restore/build.

- **File: `AgPolicy.Api.csproj`**
  - C# project file.
  - Defines:
    - Target framework: `.NET 10`
    - Nullable reference type behavior.
    - NuGet package dependencies.
  - NuGet comparison:
    - NuGet is to .NET what Composer is to PHP and npm is to frontend JavaScript.

- **File: `AgPolicy.Api.http`**
  - IDE-friendly HTTP request scratch file.
  - Can be used by Visual Studio/VS Code REST tools to test endpoints.

- **File: `appsettings.Development.json`**
  - Development-specific backend config.
  - Similar concept to environment-specific config files.

- **File: `appsettings.json`**
  - Main backend config.
  - Contains:
    - SQLite connection string.
    - Logging config.
    - Allowed hosts.
  - PHP comparison:
    - Similar to config files that read database/logging settings, though .NET commonly stores them in JSON.

- **File: `dev-backend.err.log`**
  - Local backend error log from dev server.
  - Generated file.

- **File: `dev-backend.log`**
  - Local backend output log from dev server.
  - Generated file.

- **File: `Program.cs`**
  - Backend startup file.
  - Registers framework features:
    - Controllers.
    - Swagger.
    - CORS.
    - EF Core SQLite.
    - Services.
  - Configures middleware:
    - Swagger UI in development.
    - HTTPS redirection.
    - CORS.
    - Authorization.
    - Controller routing.
  - Runs seed data at startup.
  - PHP comparison:
    - Similar to a mix of Laravel service provider registration, route/middleware bootstrap, and app startup configuration.

## Folder: `frontend/`

This folder holds frontend projects. In this app, it contains one Vite React app.

- **Folder: `agpolicy-client/`**
  - The actual frontend application.
  - It runs in the browser.
  - It calls the backend API with `fetch`.

## Folder: `frontend/agpolicy-client/`

This is the React/Vite project root.

- **Folder: `public/`**
  - Static files served directly by Vite.
  - Files here are not imported through TypeScript; they are available as public assets.
  - Vue/Vite comparison:
    - Same concept as a Vue Vite app’s `public/` folder.

  - **File: `favicon.svg`**
    - Browser tab icon.

  - **File: `icons.svg`**
    - Static SVG sprite from the Vite template.
    - Not central to the AgPolicy app logic.

- **Folder: `src/`**
  - Main frontend source code.
  - React components, API clients, types, CSS, and app entry point live here.

  - **Folder: `api/`**
    - Frontend API abstraction layer.
    - Pages call functions from here instead of using `fetch` directly.
    - Vue comparison:
      - Similar to an `api/` or `services/` folder in a Vue app with Axios/fetch wrappers.

    - **File: `claimsApi.ts`**
      - Functions for claim endpoints:
        - `getClaims`
        - `createClaim`
        - `updateClaimStatus`
      - Uses the shared `apiRequest` helper.

    - **File: `farmersApi.ts`**
      - Functions for farmer/farm endpoints:
        - `getFarmers`
        - `createFarmer`
        - `getFarms`
        - `createFarm`
        - `getFarmerPolicies`

    - **File: `http.ts`**
      - Shared HTTP wrapper around browser `fetch`.
      - Defines `API_BASE_URL`.
      - Adds JSON headers.
      - Parses backend error responses.
      - Throws JavaScript `Error` objects for failed requests.
      - Vue comparison:
        - Similar to configuring an Axios instance with a base URL and error handling.

    - **File: `policiesApi.ts`**
      - Contains `getPolicies`.

    - **File: `quotesApi.ts`**
      - Functions for quote endpoints:
        - `getQuotes`
        - `getQuote`
        - `createQuote`
        - `convertQuote`

  - **Folder: `assets/`**
    - Static assets imported by React components.
    - Mostly leftover from the Vite template.

    - **File: `hero.png`**
      - Template image asset.

    - **File: `react.svg`**
      - React logo from the Vite template.

    - **File: `vite.svg`**
      - Vite logo from the Vite template.

  - **Folder: `components/`**
    - Reusable UI components and hooks.
    - Vue comparison:
      - Similar to reusable Vue components under `src/components`.
    - React detail:
      - Components are functions that return JSX.
      - Props are function parameters typed with TypeScript interfaces.

    - **File: `EmptyState.tsx`**
      - Displays a consistent empty-state message.
      - Used when a table/list has no records.

    - **File: `Field.tsx`**
      - Reusable form field wrapper.
      - Takes a `label` and `children`.
      - React detail:
        - `children` means nested JSX passed between opening/closing component tags.
      - Vue comparison:
        - Similar to a component using a default slot.

    - **File: `PageHeader.tsx`**
      - Reusable page heading with title and description.

    - **File: `StatGrid.tsx`**
      - Reusable dashboard metrics grid.
      - Receives an array of label/value pairs and renders statistic cards.

    - **File: `StatusMessage.tsx`**
      - Reusable success/error/info message component.
      - Uses a `tone` prop to change styling.

    - **File: `useAsyncData.ts`**
      - Custom React hook for loading async data.
      - Tracks:
        - `data`
        - `loading`
        - `error`
      - React hook comparison:
        - Similar to extracting shared Composition API logic into a Vue composable.
      - Vue comparison:
        - This is close to a `useAsyncData()` composable returning refs.

  - **Folder: `pages/`**
    - Page-level React components.
    - These are larger screens, not tiny reusable components.
    - Vue comparison:
      - Similar to Vue route views/pages.
    - This app does not use React Router; navigation is local state in `App.tsx`.

    - **File: `ClaimsPage.tsx`**
      - Claim management screen.
      - Loads policies and claims.
      - Lets user:
        - Create a claim.
        - Update claim status.
      - Uses controlled form inputs with React state.
      - Vue comparison:
        - Controlled inputs are similar in purpose to `v-model`, but React manually wires `value` and `onChange`.

    - **File: `DashboardPage.tsx`**
      - Dashboard screen.
      - Loads farmers, quotes, policies, and claims.
      - Computes totals in the frontend:
        - Farmer count.
        - Quote count.
        - Active policy count.
        - Total premium.
        - Open claim count.

    - **File: `FarmersPage.tsx`**
      - Farmer and farm management screen.
      - Lets user:
        - Create a farmer.
        - Select a farmer.
        - Create a farm for that farmer.
        - View farmers and selected farmer’s farms.

    - **File: `PoliciesPage.tsx`**
      - Policy list screen.
      - Displays policies created from converted quotes.
      - Does not create policies directly because policy creation is a backend workflow rule.

    - **File: `QuotesPage.tsx`**
      - Quote workflow screen.
      - Lets user:
        - Select farmer.
        - Select farm.
        - Choose crop type.
        - Choose coverage level.
        - Enter acres.
        - Create quote.
        - Select quote.
        - Convert quote to policy.
      - This is the frontend representation of the app’s main business workflow.

  - **Folder: `types/`**
    - TypeScript interfaces for API data.
    - Vue/TS comparison:
      - Same idea as defining interfaces for props, API responses, and form data in a Vue TypeScript app.
    - Why they matter:
      - Catch typos and shape mismatches before runtime.
      - Help editors autocomplete fields like `quote.estimatedPremium`.

    - **File: `claim.ts`**
      - Defines:
        - `Claim`
        - `CreateClaimRequest`

    - **File: `farmer.ts`**
      - Defines:
        - `Farmer`
        - `CreateFarmerRequest`
        - `Farm`
        - `CreateFarmRequest`

    - **File: `policy.ts`**
      - Defines `Policy`.

    - **File: `quote.ts`**
      - Defines:
        - `Quote`
        - `CreateQuoteRequest`
        - `ConvertQuoteResponse`

  - **File: `App.tsx`**
    - Main frontend shell.
    - Holds navigation state:
      - dashboard
      - farmers
      - quotes
      - policies
      - claims
    - Renders the selected page.
    - Uses `refreshKey` to tell pages to reload data after changes.
    - React detail:
      - `useState` stores component state.
      - `useMemo` memoizes page rendering based on dependencies.
    - Vue comparison:
      - Similar to a parent Vue component holding selected tab state and rendering different child components.

  - **File: `index.css`**
    - Global CSS.
    - Styles:
      - Layout shell.
      - Navigation.
      - Forms.
      - Tables.
      - Panels.
      - Status messages.
      - Dashboard cards.
      - Responsive behavior.

  - **File: `main.tsx`**
    - Frontend entry point.
    - Mounts the React app into the DOM.
    - React equivalent of a Vue `main.ts` that does `createApp(App).mount('#app')`.

- **Folder: `dist/`**
  - Generated production build output from `npm run build`.
  - Not source code.
  - Similar to a Vue/Vite `dist/` folder.

- **Folder: `node_modules/`**
  - Installed npm packages.
  - Not source code.
  - Recreated with `npm install`.

- **File: `.gitignore`**
  - Frontend-specific ignore file from Vite.

- **File: `dev-frontend.err.log`**
  - Generated error log from the Vite dev server.

- **File: `dev-frontend.log`**
  - Generated output log from the Vite dev server.

- **File: `eslint.config.js`**
  - ESLint configuration.
  - ESLint checks JavaScript/TypeScript code quality.
  - Similar to using ESLint in a Vue project.

- **File: `index.html`**
  - HTML shell for the React app.
  - Contains the root DOM node where React mounts.
  - Vue comparison:
    - Same concept as a Vite Vue app’s `index.html`.

- **File: `package-lock.json`**
  - Exact npm dependency lockfile.
  - Ensures repeatable installs.
  - Similar to `composer.lock` in PHP.

- **File: `package.json`**
  - npm project manifest.
  - Defines:
    - Project scripts.
    - Dependencies.
    - Dev dependencies.
  - Important scripts:
    - `npm run dev`
    - `npm run build`
    - `npm run lint`

- **File: `README.md`**
  - Vite-generated frontend readme.
  - Less important than the root project README.

- **File: `tsconfig.app.json`**
  - TypeScript config for browser app code.

- **File: `tsconfig.json`**
  - Root TypeScript config that references the app and Node configs.

- **File: `tsconfig.node.json`**
  - TypeScript config for Node-side tooling files, such as `vite.config.ts`.

- **File: `vite.config.ts`**
  - Vite build/dev server configuration.
  - Uses the React plugin.
  - Vue comparison:
    - Similar to `vite.config.ts` in a Vue project, except it uses React plugin instead of Vue plugin.

## Key Technology Concepts

### C# Basics You See Here

- `namespace`
  - Organizes classes.
  - Similar to PHP namespaces.

- `class`
  - Defines a type with properties and methods.
  - Similar to PHP classes.

- `{ get; set; }`
  - C# property syntax.
  - Example:
    - `public string FirstName { get; set; } = string.Empty;`
  - Means the property can be read and assigned.

- `?`
  - Nullable marker.
  - Example:
    - `string? Phone`
  - Means `Phone` can be `null`.

- `async` / `await`
  - Used for asynchronous work, especially database and HTTP operations.
  - Similar conceptually to JavaScript `async`/`await`.

- `Task<T>`
  - Represents an async operation that eventually returns `T`.
  - Example:
    - `Task<FarmerResponse>`

- `List<T>`
  - Generic list.
  - Similar to an array of typed objects.

### ASP.NET Core Basics

- Dependency injection
  - Services are registered in `Program.cs`.
  - Controllers receive services through constructor parameters.
  - Similar in spirit to Laravel’s service container.

- Middleware
  - Request pipeline components.
  - Examples:
    - CORS
    - HTTPS redirection
    - Authorization
    - Controller routing
  - Similar to middleware in Laravel or Express.

- Controllers
  - Classes that expose HTTP endpoints.
  - Attributes define routes and HTTP verbs.

- Swagger
  - Auto-generated API documentation/testing UI.
  - Available at `/swagger` in development.

### Entity Framework Core Basics

- Entity
  - A C# class that maps to a database table.

- `DbContext`
  - Database session/configuration object.
  - Used to query and save data.

- `DbSet<T>`
  - Represents a table.

- Migration
  - Versioned schema change.
  - Similar to Laravel migrations.

- Navigation property
  - Object relationship property.
  - Example:
    - `Farmer.Farms`
    - `Farm.Farmer`

### React Basics Compared To Vue

- React component
  - Function that returns JSX.
  - Vue comparison:
    - Similar to a Vue component template plus script logic, but written together in TSX.

- JSX/TSX
  - HTML-like syntax inside TypeScript.
  - Example:
    - `<button onClick={handleClick}>Save</button>`

- Props
  - Data passed from parent to child.
  - Vue comparison:
    - Same concept as Vue props.

- State
  - Data owned by a component.
  - React uses `useState`.
  - Vue uses `ref`, `reactive`, or component data.

- Effects
  - React uses `useEffect` for lifecycle-like side effects.
  - Vue comparison:
    - Similar to `onMounted`, `watch`, or `watchEffect` depending on usage.

- Controlled inputs
  - React form inputs usually bind `value` and `onChange`.
  - Vue comparison:
    - Similar to `v-model`, but more explicit.

### TypeScript Interfaces

Interfaces describe object shapes.

Example:

```ts
export interface Farmer {
  id: number
  firstName: string
  lastName: string
  email: string
}
```

This helps the frontend know what data comes back from the API.

PHP comparison:

- Similar to documenting array shapes, but enforced by the TypeScript compiler.
- More like using typed DTO classes in PHP than passing raw associative arrays.

## Main Workflow In Code

### Creating A Quote

Frontend:

- `QuotesPage.tsx`
  - User fills out the form.
  - Calls `createQuote(...)`.

- `quotesApi.ts`
  - Sends `POST /api/quotes`.

Backend:

- `QuotesController.cs`
  - Receives request.
  - Calls `QuoteService.CreateAsync`.

- `QuoteService.cs`
  - Validates farmer/farm/crop/coverage/acres.
  - Calculates premium.
  - Saves quote through EF Core.

- `AgPolicyDbContext.cs`
  - EF Core tracks and saves the quote to SQLite.

### Converting A Quote To A Policy

Frontend:

- `QuotesPage.tsx`
  - User selects a quote.
  - Clicks convert.
  - Calls `convertQuote(id)`.

- `quotesApi.ts`
  - Sends `POST /api/quotes/{id}/convert-to-policy`.

Backend:

- `QuotesController.cs`
  - Receives conversion request.

- `QuoteService.cs`
  - Loads quote with farmer and farm.
  - Ensures quote exists.
  - Ensures quote is `Draft` or `Approved`.
  - Ensures quote has not already been converted.
  - Creates `CropPolicy`.
  - Sets quote status to `Converted`.
  - Stores policy id on quote.

Database:

- `Policies` table gets a new row.
- `Quotes` row is updated.

## Why The App Is Structured This Way

- Controllers are thin.
  - They only handle HTTP concerns.

- Services hold business logic.
  - Easier to test and reason about.

- DTOs define API contracts.
  - Safer than exposing database entities directly.

- Models represent database tables.
  - EF Core can map them to SQLite.

- API client files isolate fetch logic.
  - React pages stay focused on UI and user interaction.

- Reusable components reduce duplication.
  - `Field`, `PageHeader`, `StatusMessage`, `EmptyState`, and `StatGrid` are shared across pages.

## Generated Folders You Can Mostly Ignore

- `.git/`
  - Git internals.

- `backend/AgPolicy.Api/bin/`
  - Compiled .NET output.

- `backend/AgPolicy.Api/obj/`
  - Intermediate .NET build files.

- `frontend/agpolicy-client/node_modules/`
  - npm packages.

- `frontend/agpolicy-client/dist/`
  - Production frontend build.

These are recreated by tools and should generally not be edited manually.
