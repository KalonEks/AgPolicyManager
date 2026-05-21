# AgPolicy Manager Full Code Study Guide

This guide embeds the current authored project files so you can study the app without opening the repository. It excludes generated dependency/build/runtime folders such as `.git`, `node_modules`, `bin`, `obj`, `dist`, database files, and logs.

## How To Read This

- Read folder sections first to understand where you are in the architecture.
- For each file, read the short purpose bullets, then scan the code block, then read the technology notes.
- PHP/Vue comparisons are included where they help map .NET/C#/React concepts to familiar patterns.
- Generated EF migration files are included because they show the database schema, but you normally do not edit them manually.

## Architecture Map

- Backend request flow: HTTP request -> Controller -> Service -> AgPolicyDbContext -> SQLite.
- Frontend request flow: React page -> API client -> fetch -> ASP.NET endpoint -> JSON -> React state -> UI.
- Main workflow: Farmer -> Farm -> Quote -> Convert to Policy -> Optional Claim.


## Folder: `.`

- Project root. This is the full-stack workspace tying together the .NET backend, React frontend, documentation, setup scripts, and Git metadata.

### File: `.gitignore`

- Purpose: Root Git ignore rules. Keeps generated build output, installed dependencies, local database files, and logs out of version control.
- Note: Plain text/configuration file used by project tooling.

~~~text
bin/
obj/
node_modules/
dist/
.vs/
.vscode/
*.user
*.suo
*.db
*.db-shm
*.db-wal
*.log
backend/AgPolicy.Api/agpolicy.db
backend/AgPolicy.Api/agpolicy.db-shm
backend/AgPolicy.Api/agpolicy.db-wal
~~~

- Explanation:
  - This file belongs to `.` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `AgPolicyManager.sln`

- Purpose: .NET solution file. Visual Studio/dotnet tooling uses this to group backend projects, roughly like an IDE workspace manifest.
- Note: Plain text/configuration file used by project tooling.

~~~text
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.5.2.0
MinimumVisualStudioVersion = 10.0.40219.1
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "backend", "backend", "{1AE8ACA6-933B-BF2A-3671-3E2EAC007D16}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "AgPolicy.Api", "backend\AgPolicy.Api\AgPolicy.Api.csproj", "{C06417B8-50D8-45F7-A580-F0B6EA4D5FA1}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{C06417B8-50D8-45F7-A580-F0B6EA4D5FA1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{C06417B8-50D8-45F7-A580-F0B6EA4D5FA1}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{C06417B8-50D8-45F7-A580-F0B6EA4D5FA1}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{C06417B8-50D8-45F7-A580-F0B6EA4D5FA1}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{C06417B8-50D8-45F7-A580-F0B6EA4D5FA1} = {1AE8ACA6-933B-BF2A-3671-3E2EAC007D16}
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {4DF54528-F525-4CD0-9EC6-E3A052EC69E3}
	EndGlobalSection
EndGlobal
~~~

- Explanation:
  - This file belongs to `.` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api`

- Project folder.

### File: `backend\AgPolicy.Api\AgPolicy.Api.csproj`

- Purpose: Project file.
- Note: NuGet package references are the .NET equivalent of Composer/npm dependencies.
- Note: TargetFramework tells dotnet which runtime/compiler version this project uses.

~~~xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.7">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.7" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="10.1.7" />
  </ItemGroup>

</Project>
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\AgPolicy.Api.http`

- Purpose: Project file.
- Note: Plain text/configuration file used by project tooling.

~~~http
@AgPolicy.Api_HostAddress = http://localhost:5056

GET {{AgPolicy.Api_HostAddress}}/weatherforecast/
Accept: application/json

###
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\appsettings.Development.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\appsettings.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=agpolicy.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api\Controllers`

- Project folder.

### File: `backend\AgPolicy.Api\Controllers\ApiControllerBase.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult<T> ToActionResult<T>(ServiceResult<T> result)
    {
        return result.Status switch
        {
            ServiceResultStatus.Success => Ok(result.Value),
            ServiceResultStatus.Invalid => BadRequest(new ApiErrorResponse { Message = result.Error ?? "Invalid request." }),
            ServiceResultStatus.NotFound => NotFound(new ApiErrorResponse { Message = result.Error ?? "Resource not found." }),
            ServiceResultStatus.Conflict => Conflict(new ApiErrorResponse { Message = result.Error ?? "Request conflicts with current state." }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResponse { Message = "Unexpected server error." })
        };
    }

    protected IActionResult ToNoContentResult(ServiceResult result)
    {
        return result.Status switch
        {
            ServiceResultStatus.Success => NoContent(),
            ServiceResultStatus.Invalid => BadRequest(new ApiErrorResponse { Message = result.Error ?? "Invalid request." }),
            ServiceResultStatus.NotFound => NotFound(new ApiErrorResponse { Message = result.Error ?? "Resource not found." }),
            ServiceResultStatus.Conflict => Conflict(new ApiErrorResponse { Message = result.Error ?? "Request conflicts with current state." }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResponse { Message = "Unexpected server error." })
        };
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Controllers` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Controllers\ClaimsController.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/claims")]
public class ClaimsController(ClaimService claimService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClaimResponse>>> GetClaims()
    {
        return Ok(await claimService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClaimResponse>> GetClaim(int id)
    {
        return ToActionResult(await claimService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<ClaimResponse>> CreateClaim(CreateClaimRequest request)
    {
        var result = await claimService.CreateAsync(request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(GetClaim), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<ClaimResponse>> UpdateStatus(int id, UpdateClaimStatusRequest request)
    {
        return ToActionResult(await claimService.UpdateStatusAsync(id, request));
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Controllers` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Controllers\FarmersController.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/farmers")]
public class FarmersController(FarmerService farmerService, FarmService farmService, PolicyService policyService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FarmerResponse>>> GetFarmers()
    {
        return Ok(await farmerService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FarmerResponse>> GetFarmer(int id)
    {
        return ToActionResult(await farmerService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<FarmerResponse>> CreateFarmer(CreateFarmerRequest request)
    {
        var result = await farmerService.CreateAsync(request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(GetFarmer), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<FarmerResponse>> UpdateFarmer(int id, UpdateFarmerRequest request)
    {
        return ToActionResult(await farmerService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFarmer(int id)
    {
        return ToNoContentResult(await farmerService.DeleteAsync(id));
    }

    [HttpGet("{farmerId:int}/farms")]
    public async Task<ActionResult<IReadOnlyList<FarmResponse>>> GetFarms(int farmerId)
    {
        return ToActionResult(await farmService.GetByFarmerAsync(farmerId));
    }

    [HttpPost("{farmerId:int}/farms")]
    public async Task<ActionResult<FarmResponse>> CreateFarm(int farmerId, CreateFarmRequest request)
    {
        var result = await farmService.CreateAsync(farmerId, request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(FarmsController.GetFarm), "Farms", new { id = result.Value.Id }, result.Value);
    }

    [HttpGet("{farmerId:int}/policies")]
    public async Task<ActionResult<IReadOnlyList<PolicyResponse>>> GetPolicies(int farmerId)
    {
        return ToActionResult(await policyService.GetByFarmerAsync(farmerId));
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Controllers` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Controllers\FarmsController.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/farms")]
public class FarmsController(FarmService farmService) : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FarmResponse>> GetFarm(int id)
    {
        return ToActionResult(await farmService.GetByIdAsync(id));
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Controllers` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Controllers\PoliciesController.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/policies")]
public class PoliciesController(PolicyService policyService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PolicyResponse>>> GetPolicies()
    {
        return Ok(await policyService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PolicyResponse>> GetPolicy(int id)
    {
        return ToActionResult(await policyService.GetByIdAsync(id));
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Controllers` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Controllers\QuotesController.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/quotes")]
public class QuotesController(QuoteService quoteService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<QuoteResponse>>> GetQuotes()
    {
        return Ok(await quoteService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuoteResponse>> GetQuote(int id)
    {
        return ToActionResult(await quoteService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<QuoteResponse>> CreateQuote(CreateQuoteRequest request)
    {
        var result = await quoteService.CreateAsync(request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(GetQuote), new { id = result.Value.Id }, result.Value);
    }

    [HttpPost("{id:int}/convert-to-policy")]
    public async Task<ActionResult<ConvertQuoteResponse>> ConvertToPolicy(int id)
    {
        return ToActionResult(await quoteService.ConvertToPolicyAsync(id));
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Controllers` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api\Data`

- Project folder.

### File: `backend\AgPolicy.Api\Data\AgPolicyDbContext.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Data;

public class AgPolicyDbContext(DbContextOptions<AgPolicyDbContext> options) : DbContext(options)
{
    public DbSet<Farmer> Farmers => Set<Farmer>();
    public DbSet<Farm> Farms => Set<Farm>();
    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<CropPolicy> Policies => Set<CropPolicy>();
    public DbSet<Claim> Claims => Set<Claim>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Farmer>()
            .HasMany(f => f.Farms)
            .WithOne(f => f.Farmer)
            .HasForeignKey(f => f.FarmerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Farmer>()
            .HasMany(f => f.Quotes)
            .WithOne(q => q.Farmer)
            .HasForeignKey(q => q.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farmer>()
            .HasMany(f => f.Policies)
            .WithOne(p => p.Farmer)
            .HasForeignKey(p => p.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farm>()
            .HasMany(f => f.Quotes)
            .WithOne(q => q.Farm)
            .HasForeignKey(q => q.FarmId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farm>()
            .HasMany(f => f.Policies)
            .WithOne(p => p.Farm)
            .HasForeignKey(p => p.FarmId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CropPolicy>()
            .HasMany(p => p.Claims)
            .WithOne(c => c.Policy)
            .HasForeignKey(c => c.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CropPolicy>()
            .HasOne(p => p.Quote)
            .WithOne()
            .HasForeignKey<CropPolicy>(p => p.QuoteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Farmer>().Property(f => f.FirstName).HasMaxLength(100);
        modelBuilder.Entity<Farmer>().Property(f => f.LastName).HasMaxLength(100);
        modelBuilder.Entity<Farmer>().Property(f => f.Email).HasMaxLength(200);
        modelBuilder.Entity<Farm>().Property(f => f.FarmName).HasMaxLength(150);
        modelBuilder.Entity<Quote>().Property(q => q.CropType).HasMaxLength(50);
        modelBuilder.Entity<CropPolicy>().Property(p => p.CropType).HasMaxLength(50);
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Data` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Data\SeedData.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AgPolicyDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();

        if (await dbContext.Farmers.AnyAsync())
        {
            return;
        }

        var farmer = new Farmer
        {
            FirstName = "Sam",
            LastName = "Miller",
            Email = "sam.miller@example.com",
            Phone = "555-1010",
            County = "Story",
            State = "IA"
        };

        var farm = new Farm
        {
            Farmer = farmer,
            FarmName = "North Quarter",
            Acres = 120,
            County = "Story",
            State = "IA"
        };

        var quote = new Quote
        {
            Farmer = farmer,
            Farm = farm,
            CropType = "Corn",
            Acres = 100,
            CoverageLevel = 75,
            EstimatedPremium = 2220.00m,
            Status = "Converted",
            CreatedAt = DateTime.UtcNow.AddDays(-2)
        };

        var policy = new CropPolicy
        {
            Farmer = farmer,
            Farm = farm,
            Quote = quote,
            CropType = quote.CropType,
            CoverageLevel = quote.CoverageLevel,
            InsuredAcres = quote.Acres,
            Premium = quote.EstimatedPremium,
            Status = "Active",
            EffectiveDate = DateTime.UtcNow.Date,
            ExpirationDate = DateTime.UtcNow.Date.AddYears(1)
        };

        var claim = new Claim
        {
            Policy = policy,
            LossDate = DateTime.UtcNow.Date.AddDays(-7),
            LossReason = "Hail damage",
            EstimatedLossAmount = 8500.00m,
            Status = "Open",
            Notes = "Initial sample claim for dashboard data."
        };

        dbContext.Farmers.Add(farmer);
        dbContext.Farms.Add(farm);
        dbContext.Quotes.Add(quote);
        dbContext.Policies.Add(policy);
        dbContext.Claims.Add(claim);

        await dbContext.SaveChangesAsync();

        quote.ConvertedPolicyId = policy.Id;
        await dbContext.SaveChangesAsync();
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Data` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api\DTOs`

- Project folder.

### File: `backend\AgPolicy.Api\DTOs\ApiErrorResponse.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.DTOs;

public class ApiErrorResponse
{
    public string Message { get; set; } = string.Empty;
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\DTOs` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\DTOs\ClaimDtos.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.DTOs;

public class CreateClaimRequest
{
    public int PolicyId { get; set; }
    public DateTime LossDate { get; set; }
    public string LossReason { get; set; } = string.Empty;
    public decimal EstimatedLossAmount { get; set; }
    public string? Notes { get; set; }
}

public class UpdateClaimStatusRequest
{
    public string Status { get; set; } = string.Empty;
}

public class ClaimResponse
{
    public int Id { get; set; }
    public int PolicyId { get; set; }
    public DateTime LossDate { get; set; }
    public string LossReason { get; set; } = string.Empty;
    public decimal EstimatedLossAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\DTOs` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\DTOs\FarmDtos.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.DTOs;

public class CreateFarmRequest
{
    public string FarmName { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class FarmResponse
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\DTOs` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\DTOs\FarmerDtos.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.DTOs;

public class CreateFarmerRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class UpdateFarmerRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class FarmerResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\DTOs` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\DTOs\PolicyDtos.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.DTOs;

public class PolicyResponse
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public int FarmId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public int QuoteId { get; set; }
    public string CropType { get; set; } = string.Empty;
    public int CoverageLevel { get; set; }
    public decimal InsuredAcres { get; set; }
    public decimal Premium { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\DTOs` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\DTOs\QuoteDtos.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.DTOs;

public class CreateQuoteRequest
{
    public int FarmerId { get; set; }
    public int FarmId { get; set; }
    public string CropType { get; set; } = string.Empty;
    public decimal? Acres { get; set; }
    public int CoverageLevel { get; set; }
}

public class QuoteResponse
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public int FarmId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public string CropType { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public int CoverageLevel { get; set; }
    public decimal EstimatedPremium { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int? ConvertedPolicyId { get; set; }
}

public class ConvertQuoteResponse
{
    public int QuoteId { get; set; }
    public int PolicyId { get; set; }
    public string Message { get; set; } = string.Empty;
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\DTOs` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api\Migrations`

- Project folder.

### File: `backend\AgPolicy.Api\Migrations\20260430203243_InitialCreate.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgPolicy.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    County = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FarmerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FarmName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Acres = table.Column<decimal>(type: "TEXT", nullable: false),
                    County = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farms_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FarmerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FarmId = table.Column<int>(type: "INTEGER", nullable: false),
                    CropType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Acres = table.Column<decimal>(type: "TEXT", nullable: false),
                    CoverageLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    EstimatedPremium = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConvertedPolicyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotes_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FarmerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FarmId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    CropType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CoverageLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    InsuredAcres = table.Column<decimal>(type: "TEXT", nullable: false),
                    Premium = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policies_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PolicyId = table.Column<int>(type: "INTEGER", nullable: false),
                    LossDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LossReason = table.Column<string>(type: "TEXT", nullable: false),
                    EstimatedLossAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyId",
                table: "Claims",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_FarmerId",
                table: "Farms",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_FarmerId",
                table: "Policies",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_FarmId",
                table: "Policies",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_QuoteId",
                table: "Policies",
                column: "QuoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_FarmerId",
                table: "Quotes",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_FarmId",
                table: "Quotes",
                column: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "Farmers");
        }
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Migrations` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Migrations\20260430203243_InitialCreate.Designer.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
// <auto-generated />
using System;
using AgPolicy.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgPolicy.Api.Migrations
{
    [DbContext(typeof(AgPolicyDbContext))]
    [Migration("20260430203243_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "10.0.7");

            modelBuilder.Entity("AgPolicy.Api.Models.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("EstimatedLossAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LossDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LossReason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("PolicyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.CropPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoverageLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CropType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FarmerId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("InsuredAcres")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Premium")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuoteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("FarmerId");

                    b.HasIndex("QuoteId")
                        .IsUnique();

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Acres")
                        .HasColumnType("TEXT");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FarmName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farmer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Acres")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ConvertedPolicyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoverageLevel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CropType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("EstimatedPremium")
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FarmerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("FarmerId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Claim", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.CropPolicy", "Policy")
                        .WithMany("Claims")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policy");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.CropPolicy", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.Farm", "Farm")
                        .WithMany("Policies")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AgPolicy.Api.Models.Farmer", "Farmer")
                        .WithMany("Policies")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AgPolicy.Api.Models.Quote", "Quote")
                        .WithOne()
                        .HasForeignKey("AgPolicy.Api.Models.CropPolicy", "QuoteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("Farmer");

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farm", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.Farmer", "Farmer")
                        .WithMany("Farms")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farmer");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Quote", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.Farm", "Farm")
                        .WithMany("Quotes")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AgPolicy.Api.Models.Farmer", "Farmer")
                        .WithMany("Quotes")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("Farmer");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.CropPolicy", b =>
                {
                    b.Navigation("Claims");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farm", b =>
                {
                    b.Navigation("Policies");

                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farmer", b =>
                {
                    b.Navigation("Farms");

                    b.Navigation("Policies");

                    b.Navigation("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Migrations` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Migrations\AgPolicyDbContextModelSnapshot.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
// <auto-generated />
using System;
using AgPolicy.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgPolicy.Api.Migrations
{
    [DbContext(typeof(AgPolicyDbContext))]
    partial class AgPolicyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "10.0.7");

            modelBuilder.Entity("AgPolicy.Api.Models.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("EstimatedLossAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LossDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LossReason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("PolicyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.CropPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoverageLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CropType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FarmerId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("InsuredAcres")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Premium")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuoteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("FarmerId");

                    b.HasIndex("QuoteId")
                        .IsUnique();

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Acres")
                        .HasColumnType("TEXT");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FarmName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farmer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Acres")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ConvertedPolicyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CoverageLevel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CropType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("EstimatedPremium")
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FarmerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("FarmerId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Claim", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.CropPolicy", "Policy")
                        .WithMany("Claims")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policy");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.CropPolicy", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.Farm", "Farm")
                        .WithMany("Policies")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AgPolicy.Api.Models.Farmer", "Farmer")
                        .WithMany("Policies")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AgPolicy.Api.Models.Quote", "Quote")
                        .WithOne()
                        .HasForeignKey("AgPolicy.Api.Models.CropPolicy", "QuoteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("Farmer");

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farm", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.Farmer", "Farmer")
                        .WithMany("Farms")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farmer");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Quote", b =>
                {
                    b.HasOne("AgPolicy.Api.Models.Farm", "Farm")
                        .WithMany("Quotes")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AgPolicy.Api.Models.Farmer", "Farmer")
                        .WithMany("Quotes")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("Farmer");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.CropPolicy", b =>
                {
                    b.Navigation("Claims");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farm", b =>
                {
                    b.Navigation("Policies");

                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("AgPolicy.Api.Models.Farmer", b =>
                {
                    b.Navigation("Farms");

                    b.Navigation("Policies");

                    b.Navigation("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Migrations` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api\Models`

- Project folder.

### File: `backend\AgPolicy.Api\Models\Claim.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.Models;

public class Claim
{
    public int Id { get; set; }
    public int PolicyId { get; set; }
    public CropPolicy? Policy { get; set; }
    public DateTime LossDate { get; set; }
    public string LossReason { get; set; } = string.Empty;
    public decimal EstimatedLossAmount { get; set; }
    public string Status { get; set; } = "Open";
    public string? Notes { get; set; }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Models` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Models\CropPolicy.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.Models;

public class CropPolicy
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }
    public int FarmId { get; set; }
    public Farm? Farm { get; set; }
    public int QuoteId { get; set; }
    public Quote? Quote { get; set; }
    public string CropType { get; set; } = string.Empty;
    public int CoverageLevel { get; set; }
    public decimal InsuredAcres { get; set; }
    public decimal Premium { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Claim> Claims { get; set; } = [];
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Models` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Models\DomainOptions.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.Models;

public static class DomainOptions
{
    public static readonly IReadOnlyDictionary<string, decimal> CropBaseRates =
        new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            ["Corn"] = 18.50m,
            ["Soybeans"] = 14.25m,
            ["Wheat"] = 11.75m,
            ["Cotton"] = 20.00m
        };

    public static readonly IReadOnlyDictionary<int, decimal> CoverageMultipliers =
        new Dictionary<int, decimal>
        {
            [50] = 0.75m,
            [65] = 1.00m,
            [75] = 1.20m,
            [85] = 1.45m
        };

    public static readonly string[] QuoteStatuses = ["Draft", "Approved", "Converted", "Rejected"];
    public static readonly string[] PolicyStatuses = ["Active", "Expired", "Cancelled"];
    public static readonly string[] ClaimStatuses = ["Open", "InReview", "Approved", "Denied", "Closed"];

    public static string NormalizeCropType(string cropType)
    {
        var match = CropBaseRates.Keys.FirstOrDefault(key =>
            string.Equals(key, cropType.Trim(), StringComparison.OrdinalIgnoreCase));

        return match ?? cropType.Trim();
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Models` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Models\Farm.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.Models;

public class Farm
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public List<Quote> Quotes { get; set; } = [];
    public List<CropPolicy> Policies { get; set; } = [];
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Models` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Models\Farmer.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.Models;

public class Farmer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public List<Farm> Farms { get; set; } = [];
    public List<Quote> Quotes { get; set; } = [];
    public List<CropPolicy> Policies { get; set; } = [];
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Models` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Models\Quote.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.Models;

public class Quote
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }
    public int FarmId { get; set; }
    public Farm? Farm { get; set; }
    public string CropType { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public int CoverageLevel { get; set; }
    public decimal EstimatedPremium { get; set; }
    public string Status { get; set; } = "Draft";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? ConvertedPolicyId { get; set; }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Models` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api`

- Project folder.

### File: `backend\AgPolicy.Api\Program.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Data;
using AgPolicy.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AgPolicyDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<FarmerService>();
builder.Services.AddScoped<FarmService>();
builder.Services.AddScoped<QuoteService>();
builder.Services.AddScoped<PolicyService>();
builder.Services.AddScoped<ClaimService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AgPolicyDbContext>();
    await SeedData.InitializeAsync(dbContext);
}

app.Run();
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api\Properties`

- Project folder.

### File: `backend\AgPolicy.Api\Properties\launchSettings.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:5056",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7229;http://localhost:5056",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Properties` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `backend\AgPolicy.Api\Services`

- Project folder.

### File: `backend\AgPolicy.Api\Services\ClaimService.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Data;
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Services;

public class ClaimService(AgPolicyDbContext dbContext)
{
    public async Task<IReadOnlyList<ClaimResponse>> GetAllAsync()
    {
        return await dbContext.Claims
            .OrderByDescending(c => c.LossDate)
            .Select(c => ResponseMapper.ToResponse(c))
            .ToListAsync();
    }

    public async Task<ServiceResult<ClaimResponse>> GetByIdAsync(int id)
    {
        var claim = await dbContext.Claims.FindAsync(id);
        return claim is null
            ? ServiceResult<ClaimResponse>.NotFound("Claim not found.")
            : ServiceResult<ClaimResponse>.Success(ResponseMapper.ToResponse(claim));
    }

    public async Task<ServiceResult<ClaimResponse>> CreateAsync(CreateClaimRequest request)
    {
        if (!await dbContext.Policies.AnyAsync(p => p.Id == request.PolicyId))
        {
            return ServiceResult<ClaimResponse>.NotFound("Policy not found.");
        }

        if (request.LossDate == default)
        {
            return ServiceResult<ClaimResponse>.Invalid("Loss date is required.");
        }

        if (Validation.IsBlank(request.LossReason))
        {
            return ServiceResult<ClaimResponse>.Invalid("Loss reason is required.");
        }

        if (request.EstimatedLossAmount < 0)
        {
            return ServiceResult<ClaimResponse>.Invalid("Estimated loss amount cannot be negative.");
        }

        var claim = new Claim
        {
            PolicyId = request.PolicyId,
            LossDate = request.LossDate,
            LossReason = request.LossReason.Trim(),
            EstimatedLossAmount = request.EstimatedLossAmount,
            Status = "Open",
            Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim()
        };

        dbContext.Claims.Add(claim);
        await dbContext.SaveChangesAsync();
        return ServiceResult<ClaimResponse>.Success(ResponseMapper.ToResponse(claim));
    }

    public async Task<ServiceResult<ClaimResponse>> UpdateStatusAsync(int id, UpdateClaimStatusRequest request)
    {
        var claim = await dbContext.Claims.FindAsync(id);
        if (claim is null)
        {
            return ServiceResult<ClaimResponse>.NotFound("Claim not found.");
        }

        if (!Validation.IsValidClaimStatus(request.Status))
        {
            return ServiceResult<ClaimResponse>.Invalid("Claim status must be Open, InReview, Approved, Denied, or Closed.");
        }

        claim.Status = Validation.NormalizeStatus(request.Status, DomainOptions.ClaimStatuses);
        await dbContext.SaveChangesAsync();
        return ServiceResult<ClaimResponse>.Success(ResponseMapper.ToResponse(claim));
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Services\FarmerService.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Data;
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Services;

public class FarmerService(AgPolicyDbContext dbContext)
{
    public async Task<IReadOnlyList<FarmerResponse>> GetAllAsync()
    {
        return await dbContext.Farmers
            .OrderBy(f => f.LastName)
            .ThenBy(f => f.FirstName)
            .Select(f => ResponseMapper.ToResponse(f))
            .ToListAsync();
    }

    public async Task<ServiceResult<FarmerResponse>> GetByIdAsync(int id)
    {
        var farmer = await dbContext.Farmers.FindAsync(id);
        return farmer is null
            ? ServiceResult<FarmerResponse>.NotFound("Farmer not found.")
            : ServiceResult<FarmerResponse>.Success(ResponseMapper.ToResponse(farmer));
    }

    public async Task<ServiceResult<FarmerResponse>> CreateAsync(CreateFarmerRequest request)
    {
        var validationError = ValidateFarmer(request.FirstName, request.LastName, request.Email, request.County, request.State);
        if (validationError is not null)
        {
            return ServiceResult<FarmerResponse>.Invalid(validationError);
        }

        var farmer = new Farmer
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim(),
            County = request.County.Trim(),
            State = request.State.Trim()
        };

        dbContext.Farmers.Add(farmer);
        await dbContext.SaveChangesAsync();
        return ServiceResult<FarmerResponse>.Success(ResponseMapper.ToResponse(farmer));
    }

    public async Task<ServiceResult<FarmerResponse>> UpdateAsync(int id, UpdateFarmerRequest request)
    {
        var farmer = await dbContext.Farmers.FindAsync(id);
        if (farmer is null)
        {
            return ServiceResult<FarmerResponse>.NotFound("Farmer not found.");
        }

        var validationError = ValidateFarmer(request.FirstName, request.LastName, request.Email, request.County, request.State);
        if (validationError is not null)
        {
            return ServiceResult<FarmerResponse>.Invalid(validationError);
        }

        farmer.FirstName = request.FirstName.Trim();
        farmer.LastName = request.LastName.Trim();
        farmer.Email = request.Email.Trim();
        farmer.Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim();
        farmer.County = request.County.Trim();
        farmer.State = request.State.Trim();

        await dbContext.SaveChangesAsync();
        return ServiceResult<FarmerResponse>.Success(ResponseMapper.ToResponse(farmer));
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var farmer = await dbContext.Farmers
            .Include(f => f.Quotes)
            .Include(f => f.Policies)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (farmer is null)
        {
            return ServiceResult.NotFound("Farmer not found.");
        }

        if (farmer.Quotes.Count > 0 || farmer.Policies.Count > 0)
        {
            return ServiceResult.Conflict("Farmer cannot be deleted after quotes or policies have been created.");
        }

        dbContext.Farmers.Remove(farmer);
        await dbContext.SaveChangesAsync();
        return ServiceResult.Success();
    }

    private static string? ValidateFarmer(string firstName, string lastName, string email, string county, string state)
    {
        if (Validation.IsBlank(firstName) || Validation.IsBlank(lastName))
        {
            return "First name and last name are required.";
        }

        if (Validation.IsBlank(email) || !email.Contains('@'))
        {
            return "A valid email is required.";
        }

        if (Validation.IsBlank(county) || Validation.IsBlank(state))
        {
            return "County and state are required.";
        }

        return null;
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Services\FarmService.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Data;
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Services;

public class FarmService(AgPolicyDbContext dbContext)
{
    public async Task<ServiceResult<IReadOnlyList<FarmResponse>>> GetByFarmerAsync(int farmerId)
    {
        if (!await dbContext.Farmers.AnyAsync(f => f.Id == farmerId))
        {
            return ServiceResult<IReadOnlyList<FarmResponse>>.NotFound("Farmer not found.");
        }

        var farms = await dbContext.Farms
            .Where(f => f.FarmerId == farmerId)
            .OrderBy(f => f.FarmName)
            .Select(f => ResponseMapper.ToResponse(f))
            .ToListAsync();

        return ServiceResult<IReadOnlyList<FarmResponse>>.Success(farms);
    }

    public async Task<ServiceResult<FarmResponse>> GetByIdAsync(int id)
    {
        var farm = await dbContext.Farms.FindAsync(id);
        return farm is null
            ? ServiceResult<FarmResponse>.NotFound("Farm not found.")
            : ServiceResult<FarmResponse>.Success(ResponseMapper.ToResponse(farm));
    }

    public async Task<ServiceResult<FarmResponse>> CreateAsync(int farmerId, CreateFarmRequest request)
    {
        if (!await dbContext.Farmers.AnyAsync(f => f.Id == farmerId))
        {
            return ServiceResult<FarmResponse>.NotFound("Farmer not found.");
        }

        if (Validation.IsBlank(request.FarmName))
        {
            return ServiceResult<FarmResponse>.Invalid("Farm name is required.");
        }

        if (request.Acres <= 0)
        {
            return ServiceResult<FarmResponse>.Invalid("Acres must be greater than zero.");
        }

        if (Validation.IsBlank(request.County) || Validation.IsBlank(request.State))
        {
            return ServiceResult<FarmResponse>.Invalid("County and state are required.");
        }

        var farm = new Farm
        {
            FarmerId = farmerId,
            FarmName = request.FarmName.Trim(),
            Acres = request.Acres,
            County = request.County.Trim(),
            State = request.State.Trim()
        };

        dbContext.Farms.Add(farm);
        await dbContext.SaveChangesAsync();
        return ServiceResult<FarmResponse>.Success(ResponseMapper.ToResponse(farm));
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Services\PolicyService.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Data;
using AgPolicy.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Services;

public class PolicyService(AgPolicyDbContext dbContext)
{
    public async Task<IReadOnlyList<PolicyResponse>> GetAllAsync()
    {
        return await dbContext.Policies
            .Include(p => p.Farmer)
            .Include(p => p.Farm)
            .OrderByDescending(p => p.EffectiveDate)
            .Select(p => ResponseMapper.ToResponse(p))
            .ToListAsync();
    }

    public async Task<ServiceResult<PolicyResponse>> GetByIdAsync(int id)
    {
        var policy = await dbContext.Policies
            .Include(p => p.Farmer)
            .Include(p => p.Farm)
            .FirstOrDefaultAsync(p => p.Id == id);
        return policy is null
            ? ServiceResult<PolicyResponse>.NotFound("Policy not found.")
            : ServiceResult<PolicyResponse>.Success(ResponseMapper.ToResponse(policy));
    }

    public async Task<ServiceResult<IReadOnlyList<PolicyResponse>>> GetByFarmerAsync(int farmerId)
    {
        if (!await dbContext.Farmers.AnyAsync(f => f.Id == farmerId))
        {
            return ServiceResult<IReadOnlyList<PolicyResponse>>.NotFound("Farmer not found.");
        }

        var policies = await dbContext.Policies
            .Include(p => p.Farmer)
            .Include(p => p.Farm)
            .Where(p => p.FarmerId == farmerId)
            .OrderByDescending(p => p.EffectiveDate)
            .Select(p => ResponseMapper.ToResponse(p))
            .ToListAsync();

        return ServiceResult<IReadOnlyList<PolicyResponse>>.Success(policies);
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Services\QuoteService.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Data;
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AgPolicy.Api.Services;

public class QuoteService(AgPolicyDbContext dbContext)
{
    public async Task<IReadOnlyList<QuoteResponse>> GetAllAsync()
    {
        return await dbContext.Quotes
            .Include(q => q.Farmer)
            .Include(q => q.Farm)
            .OrderByDescending(q => q.CreatedAt)
            .Select(q => ResponseMapper.ToResponse(q))
            .ToListAsync();
    }

    public async Task<ServiceResult<QuoteResponse>> GetByIdAsync(int id)
    {
        var quote = await dbContext.Quotes
            .Include(q => q.Farmer)
            .Include(q => q.Farm)
            .FirstOrDefaultAsync(q => q.Id == id);
        return quote is null
            ? ServiceResult<QuoteResponse>.NotFound("Quote not found.")
            : ServiceResult<QuoteResponse>.Success(ResponseMapper.ToResponse(quote));
    }

    public async Task<ServiceResult<QuoteResponse>> CreateAsync(CreateQuoteRequest request)
    {
        if (!Validation.IsValidCropType(request.CropType))
        {
            return ServiceResult<QuoteResponse>.Invalid("Crop type must be Corn, Soybeans, Wheat, or Cotton.");
        }

        if (!Validation.IsValidCoverageLevel(request.CoverageLevel))
        {
            return ServiceResult<QuoteResponse>.Invalid("Coverage level must be 50, 65, 75, or 85.");
        }

        var farmer = await dbContext.Farmers.FindAsync(request.FarmerId);
        if (farmer is null)
        {
            return ServiceResult<QuoteResponse>.NotFound("Farmer not found.");
        }

        var farm = await dbContext.Farms.FindAsync(request.FarmId);
        if (farm is null)
        {
            return ServiceResult<QuoteResponse>.NotFound("Farm not found.");
        }

        if (farm.FarmerId != farmer.Id)
        {
            return ServiceResult<QuoteResponse>.Invalid("Farm must belong to the selected farmer.");
        }

        var quotedAcres = request.Acres ?? farm.Acres;

        if (quotedAcres <= 0)
        {
            return ServiceResult<QuoteResponse>.Invalid("Acres must be greater than zero.");
        }

        if (quotedAcres > farm.Acres)
        {
            return ServiceResult<QuoteResponse>.Invalid("Quote acres cannot exceed the farm acres.");
        }

        var quote = new Quote
        {
            FarmerId = request.FarmerId,
            Farmer = farmer,
            FarmId = request.FarmId,
            Farm = farm,
            CropType = DomainOptions.NormalizeCropType(request.CropType),
            Acres = quotedAcres,
            CoverageLevel = request.CoverageLevel,
            EstimatedPremium = CalculatePremium(request.CropType, quotedAcres, request.CoverageLevel),
            Status = "Draft",
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Quotes.Add(quote);
        await dbContext.SaveChangesAsync();
        return ServiceResult<QuoteResponse>.Success(ResponseMapper.ToResponse(quote));
    }

    public async Task<ServiceResult<ConvertQuoteResponse>> ConvertToPolicyAsync(int quoteId)
    {
        var quote = await dbContext.Quotes
            .Include(q => q.Farmer)
            .Include(q => q.Farm)
            .FirstOrDefaultAsync(q => q.Id == quoteId);

        if (quote is null)
        {
            return ServiceResult<ConvertQuoteResponse>.NotFound("Quote not found.");
        }

        if (quote.Status is not ("Draft" or "Approved"))
        {
            return ServiceResult<ConvertQuoteResponse>.Conflict("Only Draft or Approved quotes can be converted.");
        }

        if (quote.ConvertedPolicyId is not null)
        {
            return ServiceResult<ConvertQuoteResponse>.Conflict("Quote has already been converted to a policy.");
        }

        if (quote.Farmer is null)
        {
            return ServiceResult<ConvertQuoteResponse>.NotFound("Farmer not found.");
        }

        if (quote.Farm is null)
        {
            return ServiceResult<ConvertQuoteResponse>.NotFound("Farm not found.");
        }

        if (quote.Farm.FarmerId != quote.FarmerId)
        {
            return ServiceResult<ConvertQuoteResponse>.Conflict("Farm does not belong to the quote farmer.");
        }

        var today = DateTime.UtcNow.Date;
        var policy = new CropPolicy
        {
            FarmerId = quote.FarmerId,
            FarmId = quote.FarmId,
            QuoteId = quote.Id,
            CropType = quote.CropType,
            CoverageLevel = quote.CoverageLevel,
            InsuredAcres = quote.Acres,
            Premium = quote.EstimatedPremium,
            Status = "Active",
            EffectiveDate = today,
            ExpirationDate = today.AddYears(1)
        };

        dbContext.Policies.Add(policy);
        await dbContext.SaveChangesAsync();

        quote.Status = "Converted";
        quote.ConvertedPolicyId = policy.Id;
        await dbContext.SaveChangesAsync();

        return ServiceResult<ConvertQuoteResponse>.Success(new ConvertQuoteResponse
        {
            QuoteId = quote.Id,
            PolicyId = policy.Id,
            Message = "Quote converted to active policy."
        });
    }

    public static decimal CalculatePremium(string cropType, decimal acres, int coverageLevel)
    {
        if (!DomainOptions.CropBaseRates.TryGetValue(cropType.Trim(), out var baseRate))
        {
            throw new ArgumentException("Invalid crop type.", nameof(cropType));
        }

        if (!DomainOptions.CoverageMultipliers.TryGetValue(coverageLevel, out var multiplier))
        {
            throw new ArgumentException("Invalid coverage level.", nameof(coverageLevel));
        }

        return Math.Round(acres * baseRate * multiplier, 2);
    }
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Services\ResponseMapper.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.DTOs;
using AgPolicy.Api.Models;

namespace AgPolicy.Api.Services;

public static class ResponseMapper
{
    public static FarmerResponse ToResponse(Farmer farmer) => new()
    {
        Id = farmer.Id,
        FirstName = farmer.FirstName,
        LastName = farmer.LastName,
        Email = farmer.Email,
        Phone = farmer.Phone,
        County = farmer.County,
        State = farmer.State
    };

    public static FarmResponse ToResponse(Farm farm) => new()
    {
        Id = farm.Id,
        FarmerId = farm.FarmerId,
        FarmName = farm.FarmName,
        Acres = farm.Acres,
        County = farm.County,
        State = farm.State
    };

    public static QuoteResponse ToResponse(Quote quote) => new()
    {
        Id = quote.Id,
        FarmerId = quote.FarmerId,
        FarmerName = quote.Farmer is null ? string.Empty : $"{quote.Farmer.FirstName} {quote.Farmer.LastName}",
        FarmId = quote.FarmId,
        FarmName = quote.Farm?.FarmName ?? string.Empty,
        CropType = quote.CropType,
        Acres = quote.Acres,
        CoverageLevel = quote.CoverageLevel,
        EstimatedPremium = quote.EstimatedPremium,
        Status = quote.Status,
        CreatedAt = quote.CreatedAt,
        ConvertedPolicyId = quote.ConvertedPolicyId
    };

    public static PolicyResponse ToResponse(CropPolicy policy) => new()
    {
        Id = policy.Id,
        FarmerId = policy.FarmerId,
        FarmerName = policy.Farmer is null ? string.Empty : $"{policy.Farmer.FirstName} {policy.Farmer.LastName}",
        FarmId = policy.FarmId,
        FarmName = policy.Farm?.FarmName ?? string.Empty,
        QuoteId = policy.QuoteId,
        CropType = policy.CropType,
        CoverageLevel = policy.CoverageLevel,
        InsuredAcres = policy.InsuredAcres,
        Premium = policy.Premium,
        Status = policy.Status,
        EffectiveDate = policy.EffectiveDate,
        ExpirationDate = policy.ExpirationDate
    };

    public static ClaimResponse ToResponse(Claim claim) => new()
    {
        Id = claim.Id,
        PolicyId = claim.PolicyId,
        LossDate = claim.LossDate,
        LossReason = claim.LossReason,
        EstimatedLossAmount = claim.EstimatedLossAmount,
        Status = claim.Status,
        Notes = claim.Notes
    };
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Services\ServiceResult.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
namespace AgPolicy.Api.Services;

public enum ServiceResultStatus
{
    Success,
    Invalid,
    NotFound,
    Conflict
}

public class ServiceResult<T>
{
    private ServiceResult(ServiceResultStatus status, T? value, string? error)
    {
        Status = status;
        Value = value;
        Error = error;
    }

    public ServiceResultStatus Status { get; }
    public T? Value { get; }
    public string? Error { get; }

    public static ServiceResult<T> Success(T value) => new(ServiceResultStatus.Success, value, null);
    public static ServiceResult<T> Invalid(string error) => new(ServiceResultStatus.Invalid, default, error);
    public static ServiceResult<T> NotFound(string error) => new(ServiceResultStatus.NotFound, default, error);
    public static ServiceResult<T> Conflict(string error) => new(ServiceResultStatus.Conflict, default, error);
}

public class ServiceResult
{
    private ServiceResult(ServiceResultStatus status, string? error)
    {
        Status = status;
        Error = error;
    }

    public ServiceResultStatus Status { get; }
    public string? Error { get; }

    public static ServiceResult Success() => new(ServiceResultStatus.Success, null);
    public static ServiceResult Invalid(string error) => new(ServiceResultStatus.Invalid, error);
    public static ServiceResult NotFound(string error) => new(ServiceResultStatus.NotFound, error);
    public static ServiceResult Conflict(string error) => new(ServiceResultStatus.Conflict, error);
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `backend\AgPolicy.Api\Services\Validation.cs`

- Purpose: Project file.
- Note: C# uses namespaces/classes/properties instead of PHP arrays for most app structures.
- Note: Async database/API work returns Task or Task<T>; await pauses until the operation completes.
- Note: The file participates in ASP.NET Core dependency injection when registered or referenced by controllers/services.

~~~csharp
using AgPolicy.Api.Models;

namespace AgPolicy.Api.Services;

public static class Validation
{
    public static bool IsBlank(string value) => string.IsNullOrWhiteSpace(value);

    public static bool IsValidCropType(string cropType) =>
        !IsBlank(cropType) && DomainOptions.CropBaseRates.ContainsKey(cropType.Trim());

    public static bool IsValidCoverageLevel(int coverageLevel) =>
        DomainOptions.CoverageMultipliers.ContainsKey(coverageLevel);

    public static bool IsValidClaimStatus(string status) =>
        DomainOptions.ClaimStatuses.Any(valid => string.Equals(valid, status.Trim(), StringComparison.OrdinalIgnoreCase));

    public static string NormalizeStatus(string status, IEnumerable<string> validStatuses) =>
        validStatuses.First(valid => string.Equals(valid, status.Trim(), StringComparison.OrdinalIgnoreCase));
}
~~~

- Explanation:
  - This file belongs to `backend\AgPolicy.Api\Services` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `docs`

- Human documentation. These Markdown files are study/reference material and are not used by the app at runtime.

### File: `docs\PROJECT_WALKTHROUGH.md`

- Purpose: Project file.
- Note: Markdown documentation for humans, not runtime code.

~~~markdown
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
    - EF Coreâ€™s snapshot of the current model.
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
    - Same concept as a Vue Vite appâ€™s `public/` folder.

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
        - View farmers and selected farmerâ€™s farms.

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
      - This is the frontend representation of the appâ€™s main business workflow.

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
    - Same concept as a Vite Vue appâ€™s `index.html`.

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
  - Similar in spirit to Laravelâ€™s service container.

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
~~~

- Explanation:
  - This file belongs to `docs` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client`

- Project folder.

### File: `frontend\agpolicy-client\.gitignore`

- Purpose: Project file.
- Note: Plain text/configuration file used by project tooling.

~~~text
# Logs
logs
*.log
npm-debug.log*
yarn-debug.log*
yarn-error.log*
pnpm-debug.log*
lerna-debug.log*

node_modules
dist
dist-ssr
*.local

# Editor directories and files
.vscode/*
!.vscode/extensions.json
.idea
.DS_Store
*.suo
*.ntvs*
*.njsproj
*.sln
*.sw?
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\eslint.config.js`

- Purpose: Project file.
- Note: Plain text/configuration file used by project tooling.

~~~text
import js from '@eslint/js'
import globals from 'globals'
import reactHooks from 'eslint-plugin-react-hooks'
import reactRefresh from 'eslint-plugin-react-refresh'
import tseslint from 'typescript-eslint'
import { defineConfig, globalIgnores } from 'eslint/config'

export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      js.configs.recommended,
      tseslint.configs.recommended,
      reactHooks.configs.flat.recommended,
      reactRefresh.configs.vite,
    ],
    languageOptions: {
      globals: globals.browser,
    },
  },
])
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\index.html`

- Purpose: Project file.
- Note: Plain text/configuration file used by project tooling.

~~~html
<!doctype html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <link rel="icon" type="image/svg+xml" href="/favicon.svg" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>agpolicy-client</title>
  </head>
  <body>
    <div id="root"></div>
    <script type="module" src="/src/main.tsx"></script>
  </body>
</html>
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\package.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "name": "agpolicy-client",
  "private": true,
  "version": "0.0.0",
  "type": "module",
  "scripts": {
    "dev": "vite",
    "build": "tsc -b && vite build",
    "lint": "eslint .",
    "preview": "vite preview"
  },
  "dependencies": {
    "react": "^19.2.5",
    "react-dom": "^19.2.5"
  },
  "devDependencies": {
    "@eslint/js": "^10.0.1",
    "@types/node": "^24.12.2",
    "@types/react": "^19.2.14",
    "@types/react-dom": "^19.2.3",
    "@vitejs/plugin-react": "^6.0.1",
    "eslint": "^10.2.1",
    "eslint-plugin-react-hooks": "^7.1.1",
    "eslint-plugin-react-refresh": "^0.5.2",
    "globals": "^17.5.0",
    "typescript": "~6.0.2",
    "typescript-eslint": "^8.58.2",
    "vite": "^8.0.10"
  }
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\package-lock.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "name": "agpolicy-client",
  "version": "0.0.0",
  "lockfileVersion": 3,
  "requires": true,
  "packages": {
    "": {
      "name": "agpolicy-client",
      "version": "0.0.0",
      "dependencies": {
        "react": "^19.2.5",
        "react-dom": "^19.2.5"
      },
      "devDependencies": {
        "@eslint/js": "^10.0.1",
        "@types/node": "^24.12.2",
        "@types/react": "^19.2.14",
        "@types/react-dom": "^19.2.3",
        "@vitejs/plugin-react": "^6.0.1",
        "eslint": "^10.2.1",
        "eslint-plugin-react-hooks": "^7.1.1",
        "eslint-plugin-react-refresh": "^0.5.2",
        "globals": "^17.5.0",
        "typescript": "~6.0.2",
        "typescript-eslint": "^8.58.2",
        "vite": "^8.0.10"
      }
    },
    "node_modules/@babel/code-frame": {
      "version": "7.29.0",
      "resolved": "https://registry.npmjs.org/@babel/code-frame/-/code-frame-7.29.0.tgz",
      "integrity": "sha512-9NhCeYjq9+3uxgdtp20LSiJXJvN0FeCtNGpJxuMFZ1Kv3cWUNb6DOhJwUvcVCzKGR66cw4njwM6hrJLqgOwbcw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/helper-validator-identifier": "^7.28.5",
        "js-tokens": "^4.0.0",
        "picocolors": "^1.1.1"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/compat-data": {
      "version": "7.29.0",
      "resolved": "https://registry.npmjs.org/@babel/compat-data/-/compat-data-7.29.0.tgz",
      "integrity": "sha512-T1NCJqT/j9+cn8fvkt7jtwbLBfLC/1y1c7NtCeXFRgzGTsafi68MRv8yzkYSapBnFA6L3U2VSc02ciDzoAJhJg==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/core": {
      "version": "7.29.0",
      "resolved": "https://registry.npmjs.org/@babel/core/-/core-7.29.0.tgz",
      "integrity": "sha512-CGOfOJqWjg2qW/Mb6zNsDm+u5vFQ8DxXfbM09z69p5Z6+mE1ikP2jUXw+j42Pf1XTYED2Rni5f95npYeuwMDQA==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "dependencies": {
        "@babel/code-frame": "^7.29.0",
        "@babel/generator": "^7.29.0",
        "@babel/helper-compilation-targets": "^7.28.6",
        "@babel/helper-module-transforms": "^7.28.6",
        "@babel/helpers": "^7.28.6",
        "@babel/parser": "^7.29.0",
        "@babel/template": "^7.28.6",
        "@babel/traverse": "^7.29.0",
        "@babel/types": "^7.29.0",
        "@jridgewell/remapping": "^2.3.5",
        "convert-source-map": "^2.0.0",
        "debug": "^4.1.0",
        "gensync": "^1.0.0-beta.2",
        "json5": "^2.2.3",
        "semver": "^6.3.1"
      },
      "engines": {
        "node": ">=6.9.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/babel"
      }
    },
    "node_modules/@babel/generator": {
      "version": "7.29.1",
      "resolved": "https://registry.npmjs.org/@babel/generator/-/generator-7.29.1.tgz",
      "integrity": "sha512-qsaF+9Qcm2Qv8SRIMMscAvG4O3lJ0F1GuMo5HR/Bp02LopNgnZBC/EkbevHFeGs4ls/oPz9v+Bsmzbkbe+0dUw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/parser": "^7.29.0",
        "@babel/types": "^7.29.0",
        "@jridgewell/gen-mapping": "^0.3.12",
        "@jridgewell/trace-mapping": "^0.3.28",
        "jsesc": "^3.0.2"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/helper-compilation-targets": {
      "version": "7.28.6",
      "resolved": "https://registry.npmjs.org/@babel/helper-compilation-targets/-/helper-compilation-targets-7.28.6.tgz",
      "integrity": "sha512-JYtls3hqi15fcx5GaSNL7SCTJ2MNmjrkHXg4FSpOA/grxK8KwyZ5bubHsCq8FXCkua6xhuaaBit+3b7+VZRfcA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/compat-data": "^7.28.6",
        "@babel/helper-validator-option": "^7.27.1",
        "browserslist": "^4.24.0",
        "lru-cache": "^5.1.1",
        "semver": "^6.3.1"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/helper-globals": {
      "version": "7.28.0",
      "resolved": "https://registry.npmjs.org/@babel/helper-globals/-/helper-globals-7.28.0.tgz",
      "integrity": "sha512-+W6cISkXFa1jXsDEdYA8HeevQT/FULhxzR99pxphltZcVaugps53THCeiWA8SguxxpSp3gKPiuYfSWopkLQ4hw==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/helper-module-imports": {
      "version": "7.28.6",
      "resolved": "https://registry.npmjs.org/@babel/helper-module-imports/-/helper-module-imports-7.28.6.tgz",
      "integrity": "sha512-l5XkZK7r7wa9LucGw9LwZyyCUscb4x37JWTPz7swwFE/0FMQAGpiWUZn8u9DzkSBWEcK25jmvubfpw2dnAMdbw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/traverse": "^7.28.6",
        "@babel/types": "^7.28.6"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/helper-module-transforms": {
      "version": "7.28.6",
      "resolved": "https://registry.npmjs.org/@babel/helper-module-transforms/-/helper-module-transforms-7.28.6.tgz",
      "integrity": "sha512-67oXFAYr2cDLDVGLXTEABjdBJZ6drElUSI7WKp70NrpyISso3plG9SAGEF6y7zbha/wOzUByWWTJvEDVNIUGcA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/helper-module-imports": "^7.28.6",
        "@babel/helper-validator-identifier": "^7.28.5",
        "@babel/traverse": "^7.28.6"
      },
      "engines": {
        "node": ">=6.9.0"
      },
      "peerDependencies": {
        "@babel/core": "^7.0.0"
      }
    },
    "node_modules/@babel/helper-string-parser": {
      "version": "7.27.1",
      "resolved": "https://registry.npmjs.org/@babel/helper-string-parser/-/helper-string-parser-7.27.1.tgz",
      "integrity": "sha512-qMlSxKbpRlAridDExk92nSobyDdpPijUq2DW6oDnUqd0iOGxmQjyqhMIihI9+zv4LPyZdRje2cavWPbCbWm3eA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/helper-validator-identifier": {
      "version": "7.28.5",
      "resolved": "https://registry.npmjs.org/@babel/helper-validator-identifier/-/helper-validator-identifier-7.28.5.tgz",
      "integrity": "sha512-qSs4ifwzKJSV39ucNjsvc6WVHs6b7S03sOh2OcHF9UHfVPqWWALUsNUVzhSBiItjRZoLHx7nIarVjqKVusUZ1Q==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/helper-validator-option": {
      "version": "7.27.1",
      "resolved": "https://registry.npmjs.org/@babel/helper-validator-option/-/helper-validator-option-7.27.1.tgz",
      "integrity": "sha512-YvjJow9FxbhFFKDSuFnVCe2WxXk1zWc22fFePVNEaWJEu8IrZVlda6N0uHwzZrUM1il7NC9Mlp4MaJYbYd9JSg==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/helpers": {
      "version": "7.29.2",
      "resolved": "https://registry.npmjs.org/@babel/helpers/-/helpers-7.29.2.tgz",
      "integrity": "sha512-HoGuUs4sCZNezVEKdVcwqmZN8GoHirLUcLaYVNBK2J0DadGtdcqgr3BCbvH8+XUo4NGjNl3VOtSjEKNzqfFgKw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/template": "^7.28.6",
        "@babel/types": "^7.29.0"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/parser": {
      "version": "7.29.2",
      "resolved": "https://registry.npmjs.org/@babel/parser/-/parser-7.29.2.tgz",
      "integrity": "sha512-4GgRzy/+fsBa72/RZVJmGKPmZu9Byn8o4MoLpmNe1m8ZfYnz5emHLQz3U4gLud6Zwl0RZIcgiLD7Uq7ySFuDLA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/types": "^7.29.0"
      },
      "bin": {
        "parser": "bin/babel-parser.js"
      },
      "engines": {
        "node": ">=6.0.0"
      }
    },
    "node_modules/@babel/template": {
      "version": "7.28.6",
      "resolved": "https://registry.npmjs.org/@babel/template/-/template-7.28.6.tgz",
      "integrity": "sha512-YA6Ma2KsCdGb+WC6UpBVFJGXL58MDA6oyONbjyF/+5sBgxY/dwkhLogbMT2GXXyU84/IhRw/2D1Os1B/giz+BQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/code-frame": "^7.28.6",
        "@babel/parser": "^7.28.6",
        "@babel/types": "^7.28.6"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/traverse": {
      "version": "7.29.0",
      "resolved": "https://registry.npmjs.org/@babel/traverse/-/traverse-7.29.0.tgz",
      "integrity": "sha512-4HPiQr0X7+waHfyXPZpWPfWL/J7dcN1mx9gL6WdQVMbPnF3+ZhSMs8tCxN7oHddJE9fhNE7+lxdnlyemKfJRuA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/code-frame": "^7.29.0",
        "@babel/generator": "^7.29.0",
        "@babel/helper-globals": "^7.28.0",
        "@babel/parser": "^7.29.0",
        "@babel/template": "^7.28.6",
        "@babel/types": "^7.29.0",
        "debug": "^4.3.1"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@babel/types": {
      "version": "7.29.0",
      "resolved": "https://registry.npmjs.org/@babel/types/-/types-7.29.0.tgz",
      "integrity": "sha512-LwdZHpScM4Qz8Xw2iKSzS+cfglZzJGvofQICy7W7v4caru4EaAmyUuO6BGrbyQ2mYV11W0U8j5mBhd14dd3B0A==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/helper-string-parser": "^7.27.1",
        "@babel/helper-validator-identifier": "^7.28.5"
      },
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/@emnapi/wasi-threads": {
      "version": "1.2.1",
      "resolved": "https://registry.npmjs.org/@emnapi/wasi-threads/-/wasi-threads-1.2.1.tgz",
      "integrity": "sha512-uTII7OYF+/Mes/MrcIOYp5yOtSMLBWSIoLPpcgwipoiKbli6k322tcoFsxoIIxPDqW01SQGAgko4EzZi2BNv2w==",
      "dev": true,
      "license": "MIT",
      "optional": true,
      "dependencies": {
        "tslib": "^2.4.0"
      }
    },
    "node_modules/@eslint-community/eslint-utils": {
      "version": "4.9.1",
      "resolved": "https://registry.npmjs.org/@eslint-community/eslint-utils/-/eslint-utils-4.9.1.tgz",
      "integrity": "sha512-phrYmNiYppR7znFEdqgfWHXR6NCkZEK7hwWDHZUjit/2/U0r6XvkDl0SYnoM51Hq7FhCGdLDT6zxCCOY1hexsQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "eslint-visitor-keys": "^3.4.3"
      },
      "engines": {
        "node": "^12.22.0 || ^14.17.0 || >=16.0.0"
      },
      "funding": {
        "url": "https://opencollective.com/eslint"
      },
      "peerDependencies": {
        "eslint": "^6.0.0 || ^7.0.0 || >=8.0.0"
      }
    },
    "node_modules/@eslint-community/eslint-utils/node_modules/eslint-visitor-keys": {
      "version": "3.4.3",
      "resolved": "https://registry.npmjs.org/eslint-visitor-keys/-/eslint-visitor-keys-3.4.3.tgz",
      "integrity": "sha512-wpc+LXeiyiisxPlEkUzU6svyS1frIO3Mgxj1fdy7Pm8Ygzguax2N3Fa/D/ag1WqbOprdI+uY6wMUl8/a2G+iag==",
      "dev": true,
      "license": "Apache-2.0",
      "engines": {
        "node": "^12.22.0 || ^14.17.0 || >=16.0.0"
      },
      "funding": {
        "url": "https://opencollective.com/eslint"
      }
    },
    "node_modules/@eslint-community/regexpp": {
      "version": "4.12.2",
      "resolved": "https://registry.npmjs.org/@eslint-community/regexpp/-/regexpp-4.12.2.tgz",
      "integrity": "sha512-EriSTlt5OC9/7SXkRSCAhfSxxoSUgBm33OH+IkwbdpgoqsSsUg7y3uh+IICI/Qg4BBWr3U2i39RpmycbxMq4ew==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": "^12.0.0 || ^14.0.0 || >=16.0.0"
      }
    },
    "node_modules/@eslint/config-array": {
      "version": "0.23.5",
      "resolved": "https://registry.npmjs.org/@eslint/config-array/-/config-array-0.23.5.tgz",
      "integrity": "sha512-Y3kKLvC1dvTOT+oGlqNQ1XLqK6D1HU2YXPc52NmAlJZbMMWDzGYXMiPRJ8TYD39muD/OTjlZmNJ4ib7dvSrMBA==",
      "dev": true,
      "license": "Apache-2.0",
      "dependencies": {
        "@eslint/object-schema": "^3.0.5",
        "debug": "^4.3.1",
        "minimatch": "^10.2.4"
      },
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      }
    },
    "node_modules/@eslint/config-helpers": {
      "version": "0.5.5",
      "resolved": "https://registry.npmjs.org/@eslint/config-helpers/-/config-helpers-0.5.5.tgz",
      "integrity": "sha512-eIJYKTCECbP/nsKaaruF6LW967mtbQbsw4JTtSVkUQc9MneSkbrgPJAbKl9nWr0ZeowV8BfsarBmPpBzGelA2w==",
      "dev": true,
      "license": "Apache-2.0",
      "dependencies": {
        "@eslint/core": "^1.2.1"
      },
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      }
    },
    "node_modules/@eslint/core": {
      "version": "1.2.1",
      "resolved": "https://registry.npmjs.org/@eslint/core/-/core-1.2.1.tgz",
      "integrity": "sha512-MwcE1P+AZ4C6DWlpin/OmOA54mmIZ/+xZuJiQd4SyB29oAJjN30UW9wkKNptW2ctp4cEsvhlLY/CsQ1uoHDloQ==",
      "dev": true,
      "license": "Apache-2.0",
      "dependencies": {
        "@types/json-schema": "^7.0.15"
      },
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      }
    },
    "node_modules/@eslint/js": {
      "version": "10.0.1",
      "resolved": "https://registry.npmjs.org/@eslint/js/-/js-10.0.1.tgz",
      "integrity": "sha512-zeR9k5pd4gxjZ0abRoIaxdc7I3nDktoXZk2qOv9gCNWx3mVwEn32VRhyLaRsDiJjTs0xq/T8mfPtyuXu7GWBcA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      },
      "funding": {
        "url": "https://eslint.org/donate"
      },
      "peerDependencies": {
        "eslint": "^10.0.0"
      },
      "peerDependenciesMeta": {
        "eslint": {
          "optional": true
        }
      }
    },
    "node_modules/@eslint/object-schema": {
      "version": "3.0.5",
      "resolved": "https://registry.npmjs.org/@eslint/object-schema/-/object-schema-3.0.5.tgz",
      "integrity": "sha512-vqTaUEgxzm+YDSdElad6PiRoX4t8VGDjCtt05zn4nU810UIx/uNEV7/lZJ6KwFThKZOzOxzXy48da+No7HZaMw==",
      "dev": true,
      "license": "Apache-2.0",
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      }
    },
    "node_modules/@eslint/plugin-kit": {
      "version": "0.7.1",
      "resolved": "https://registry.npmjs.org/@eslint/plugin-kit/-/plugin-kit-0.7.1.tgz",
      "integrity": "sha512-rZAP3aVgB9ds9KOeUSL+zZ21hPmo8dh6fnIFwRQj5EAZl9gzR7wxYbYXYysAM8CTqGmUGyp2S4kUdV17MnGuWQ==",
      "dev": true,
      "license": "Apache-2.0",
      "dependencies": {
        "@eslint/core": "^1.2.1",
        "levn": "^0.4.1"
      },
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      }
    },
    "node_modules/@humanfs/core": {
      "version": "0.19.2",
      "resolved": "https://registry.npmjs.org/@humanfs/core/-/core-0.19.2.tgz",
      "integrity": "sha512-UhXNm+CFMWcbChXywFwkmhqjs3PRCmcSa/hfBgLIb7oQ5HNb1wS0icWsGtSAUNgefHeI+eBrA8I1fxmbHsGdvA==",
      "dev": true,
      "license": "Apache-2.0",
      "dependencies": {
        "@humanfs/types": "^0.15.0"
      },
      "engines": {
        "node": ">=18.18.0"
      }
    },
    "node_modules/@humanfs/node": {
      "version": "0.16.8",
      "resolved": "https://registry.npmjs.org/@humanfs/node/-/node-0.16.8.tgz",
      "integrity": "sha512-gE1eQNZ3R++kTzFUpdGlpmy8kDZD/MLyHqDwqjkVQI0JMdI1D51sy1H958PNXYkM2rAac7e5/CnIKZrHtPh3BQ==",
      "dev": true,
      "license": "Apache-2.0",
      "dependencies": {
        "@humanfs/core": "^0.19.2",
        "@humanfs/types": "^0.15.0",
        "@humanwhocodes/retry": "^0.4.0"
      },
      "engines": {
        "node": ">=18.18.0"
      }
    },
    "node_modules/@humanfs/types": {
      "version": "0.15.0",
      "resolved": "https://registry.npmjs.org/@humanfs/types/-/types-0.15.0.tgz",
      "integrity": "sha512-ZZ1w0aoQkwuUuC7Yf+7sdeaNfqQiiLcSRbfI08oAxqLtpXQr9AIVX7Ay7HLDuiLYAaFPu8oBYNq/QIi9URHJ3Q==",
      "dev": true,
      "license": "Apache-2.0",
      "engines": {
        "node": ">=18.18.0"
      }
    },
    "node_modules/@humanwhocodes/module-importer": {
      "version": "1.0.1",
      "resolved": "https://registry.npmjs.org/@humanwhocodes/module-importer/-/module-importer-1.0.1.tgz",
      "integrity": "sha512-bxveV4V8v5Yb4ncFTT3rPSgZBOpCkjfK0y4oVVVJwIuDVBRMDXrPyXRL988i5ap9m9bnyEEjWfm5WkBmtffLfA==",
      "dev": true,
      "license": "Apache-2.0",
      "engines": {
        "node": ">=12.22"
      },
      "funding": {
        "type": "github",
        "url": "https://github.com/sponsors/nzakas"
      }
    },
    "node_modules/@humanwhocodes/retry": {
      "version": "0.4.3",
      "resolved": "https://registry.npmjs.org/@humanwhocodes/retry/-/retry-0.4.3.tgz",
      "integrity": "sha512-bV0Tgo9K4hfPCek+aMAn81RppFKv2ySDQeMoSZuvTASywNTnVJCArCZE2FWqpvIatKu7VMRLWlR1EazvVhDyhQ==",
      "dev": true,
      "license": "Apache-2.0",
      "engines": {
        "node": ">=18.18"
      },
      "funding": {
        "type": "github",
        "url": "https://github.com/sponsors/nzakas"
      }
    },
    "node_modules/@jridgewell/gen-mapping": {
      "version": "0.3.13",
      "resolved": "https://registry.npmjs.org/@jridgewell/gen-mapping/-/gen-mapping-0.3.13.tgz",
      "integrity": "sha512-2kkt/7niJ6MgEPxF0bYdQ6etZaA+fQvDcLKckhy1yIQOzaoKjBBjSj63/aLVjYE3qhRt5dvM+uUyfCg6UKCBbA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@jridgewell/sourcemap-codec": "^1.5.0",
        "@jridgewell/trace-mapping": "^0.3.24"
      }
    },
    "node_modules/@jridgewell/remapping": {
      "version": "2.3.5",
      "resolved": "https://registry.npmjs.org/@jridgewell/remapping/-/remapping-2.3.5.tgz",
      "integrity": "sha512-LI9u/+laYG4Ds1TDKSJW2YPrIlcVYOwi2fUC6xB43lueCjgxV4lffOCZCtYFiH6TNOX+tQKXx97T4IKHbhyHEQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@jridgewell/gen-mapping": "^0.3.5",
        "@jridgewell/trace-mapping": "^0.3.24"
      }
    },
    "node_modules/@jridgewell/resolve-uri": {
      "version": "3.1.2",
      "resolved": "https://registry.npmjs.org/@jridgewell/resolve-uri/-/resolve-uri-3.1.2.tgz",
      "integrity": "sha512-bRISgCIjP20/tbWSPWMEi54QVPRZExkuD9lJL+UIxUKtwVJA8wW1Trb1jMs1RFXo1CBTNZ/5hpC9QvmKWdopKw==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6.0.0"
      }
    },
    "node_modules/@jridgewell/sourcemap-codec": {
      "version": "1.5.5",
      "resolved": "https://registry.npmjs.org/@jridgewell/sourcemap-codec/-/sourcemap-codec-1.5.5.tgz",
      "integrity": "sha512-cYQ9310grqxueWbl+WuIUIaiUaDcj7WOq5fVhEljNVgRfOUhY9fy2zTvfoqWsnebh8Sl70VScFbICvJnLKB0Og==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/@jridgewell/trace-mapping": {
      "version": "0.3.31",
      "resolved": "https://registry.npmjs.org/@jridgewell/trace-mapping/-/trace-mapping-0.3.31.tgz",
      "integrity": "sha512-zzNR+SdQSDJzc8joaeP8QQoCQr8NuYx2dIIytl1QeBEZHJ9uW6hebsrYgbz8hJwUQao3TWCMtmfV8Nu1twOLAw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@jridgewell/resolve-uri": "^3.1.0",
        "@jridgewell/sourcemap-codec": "^1.4.14"
      }
    },
    "node_modules/@napi-rs/wasm-runtime": {
      "version": "1.1.4",
      "resolved": "https://registry.npmjs.org/@napi-rs/wasm-runtime/-/wasm-runtime-1.1.4.tgz",
      "integrity": "sha512-3NQNNgA1YSlJb/kMH1ildASP9HW7/7kYnRI2szWJaofaS1hWmbGI4H+d3+22aGzXXN9IJ+n+GiFVcGipJP18ow==",
      "dev": true,
      "license": "MIT",
      "optional": true,
      "dependencies": {
        "@tybys/wasm-util": "^0.10.1"
      },
      "funding": {
        "type": "github",
        "url": "https://github.com/sponsors/Brooooooklyn"
      },
      "peerDependencies": {
        "@emnapi/core": "^1.7.1",
        "@emnapi/runtime": "^1.7.1"
      }
    },
    "node_modules/@oxc-project/types": {
      "version": "0.127.0",
      "resolved": "https://registry.npmjs.org/@oxc-project/types/-/types-0.127.0.tgz",
      "integrity": "sha512-aIYXQBo4lCbO4z0R3FHeucQHpF46l2LbMdxRvqvuRuW2OxdnSkcng5B8+K12spgLDj93rtN3+J2Vac/TIO+ciQ==",
      "dev": true,
      "license": "MIT",
      "funding": {
        "url": "https://github.com/sponsors/Boshen"
      }
    },
    "node_modules/@rolldown/binding-android-arm64": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-android-arm64/-/binding-android-arm64-1.0.0-rc.17.tgz",
      "integrity": "sha512-s70pVGhw4zqGeFnXWvAzJDlvxhlRollagdCCKRgOsgUOH3N1l0LIxf83AtGzmb5SiVM4Hjl5HyarMRfdfj3DaQ==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "android"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-darwin-arm64": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-darwin-arm64/-/binding-darwin-arm64-1.0.0-rc.17.tgz",
      "integrity": "sha512-4ksWc9n0mhlZpZ9PMZgTGjeOPRu8MB1Z3Tz0Mo02eWfWCHMW1zN82Qz/pL/rC+yQa+8ZnutMF0JjJe7PjwasYw==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "darwin"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-darwin-x64": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-darwin-x64/-/binding-darwin-x64-1.0.0-rc.17.tgz",
      "integrity": "sha512-SUSDOI6WwUVNcWxd02QEBjLdY1VPHvlEkw6T/8nYG322iYWCTxRb1vzk4E+mWWYehTp7ERibq54LSJGjmouOsw==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "darwin"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-freebsd-x64": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-freebsd-x64/-/binding-freebsd-x64-1.0.0-rc.17.tgz",
      "integrity": "sha512-hwnz3nw9dbJ05EDO/PvcjaaewqqDy7Y1rn1UO81l8iIK1GjenME75dl16ajbvSSMfv66WXSRCYKIqfgq2KCfxw==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "freebsd"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-linux-arm-gnueabihf": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-linux-arm-gnueabihf/-/binding-linux-arm-gnueabihf-1.0.0-rc.17.tgz",
      "integrity": "sha512-IS+W7epTcwANmFSQFrS1SivEXHtl1JtuQA9wlxrZTcNi6mx+FDOYrakGevvvTwgj2JvWiK8B29/qD9BELZPyXQ==",
      "cpu": [
        "arm"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-linux-arm64-gnu": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-linux-arm64-gnu/-/binding-linux-arm64-gnu-1.0.0-rc.17.tgz",
      "integrity": "sha512-e6usGaHKW5BMNZOymS1UcEYGowQMWcgZ71Z17Sl/h2+ZziNJ1a9n3Zvcz6LdRyIW5572wBCTH/Z+bKuZouGk9Q==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-linux-arm64-musl": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-linux-arm64-musl/-/binding-linux-arm64-musl-1.0.0-rc.17.tgz",
      "integrity": "sha512-b/CgbwAJpmrRLp02RPfhbudf5tZnN9nsPWK82znefso832etkem8H7FSZwxrOI9djcdTP7U6YfNhbRnh7djErg==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-linux-ppc64-gnu": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-linux-ppc64-gnu/-/binding-linux-ppc64-gnu-1.0.0-rc.17.tgz",
      "integrity": "sha512-4EII1iNGRUN5WwGbF/kOh/EIkoDN9HsupgLQoXfY+D1oyJm7/F4t5PYU5n8SWZgG0FEwakyM8pGgwcBYruGTlA==",
      "cpu": [
        "ppc64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-linux-s390x-gnu": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-linux-s390x-gnu/-/binding-linux-s390x-gnu-1.0.0-rc.17.tgz",
      "integrity": "sha512-AH8oq3XqQo4IibpVXvPeLDI5pzkpYn0WiZAfT05kFzoJ6tQNzwRdDYQ45M8I/gslbodRZwW8uxLhbSBbkv96rA==",
      "cpu": [
        "s390x"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-linux-x64-gnu": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-linux-x64-gnu/-/binding-linux-x64-gnu-1.0.0-rc.17.tgz",
      "integrity": "sha512-cLnjV3xfo7KslbU41Z7z8BH/E1y5mzUYzAqih1d1MDaIGZRCMqTijqLv76/P7fyHuvUcfGsIpqCdddbxLLK9rA==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-linux-x64-musl": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-linux-x64-musl/-/binding-linux-x64-musl-1.0.0-rc.17.tgz",
      "integrity": "sha512-0phclDw1spsL7dUB37sIARuis2tAgomCJXAHZlpt8PXZ4Ba0dRP1e+66lsRqrfhISeN9bEGNjQs+T/Fbd7oYGw==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-openharmony-arm64": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-openharmony-arm64/-/binding-openharmony-arm64-1.0.0-rc.17.tgz",
      "integrity": "sha512-0ag/hEgXOwgw4t8QyQvUCxvEg+V0KBcA6YuOx9g0r02MprutRF5dyljgm3EmR02O292UX7UeS6HzWHAl6KgyhA==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "openharmony"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-wasm32-wasi": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-wasm32-wasi/-/binding-wasm32-wasi-1.0.0-rc.17.tgz",
      "integrity": "sha512-LEXei6vo0E5wTGwpkJ4KoT3OZJRnglwldt5ziLzOlc6qqb55z4tWNq2A+PFqCJuvWWdP53CVhG1Z9NtToDPJrA==",
      "cpu": [
        "wasm32"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "dependencies": {
        "@emnapi/core": "1.10.0",
        "@emnapi/runtime": "1.10.0",
        "@napi-rs/wasm-runtime": "^1.1.4"
      },
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-win32-arm64-msvc": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-win32-arm64-msvc/-/binding-win32-arm64-msvc-1.0.0-rc.17.tgz",
      "integrity": "sha512-gUmyzBl3SPMa6hrqFUth9sVfcLBlYsbMzBx5PlexMroZStgzGqlZ26pYG89rBb45Mnia+oil6YAIFeEWGWhoZA==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "win32"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/binding-win32-x64-msvc": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/binding-win32-x64-msvc/-/binding-win32-x64-msvc-1.0.0-rc.17.tgz",
      "integrity": "sha512-3hkiolcUAvPB9FLb3UZdfjVVNWherN1f/skkGWJP/fgSQhYUZpSIRr0/I8ZK9TkF3F7kxvJAk0+IcKvPHk9qQg==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "win32"
      ],
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      }
    },
    "node_modules/@rolldown/pluginutils": {
      "version": "1.0.0-rc.7",
      "resolved": "https://registry.npmjs.org/@rolldown/pluginutils/-/pluginutils-1.0.0-rc.7.tgz",
      "integrity": "sha512-qujRfC8sFVInYSPPMLQByRh7zhwkGFS4+tyMQ83srV1qrxL4g8E2tyxVVyxd0+8QeBM1mIk9KbWxkegRr76XzA==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/@tybys/wasm-util": {
      "version": "0.10.1",
      "resolved": "https://registry.npmjs.org/@tybys/wasm-util/-/wasm-util-0.10.1.tgz",
      "integrity": "sha512-9tTaPJLSiejZKx+Bmog4uSubteqTvFrVrURwkmHixBo0G4seD0zUxp98E1DzUBJxLQ3NPwXrGKDiVjwx/DpPsg==",
      "dev": true,
      "license": "MIT",
      "optional": true,
      "dependencies": {
        "tslib": "^2.4.0"
      }
    },
    "node_modules/@types/esrecurse": {
      "version": "4.3.1",
      "resolved": "https://registry.npmjs.org/@types/esrecurse/-/esrecurse-4.3.1.tgz",
      "integrity": "sha512-xJBAbDifo5hpffDBuHl0Y8ywswbiAp/Wi7Y/GtAgSlZyIABppyurxVueOPE8LUQOxdlgi6Zqce7uoEpqNTeiUw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/@types/estree": {
      "version": "1.0.8",
      "resolved": "https://registry.npmjs.org/@types/estree/-/estree-1.0.8.tgz",
      "integrity": "sha512-dWHzHa2WqEXI/O1E9OjrocMTKJl2mSrEolh1Iomrv6U+JuNwaHXsXx9bLu5gG7BUWFIN0skIQJQ/L1rIex4X6w==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/@types/json-schema": {
      "version": "7.0.15",
      "resolved": "https://registry.npmjs.org/@types/json-schema/-/json-schema-7.0.15.tgz",
      "integrity": "sha512-5+fP8P8MFNC+AyZCDxrB2pkZFPGzqQWUzpSeuuVLvm8VMcorNYavBqoFcxK8bQz4Qsbn4oUEEem4wDLfcysGHA==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/@types/node": {
      "version": "24.12.2",
      "resolved": "https://registry.npmjs.org/@types/node/-/node-24.12.2.tgz",
      "integrity": "sha512-A1sre26ke7HDIuY/M23nd9gfB+nrmhtYyMINbjI1zHJxYteKR6qSMX56FsmjMcDb3SMcjJg5BiRRgOCC/yBD0g==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "dependencies": {
        "undici-types": "~7.16.0"
      }
    },
    "node_modules/@types/react": {
      "version": "19.2.14",
      "resolved": "https://registry.npmjs.org/@types/react/-/react-19.2.14.tgz",
      "integrity": "sha512-ilcTH/UniCkMdtexkoCN0bI7pMcJDvmQFPvuPvmEaYA/NSfFTAgdUSLAoVjaRJm7+6PvcM+q1zYOwS4wTYMF9w==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "dependencies": {
        "csstype": "^3.2.2"
      }
    },
    "node_modules/@types/react-dom": {
      "version": "19.2.3",
      "resolved": "https://registry.npmjs.org/@types/react-dom/-/react-dom-19.2.3.tgz",
      "integrity": "sha512-jp2L/eY6fn+KgVVQAOqYItbF0VY/YApe5Mz2F0aykSO8gx31bYCZyvSeYxCHKvzHG5eZjc+zyaS5BrBWya2+kQ==",
      "dev": true,
      "license": "MIT",
      "peerDependencies": {
        "@types/react": "^19.2.0"
      }
    },
    "node_modules/@typescript-eslint/eslint-plugin": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/eslint-plugin/-/eslint-plugin-8.59.1.tgz",
      "integrity": "sha512-BOziFIfE+6osHO9FoJG4zjoHUcvI7fTNBSpdAwrNH0/TLvzjsk2oo8XSSOT2HhqUyhZPfHv4UOffoJ9oEEQ7Ag==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@eslint-community/regexpp": "^4.12.2",
        "@typescript-eslint/scope-manager": "8.59.1",
        "@typescript-eslint/type-utils": "8.59.1",
        "@typescript-eslint/utils": "8.59.1",
        "@typescript-eslint/visitor-keys": "8.59.1",
        "ignore": "^7.0.5",
        "natural-compare": "^1.4.0",
        "ts-api-utils": "^2.5.0"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "@typescript-eslint/parser": "^8.59.1",
        "eslint": "^8.57.0 || ^9.0.0 || ^10.0.0",
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/@typescript-eslint/eslint-plugin/node_modules/ignore": {
      "version": "7.0.5",
      "resolved": "https://registry.npmjs.org/ignore/-/ignore-7.0.5.tgz",
      "integrity": "sha512-Hs59xBNfUIunMFgWAbGX5cq6893IbWg4KnrjbYwX3tx0ztorVgTDA6B2sxf8ejHJ4wz8BqGUMYlnzNBer5NvGg==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">= 4"
      }
    },
    "node_modules/@typescript-eslint/parser": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/parser/-/parser-8.59.1.tgz",
      "integrity": "sha512-HDQH9O/47Dxi1ceDhBXdaldtf/WV9yRYMjbjCuNk3qnaTD564qwv61Y7+gTxwxRKzSrgO5uhtw584igXVuuZkA==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "dependencies": {
        "@typescript-eslint/scope-manager": "8.59.1",
        "@typescript-eslint/types": "8.59.1",
        "@typescript-eslint/typescript-estree": "8.59.1",
        "@typescript-eslint/visitor-keys": "8.59.1",
        "debug": "^4.4.3"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "eslint": "^8.57.0 || ^9.0.0 || ^10.0.0",
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/@typescript-eslint/project-service": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/project-service/-/project-service-8.59.1.tgz",
      "integrity": "sha512-+MuHQlHiEr00Of/IQbE/MmEoi44znZHbR/Pz7Opq4HryUOlRi+/44dro9Ycy8Fyo+/024IWtw8m4JUMCGTYxDg==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@typescript-eslint/tsconfig-utils": "^8.59.1",
        "@typescript-eslint/types": "^8.59.1",
        "debug": "^4.4.3"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/@typescript-eslint/scope-manager": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/scope-manager/-/scope-manager-8.59.1.tgz",
      "integrity": "sha512-LwuHQI4pDOYVKvmH2dkaJo6YZCSgouVgnS/z7yBPKBMvgtBvyLqiLy9Z6b7+m/TRcX1NFYUqZetI5Y+aT4GEfg==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@typescript-eslint/types": "8.59.1",
        "@typescript-eslint/visitor-keys": "8.59.1"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      }
    },
    "node_modules/@typescript-eslint/tsconfig-utils": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/tsconfig-utils/-/tsconfig-utils-8.59.1.tgz",
      "integrity": "sha512-/0nEyPbX7gRsk0Uwfe4ALwwgxuA66d/l2mhRDNlAvaj4U3juhUtJNq0DsY8M2AYwwb9rEq2hrC3IcIcEt++iJA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/@typescript-eslint/type-utils": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/type-utils/-/type-utils-8.59.1.tgz",
      "integrity": "sha512-klWPBR2ciQHS3f++ug/mVnWKPjBUo7icEL3FAO1lhAR1Z1i5NQYZ1EannMSRYcq5qCv5wNALlXr6fksRHyYl7w==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@typescript-eslint/types": "8.59.1",
        "@typescript-eslint/typescript-estree": "8.59.1",
        "@typescript-eslint/utils": "8.59.1",
        "debug": "^4.4.3",
        "ts-api-utils": "^2.5.0"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "eslint": "^8.57.0 || ^9.0.0 || ^10.0.0",
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/@typescript-eslint/types": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/types/-/types-8.59.1.tgz",
      "integrity": "sha512-ZDCjgccSdYPw5Bxh+my4Z0lJU96ZDN7jbBzvmEn0FZx3RtU1C7VWl6NbDx94bwY3V5YsgwRzJPOgeY2Q/nLG8A==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      }
    },
    "node_modules/@typescript-eslint/typescript-estree": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/typescript-estree/-/typescript-estree-8.59.1.tgz",
      "integrity": "sha512-OUd+vJS05sSkOip+BkZ/2NS8RMxrAAJemsC6vU3kmfLyeaJT0TftHkV9mcx2107MmsBVXXexhVu4F0TZXyMl4g==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@typescript-eslint/project-service": "8.59.1",
        "@typescript-eslint/tsconfig-utils": "8.59.1",
        "@typescript-eslint/types": "8.59.1",
        "@typescript-eslint/visitor-keys": "8.59.1",
        "debug": "^4.4.3",
        "minimatch": "^10.2.2",
        "semver": "^7.7.3",
        "tinyglobby": "^0.2.15",
        "ts-api-utils": "^2.5.0"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/@typescript-eslint/typescript-estree/node_modules/semver": {
      "version": "7.7.4",
      "resolved": "https://registry.npmjs.org/semver/-/semver-7.7.4.tgz",
      "integrity": "sha512-vFKC2IEtQnVhpT78h1Yp8wzwrf8CM+MzKMHGJZfBtzhZNycRFnXsHk6E5TxIkkMsgNS7mdX3AGB7x2QM2di4lA==",
      "dev": true,
      "license": "ISC",
      "bin": {
        "semver": "bin/semver.js"
      },
      "engines": {
        "node": ">=10"
      }
    },
    "node_modules/@typescript-eslint/utils": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/utils/-/utils-8.59.1.tgz",
      "integrity": "sha512-3pIeoXhCeYH9FSCBI8P3iNwJlGuzPlYKkTlen2O9T1DSeeg8UG8jstq6BLk+Mda0qup7mgk4z4XL4OzRaxZ8LA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@eslint-community/eslint-utils": "^4.9.1",
        "@typescript-eslint/scope-manager": "8.59.1",
        "@typescript-eslint/types": "8.59.1",
        "@typescript-eslint/typescript-estree": "8.59.1"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "eslint": "^8.57.0 || ^9.0.0 || ^10.0.0",
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/@typescript-eslint/visitor-keys": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/@typescript-eslint/visitor-keys/-/visitor-keys-8.59.1.tgz",
      "integrity": "sha512-LdDNl6C5iJExcM0Yh0PwAIBb9PrSiCsWamF/JyEZawm3kFDnRoaq3LGE4bpyRao/fWeGKKyw7icx0YxrLFC5Cg==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@typescript-eslint/types": "8.59.1",
        "eslint-visitor-keys": "^5.0.0"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      }
    },
    "node_modules/@vitejs/plugin-react": {
      "version": "6.0.1",
      "resolved": "https://registry.npmjs.org/@vitejs/plugin-react/-/plugin-react-6.0.1.tgz",
      "integrity": "sha512-l9X/E3cDb+xY3SWzlG1MOGt2usfEHGMNIaegaUGFsLkb3RCn/k8/TOXBcab+OndDI4TBtktT8/9BwwW8Vi9KUQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@rolldown/pluginutils": "1.0.0-rc.7"
      },
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      },
      "peerDependencies": {
        "@rolldown/plugin-babel": "^0.1.7 || ^0.2.0",
        "babel-plugin-react-compiler": "^1.0.0",
        "vite": "^8.0.0"
      },
      "peerDependenciesMeta": {
        "@rolldown/plugin-babel": {
          "optional": true
        },
        "babel-plugin-react-compiler": {
          "optional": true
        }
      }
    },
    "node_modules/acorn": {
      "version": "8.16.0",
      "resolved": "https://registry.npmjs.org/acorn/-/acorn-8.16.0.tgz",
      "integrity": "sha512-UVJyE9MttOsBQIDKw1skb9nAwQuR5wuGD3+82K6JgJlm/Y+KI92oNsMNGZCYdDsVtRHSak0pcV5Dno5+4jh9sw==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "bin": {
        "acorn": "bin/acorn"
      },
      "engines": {
        "node": ">=0.4.0"
      }
    },
    "node_modules/acorn-jsx": {
      "version": "5.3.2",
      "resolved": "https://registry.npmjs.org/acorn-jsx/-/acorn-jsx-5.3.2.tgz",
      "integrity": "sha512-rq9s+JNhf0IChjtDXxllJ7g41oZk5SlXtp0LHwyA5cejwn7vKmKp4pPri6YEePv2PU65sAsegbXtIinmDFDXgQ==",
      "dev": true,
      "license": "MIT",
      "peerDependencies": {
        "acorn": "^6.0.0 || ^7.0.0 || ^8.0.0"
      }
    },
    "node_modules/ajv": {
      "version": "6.15.0",
      "resolved": "https://registry.npmjs.org/ajv/-/ajv-6.15.0.tgz",
      "integrity": "sha512-fgFx7Hfoq60ytK2c7DhnF8jIvzYgOMxfugjLOSMHjLIPgenqa7S7oaagATUq99mV6IYvN2tRmC0wnTYX6iPbMw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "fast-deep-equal": "^3.1.1",
        "fast-json-stable-stringify": "^2.0.0",
        "json-schema-traverse": "^0.4.1",
        "uri-js": "^4.2.2"
      },
      "funding": {
        "type": "github",
        "url": "https://github.com/sponsors/epoberezkin"
      }
    },
    "node_modules/balanced-match": {
      "version": "4.0.4",
      "resolved": "https://registry.npmjs.org/balanced-match/-/balanced-match-4.0.4.tgz",
      "integrity": "sha512-BLrgEcRTwX2o6gGxGOCNyMvGSp35YofuYzw9h1IMTRmKqttAZZVU67bdb9Pr2vUHA8+j3i2tJfjO6C6+4myGTA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": "18 || 20 || >=22"
      }
    },
    "node_modules/baseline-browser-mapping": {
      "version": "2.10.24",
      "resolved": "https://registry.npmjs.org/baseline-browser-mapping/-/baseline-browser-mapping-2.10.24.tgz",
      "integrity": "sha512-I2NkZOOrj2XuguvWCK6OVh9GavsNjZjK908Rq3mIBK25+GD8vPX5w2WdxVqnQ7xx3SrZJiCiZFu+/Oz50oSYSA==",
      "dev": true,
      "license": "Apache-2.0",
      "bin": {
        "baseline-browser-mapping": "dist/cli.cjs"
      },
      "engines": {
        "node": ">=6.0.0"
      }
    },
    "node_modules/brace-expansion": {
      "version": "5.0.5",
      "resolved": "https://registry.npmjs.org/brace-expansion/-/brace-expansion-5.0.5.tgz",
      "integrity": "sha512-VZznLgtwhn+Mact9tfiwx64fA9erHH/MCXEUfB/0bX/6Fz6ny5EGTXYltMocqg4xFAQZtnO3DHWWXi8RiuN7cQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "balanced-match": "^4.0.2"
      },
      "engines": {
        "node": "18 || 20 || >=22"
      }
    },
    "node_modules/browserslist": {
      "version": "4.28.2",
      "resolved": "https://registry.npmjs.org/browserslist/-/browserslist-4.28.2.tgz",
      "integrity": "sha512-48xSriZYYg+8qXna9kwqjIVzuQxi+KYWp2+5nCYnYKPTr0LvD89Jqk2Or5ogxz0NUMfIjhh2lIUX/LyX9B4oIg==",
      "dev": true,
      "funding": [
        {
          "type": "opencollective",
          "url": "https://opencollective.com/browserslist"
        },
        {
          "type": "tidelift",
          "url": "https://tidelift.com/funding/github/npm/browserslist"
        },
        {
          "type": "github",
          "url": "https://github.com/sponsors/ai"
        }
      ],
      "license": "MIT",
      "peer": true,
      "dependencies": {
        "baseline-browser-mapping": "^2.10.12",
        "caniuse-lite": "^1.0.30001782",
        "electron-to-chromium": "^1.5.328",
        "node-releases": "^2.0.36",
        "update-browserslist-db": "^1.2.3"
      },
      "bin": {
        "browserslist": "cli.js"
      },
      "engines": {
        "node": "^6 || ^7 || ^8 || ^9 || ^10 || ^11 || ^12 || >=13.7"
      }
    },
    "node_modules/caniuse-lite": {
      "version": "1.0.30001791",
      "resolved": "https://registry.npmjs.org/caniuse-lite/-/caniuse-lite-1.0.30001791.tgz",
      "integrity": "sha512-yk0l/YSrOnFZk3UROpDLQD9+kC1l4meK/wed583AXrzoarMGJcbRi2Q4RaUYbKxYAsZ8sWmaSa/DsLmdBeI1vQ==",
      "dev": true,
      "funding": [
        {
          "type": "opencollective",
          "url": "https://opencollective.com/browserslist"
        },
        {
          "type": "tidelift",
          "url": "https://tidelift.com/funding/github/npm/caniuse-lite"
        },
        {
          "type": "github",
          "url": "https://github.com/sponsors/ai"
        }
      ],
      "license": "CC-BY-4.0"
    },
    "node_modules/convert-source-map": {
      "version": "2.0.0",
      "resolved": "https://registry.npmjs.org/convert-source-map/-/convert-source-map-2.0.0.tgz",
      "integrity": "sha512-Kvp459HrV2FEJ1CAsi1Ku+MY3kasH19TFykTz2xWmMeq6bk2NU3XXvfJ+Q61m0xktWwt+1HSYf3JZsTms3aRJg==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/cross-spawn": {
      "version": "7.0.6",
      "resolved": "https://registry.npmjs.org/cross-spawn/-/cross-spawn-7.0.6.tgz",
      "integrity": "sha512-uV2QOWP2nWzsy2aMp8aRibhi9dlzF5Hgh5SHaB9OiTGEyDTiJJyx0uy51QXdyWbtAHNua4XJzUKca3OzKUd3vA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "path-key": "^3.1.0",
        "shebang-command": "^2.0.0",
        "which": "^2.0.1"
      },
      "engines": {
        "node": ">= 8"
      }
    },
    "node_modules/csstype": {
      "version": "3.2.3",
      "resolved": "https://registry.npmjs.org/csstype/-/csstype-3.2.3.tgz",
      "integrity": "sha512-z1HGKcYy2xA8AGQfwrn0PAy+PB7X/GSj3UVJW9qKyn43xWa+gl5nXmU4qqLMRzWVLFC8KusUX8T/0kCiOYpAIQ==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/debug": {
      "version": "4.4.3",
      "resolved": "https://registry.npmjs.org/debug/-/debug-4.4.3.tgz",
      "integrity": "sha512-RGwwWnwQvkVfavKVt22FGLw+xYSdzARwm0ru6DhTVA3umU5hZc28V3kO4stgYryrTlLpuvgI9GiijltAjNbcqA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "ms": "^2.1.3"
      },
      "engines": {
        "node": ">=6.0"
      },
      "peerDependenciesMeta": {
        "supports-color": {
          "optional": true
        }
      }
    },
    "node_modules/deep-is": {
      "version": "0.1.4",
      "resolved": "https://registry.npmjs.org/deep-is/-/deep-is-0.1.4.tgz",
      "integrity": "sha512-oIPzksmTg4/MriiaYGO+okXDT7ztn/w3Eptv/+gSIdMdKsJo0u4CfYNFJPy+4SKMuCqGw2wxnA+URMg3t8a/bQ==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/detect-libc": {
      "version": "2.1.2",
      "resolved": "https://registry.npmjs.org/detect-libc/-/detect-libc-2.1.2.tgz",
      "integrity": "sha512-Btj2BOOO83o3WyH59e8MgXsxEQVcarkUOpEYrubB0urwnN10yQ364rsiByU11nZlqWYZm05i/of7io4mzihBtQ==",
      "dev": true,
      "license": "Apache-2.0",
      "engines": {
        "node": ">=8"
      }
    },
    "node_modules/electron-to-chromium": {
      "version": "1.5.345",
      "resolved": "https://registry.npmjs.org/electron-to-chromium/-/electron-to-chromium-1.5.345.tgz",
      "integrity": "sha512-F9JXQGiMrz6yVNPI2qOVPvB9HzjH5cGzhs8oJ6A28V5L/YnzN/0KsuiibqF+F1Fd9qxFzD1BUnYSd8JfULxTwg==",
      "dev": true,
      "license": "ISC"
    },
    "node_modules/escalade": {
      "version": "3.2.0",
      "resolved": "https://registry.npmjs.org/escalade/-/escalade-3.2.0.tgz",
      "integrity": "sha512-WUj2qlxaQtO4g6Pq5c29GTcWGDyd8itL8zTlipgECz3JesAiiOKotd8JU6otB3PACgG6xkJUyVhboMS+bje/jA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6"
      }
    },
    "node_modules/escape-string-regexp": {
      "version": "4.0.0",
      "resolved": "https://registry.npmjs.org/escape-string-regexp/-/escape-string-regexp-4.0.0.tgz",
      "integrity": "sha512-TtpcNJ3XAzx3Gq8sWRzJaVajRs0uVxA2YAkdb1jm2YkPz4G6egUFAyA3n5vtEIZefPk5Wa4UXbKuS5fKkJWdgA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=10"
      },
      "funding": {
        "url": "https://github.com/sponsors/sindresorhus"
      }
    },
    "node_modules/eslint": {
      "version": "10.2.1",
      "resolved": "https://registry.npmjs.org/eslint/-/eslint-10.2.1.tgz",
      "integrity": "sha512-wiyGaKsDgqXvF40P8mDwiUp/KQjE1FdrIEJsM8PZ3XCiniTMXS3OHWWUe5FI5agoCnr8x4xPrTDZuxsBlNHl+Q==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "dependencies": {
        "@eslint-community/eslint-utils": "^4.8.0",
        "@eslint-community/regexpp": "^4.12.2",
        "@eslint/config-array": "^0.23.5",
        "@eslint/config-helpers": "^0.5.5",
        "@eslint/core": "^1.2.1",
        "@eslint/plugin-kit": "^0.7.1",
        "@humanfs/node": "^0.16.6",
        "@humanwhocodes/module-importer": "^1.0.1",
        "@humanwhocodes/retry": "^0.4.2",
        "@types/estree": "^1.0.6",
        "ajv": "^6.14.0",
        "cross-spawn": "^7.0.6",
        "debug": "^4.3.2",
        "escape-string-regexp": "^4.0.0",
        "eslint-scope": "^9.1.2",
        "eslint-visitor-keys": "^5.0.1",
        "espree": "^11.2.0",
        "esquery": "^1.7.0",
        "esutils": "^2.0.2",
        "fast-deep-equal": "^3.1.3",
        "file-entry-cache": "^8.0.0",
        "find-up": "^5.0.0",
        "glob-parent": "^6.0.2",
        "ignore": "^5.2.0",
        "imurmurhash": "^0.1.4",
        "is-glob": "^4.0.0",
        "json-stable-stringify-without-jsonify": "^1.0.1",
        "minimatch": "^10.2.4",
        "natural-compare": "^1.4.0",
        "optionator": "^0.9.3"
      },
      "bin": {
        "eslint": "bin/eslint.js"
      },
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      },
      "funding": {
        "url": "https://eslint.org/donate"
      },
      "peerDependencies": {
        "jiti": "*"
      },
      "peerDependenciesMeta": {
        "jiti": {
          "optional": true
        }
      }
    },
    "node_modules/eslint-plugin-react-hooks": {
      "version": "7.1.1",
      "resolved": "https://registry.npmjs.org/eslint-plugin-react-hooks/-/eslint-plugin-react-hooks-7.1.1.tgz",
      "integrity": "sha512-f2I7Gw6JbvCexzIInuSbZpfdQ44D7iqdWX01FKLvrPgqxoE7oMj8clOfto8U6vYiz4yd5oKu39rRSVOe1zRu0g==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@babel/core": "^7.24.4",
        "@babel/parser": "^7.24.4",
        "hermes-parser": "^0.25.1",
        "zod": "^3.25.0 || ^4.0.0",
        "zod-validation-error": "^3.5.0 || ^4.0.0"
      },
      "engines": {
        "node": ">=18"
      },
      "peerDependencies": {
        "eslint": "^3.0.0 || ^4.0.0 || ^5.0.0 || ^6.0.0 || ^7.0.0 || ^8.0.0-0 || ^9.0.0 || ^10.0.0"
      }
    },
    "node_modules/eslint-plugin-react-refresh": {
      "version": "0.5.2",
      "resolved": "https://registry.npmjs.org/eslint-plugin-react-refresh/-/eslint-plugin-react-refresh-0.5.2.tgz",
      "integrity": "sha512-hmgTH57GfzoTFjVN0yBwTggnsVUF2tcqi7RJZHqi9lIezSs4eFyAMktA68YD4r5kNw1mxyY4dmkyoFDb3FIqrA==",
      "dev": true,
      "license": "MIT",
      "peerDependencies": {
        "eslint": "^9 || ^10"
      }
    },
    "node_modules/eslint-scope": {
      "version": "9.1.2",
      "resolved": "https://registry.npmjs.org/eslint-scope/-/eslint-scope-9.1.2.tgz",
      "integrity": "sha512-xS90H51cKw0jltxmvmHy2Iai1LIqrfbw57b79w/J7MfvDfkIkFZ+kj6zC3BjtUwh150HsSSdxXZcsuv72miDFQ==",
      "dev": true,
      "license": "BSD-2-Clause",
      "dependencies": {
        "@types/esrecurse": "^4.3.1",
        "@types/estree": "^1.0.8",
        "esrecurse": "^4.3.0",
        "estraverse": "^5.2.0"
      },
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      },
      "funding": {
        "url": "https://opencollective.com/eslint"
      }
    },
    "node_modules/eslint-visitor-keys": {
      "version": "5.0.1",
      "resolved": "https://registry.npmjs.org/eslint-visitor-keys/-/eslint-visitor-keys-5.0.1.tgz",
      "integrity": "sha512-tD40eHxA35h0PEIZNeIjkHoDR4YjjJp34biM0mDvplBe//mB+IHCqHDGV7pxF+7MklTvighcCPPZC7ynWyjdTA==",
      "dev": true,
      "license": "Apache-2.0",
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      },
      "funding": {
        "url": "https://opencollective.com/eslint"
      }
    },
    "node_modules/espree": {
      "version": "11.2.0",
      "resolved": "https://registry.npmjs.org/espree/-/espree-11.2.0.tgz",
      "integrity": "sha512-7p3DrVEIopW1B1avAGLuCSh1jubc01H2JHc8B4qqGblmg5gI9yumBgACjWo4JlIc04ufug4xJ3SQI8HkS/Rgzw==",
      "dev": true,
      "license": "BSD-2-Clause",
      "dependencies": {
        "acorn": "^8.16.0",
        "acorn-jsx": "^5.3.2",
        "eslint-visitor-keys": "^5.0.1"
      },
      "engines": {
        "node": "^20.19.0 || ^22.13.0 || >=24"
      },
      "funding": {
        "url": "https://opencollective.com/eslint"
      }
    },
    "node_modules/esquery": {
      "version": "1.7.0",
      "resolved": "https://registry.npmjs.org/esquery/-/esquery-1.7.0.tgz",
      "integrity": "sha512-Ap6G0WQwcU/LHsvLwON1fAQX9Zp0A2Y6Y/cJBl9r/JbW90Zyg4/zbG6zzKa2OTALELarYHmKu0GhpM5EO+7T0g==",
      "dev": true,
      "license": "BSD-3-Clause",
      "dependencies": {
        "estraverse": "^5.1.0"
      },
      "engines": {
        "node": ">=0.10"
      }
    },
    "node_modules/esrecurse": {
      "version": "4.3.0",
      "resolved": "https://registry.npmjs.org/esrecurse/-/esrecurse-4.3.0.tgz",
      "integrity": "sha512-KmfKL3b6G+RXvP8N1vr3Tq1kL/oCFgn2NYXEtqP8/L3pKapUA4G8cFVaoF3SU323CD4XypR/ffioHmkti6/Tag==",
      "dev": true,
      "license": "BSD-2-Clause",
      "dependencies": {
        "estraverse": "^5.2.0"
      },
      "engines": {
        "node": ">=4.0"
      }
    },
    "node_modules/estraverse": {
      "version": "5.3.0",
      "resolved": "https://registry.npmjs.org/estraverse/-/estraverse-5.3.0.tgz",
      "integrity": "sha512-MMdARuVEQziNTeJD8DgMqmhwR11BRQ/cBP+pLtYdSTnf3MIO8fFeiINEbX36ZdNlfU/7A9f3gUw49B3oQsvwBA==",
      "dev": true,
      "license": "BSD-2-Clause",
      "engines": {
        "node": ">=4.0"
      }
    },
    "node_modules/esutils": {
      "version": "2.0.3",
      "resolved": "https://registry.npmjs.org/esutils/-/esutils-2.0.3.tgz",
      "integrity": "sha512-kVscqXk4OCp68SZ0dkgEKVi6/8ij300KBWTJq32P/dYeWTSwK41WyTxalN1eRmA5Z9UU/LX9D7FWSmV9SAYx6g==",
      "dev": true,
      "license": "BSD-2-Clause",
      "engines": {
        "node": ">=0.10.0"
      }
    },
    "node_modules/fast-deep-equal": {
      "version": "3.1.3",
      "resolved": "https://registry.npmjs.org/fast-deep-equal/-/fast-deep-equal-3.1.3.tgz",
      "integrity": "sha512-f3qQ9oQy9j2AhBe/H9VC91wLmKBCCU/gDOnKNAYG5hswO7BLKj09Hc5HYNz9cGI++xlpDCIgDaitVs03ATR84Q==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/fast-json-stable-stringify": {
      "version": "2.1.0",
      "resolved": "https://registry.npmjs.org/fast-json-stable-stringify/-/fast-json-stable-stringify-2.1.0.tgz",
      "integrity": "sha512-lhd/wF+Lk98HZoTCtlVraHtfh5XYijIjalXck7saUtuanSDyLMxnHhSXEDJqHxD7msR8D0uCmqlkwjCV8xvwHw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/fast-levenshtein": {
      "version": "2.0.6",
      "resolved": "https://registry.npmjs.org/fast-levenshtein/-/fast-levenshtein-2.0.6.tgz",
      "integrity": "sha512-DCXu6Ifhqcks7TZKY3Hxp3y6qphY5SJZmrWMDrKcERSOXWQdMhU9Ig/PYrzyw/ul9jOIyh0N4M0tbC5hodg8dw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/fdir": {
      "version": "6.5.0",
      "resolved": "https://registry.npmjs.org/fdir/-/fdir-6.5.0.tgz",
      "integrity": "sha512-tIbYtZbucOs0BRGqPJkshJUYdL+SDH7dVM8gjy+ERp3WAUjLEFJE+02kanyHtwjWOnwrKYBiwAmM0p4kLJAnXg==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=12.0.0"
      },
      "peerDependencies": {
        "picomatch": "^3 || ^4"
      },
      "peerDependenciesMeta": {
        "picomatch": {
          "optional": true
        }
      }
    },
    "node_modules/file-entry-cache": {
      "version": "8.0.0",
      "resolved": "https://registry.npmjs.org/file-entry-cache/-/file-entry-cache-8.0.0.tgz",
      "integrity": "sha512-XXTUwCvisa5oacNGRP9SfNtYBNAMi+RPwBFmblZEF7N7swHYQS6/Zfk7SRwx4D5j3CH211YNRco1DEMNVfZCnQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "flat-cache": "^4.0.0"
      },
      "engines": {
        "node": ">=16.0.0"
      }
    },
    "node_modules/find-up": {
      "version": "5.0.0",
      "resolved": "https://registry.npmjs.org/find-up/-/find-up-5.0.0.tgz",
      "integrity": "sha512-78/PXT1wlLLDgTzDs7sjq9hzz0vXD+zn+7wypEe4fXQxCmdmqfGsEPQxmiCSQI3ajFV91bVSsvNtrJRiW6nGng==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "locate-path": "^6.0.0",
        "path-exists": "^4.0.0"
      },
      "engines": {
        "node": ">=10"
      },
      "funding": {
        "url": "https://github.com/sponsors/sindresorhus"
      }
    },
    "node_modules/flat-cache": {
      "version": "4.0.1",
      "resolved": "https://registry.npmjs.org/flat-cache/-/flat-cache-4.0.1.tgz",
      "integrity": "sha512-f7ccFPK3SXFHpx15UIGyRJ/FJQctuKZ0zVuN3frBo4HnK3cay9VEW0R6yPYFHC0AgqhukPzKjq22t5DmAyqGyw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "flatted": "^3.2.9",
        "keyv": "^4.5.4"
      },
      "engines": {
        "node": ">=16"
      }
    },
    "node_modules/flatted": {
      "version": "3.4.2",
      "resolved": "https://registry.npmjs.org/flatted/-/flatted-3.4.2.tgz",
      "integrity": "sha512-PjDse7RzhcPkIJwy5t7KPWQSZ9cAbzQXcafsetQoD7sOJRQlGikNbx7yZp2OotDnJyrDcbyRq3Ttb18iYOqkxA==",
      "dev": true,
      "license": "ISC"
    },
    "node_modules/fsevents": {
      "version": "2.3.3",
      "resolved": "https://registry.npmjs.org/fsevents/-/fsevents-2.3.3.tgz",
      "integrity": "sha512-5xoDfX+fL7faATnagmWPpbFtwh/R77WmMMqqHGS65C3vvB0YHrgF+B1YmZ3441tMj5n63k0212XNoJwzlhffQw==",
      "dev": true,
      "hasInstallScript": true,
      "license": "MIT",
      "optional": true,
      "os": [
        "darwin"
      ],
      "engines": {
        "node": "^8.16.0 || ^10.6.0 || >=11.0.0"
      }
    },
    "node_modules/gensync": {
      "version": "1.0.0-beta.2",
      "resolved": "https://registry.npmjs.org/gensync/-/gensync-1.0.0-beta.2.tgz",
      "integrity": "sha512-3hN7NaskYvMDLQY55gnW3NQ+mesEAepTqlg+VEbj7zzqEMBVNhzcGYYeqFo/TlYz6eQiFcp1HcsCZO+nGgS8zg==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6.9.0"
      }
    },
    "node_modules/glob-parent": {
      "version": "6.0.2",
      "resolved": "https://registry.npmjs.org/glob-parent/-/glob-parent-6.0.2.tgz",
      "integrity": "sha512-XxwI8EOhVQgWp6iDL+3b0r86f4d6AX6zSU55HfB4ydCEuXLXc5FcYeOu+nnGftS4TEju/11rt4KJPTMgbfmv4A==",
      "dev": true,
      "license": "ISC",
      "dependencies": {
        "is-glob": "^4.0.3"
      },
      "engines": {
        "node": ">=10.13.0"
      }
    },
    "node_modules/globals": {
      "version": "17.5.0",
      "resolved": "https://registry.npmjs.org/globals/-/globals-17.5.0.tgz",
      "integrity": "sha512-qoV+HK2yFl/366t2/Cb3+xxPUo5BuMynomoDmiaZBIdbs+0pYbjfZU+twLhGKp4uCZ/+NbtpVepH5bGCxRyy2g==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=18"
      },
      "funding": {
        "url": "https://github.com/sponsors/sindresorhus"
      }
    },
    "node_modules/hermes-estree": {
      "version": "0.25.1",
      "resolved": "https://registry.npmjs.org/hermes-estree/-/hermes-estree-0.25.1.tgz",
      "integrity": "sha512-0wUoCcLp+5Ev5pDW2OriHC2MJCbwLwuRx+gAqMTOkGKJJiBCLjtrvy4PWUGn6MIVefecRpzoOZ/UV6iGdOr+Cw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/hermes-parser": {
      "version": "0.25.1",
      "resolved": "https://registry.npmjs.org/hermes-parser/-/hermes-parser-0.25.1.tgz",
      "integrity": "sha512-6pEjquH3rqaI6cYAXYPcz9MS4rY6R4ngRgrgfDshRptUZIc3lw0MCIJIGDj9++mfySOuPTHB4nrSW99BCvOPIA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "hermes-estree": "0.25.1"
      }
    },
    "node_modules/ignore": {
      "version": "5.3.2",
      "resolved": "https://registry.npmjs.org/ignore/-/ignore-5.3.2.tgz",
      "integrity": "sha512-hsBTNUqQTDwkWtcdYI2i06Y/nUBEsNEDJKjWdigLvegy8kDuJAS8uRlpkkcQpyEXL0Z/pjDy5HBmMjRCJ2gq+g==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">= 4"
      }
    },
    "node_modules/imurmurhash": {
      "version": "0.1.4",
      "resolved": "https://registry.npmjs.org/imurmurhash/-/imurmurhash-0.1.4.tgz",
      "integrity": "sha512-JmXMZ6wuvDmLiHEml9ykzqO6lwFbof0GG4IkcGaENdCRDDmMVnny7s5HsIgHCbaq0w2MyPhDqkhTUgS2LU2PHA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=0.8.19"
      }
    },
    "node_modules/is-extglob": {
      "version": "2.1.1",
      "resolved": "https://registry.npmjs.org/is-extglob/-/is-extglob-2.1.1.tgz",
      "integrity": "sha512-SbKbANkN603Vi4jEZv49LeVJMn4yGwsbzZworEoyEiutsN3nJYdbO36zfhGJ6QEDpOZIFkDtnq5JRxmvl3jsoQ==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=0.10.0"
      }
    },
    "node_modules/is-glob": {
      "version": "4.0.3",
      "resolved": "https://registry.npmjs.org/is-glob/-/is-glob-4.0.3.tgz",
      "integrity": "sha512-xelSayHH36ZgE7ZWhli7pW34hNbNl8Ojv5KVmkJD4hBdD3th8Tfk9vYasLM+mXWOZhFkgZfxhLSnrwRr4elSSg==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "is-extglob": "^2.1.1"
      },
      "engines": {
        "node": ">=0.10.0"
      }
    },
    "node_modules/isexe": {
      "version": "2.0.0",
      "resolved": "https://registry.npmjs.org/isexe/-/isexe-2.0.0.tgz",
      "integrity": "sha512-RHxMLp9lnKHGHRng9QFhRCMbYAcVpn69smSGcq3f36xjgVVWThj4qqLbTLlq7Ssj8B+fIQ1EuCEGI2lKsyQeIw==",
      "dev": true,
      "license": "ISC"
    },
    "node_modules/js-tokens": {
      "version": "4.0.0",
      "resolved": "https://registry.npmjs.org/js-tokens/-/js-tokens-4.0.0.tgz",
      "integrity": "sha512-RdJUflcE3cUzKiMqQgsCu06FPu9UdIJO0beYbPhHN4k6apgJtifcoCtT9bcxOpYBtpD2kCM6Sbzg4CausW/PKQ==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/jsesc": {
      "version": "3.1.0",
      "resolved": "https://registry.npmjs.org/jsesc/-/jsesc-3.1.0.tgz",
      "integrity": "sha512-/sM3dO2FOzXjKQhJuo0Q173wf2KOo8t4I8vHy6lF9poUp7bKT0/NHE8fPX23PwfhnykfqnC2xRxOnVw5XuGIaA==",
      "dev": true,
      "license": "MIT",
      "bin": {
        "jsesc": "bin/jsesc"
      },
      "engines": {
        "node": ">=6"
      }
    },
    "node_modules/json-buffer": {
      "version": "3.0.1",
      "resolved": "https://registry.npmjs.org/json-buffer/-/json-buffer-3.0.1.tgz",
      "integrity": "sha512-4bV5BfR2mqfQTJm+V5tPPdf+ZpuhiIvTuAB5g8kcrXOZpTT/QwwVRWBywX1ozr6lEuPdbHxwaJlm9G6mI2sfSQ==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/json-schema-traverse": {
      "version": "0.4.1",
      "resolved": "https://registry.npmjs.org/json-schema-traverse/-/json-schema-traverse-0.4.1.tgz",
      "integrity": "sha512-xbbCH5dCYU5T8LcEhhuh7HJ88HXuW3qsI3Y0zOZFKfZEHcpWiHU/Jxzk629Brsab/mMiHQti9wMP+845RPe3Vg==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/json-stable-stringify-without-jsonify": {
      "version": "1.0.1",
      "resolved": "https://registry.npmjs.org/json-stable-stringify-without-jsonify/-/json-stable-stringify-without-jsonify-1.0.1.tgz",
      "integrity": "sha512-Bdboy+l7tA3OGW6FjyFHWkP5LuByj1Tk33Ljyq0axyzdk9//JSi2u3fP1QSmd1KNwq6VOKYGlAu87CisVir6Pw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/json5": {
      "version": "2.2.3",
      "resolved": "https://registry.npmjs.org/json5/-/json5-2.2.3.tgz",
      "integrity": "sha512-XmOWe7eyHYH14cLdVPoyg+GOH3rYX++KpzrylJwSW98t3Nk+U8XOl8FWKOgwtzdb8lXGf6zYwDUzeHMWfxasyg==",
      "dev": true,
      "license": "MIT",
      "bin": {
        "json5": "lib/cli.js"
      },
      "engines": {
        "node": ">=6"
      }
    },
    "node_modules/keyv": {
      "version": "4.5.4",
      "resolved": "https://registry.npmjs.org/keyv/-/keyv-4.5.4.tgz",
      "integrity": "sha512-oxVHkHR/EJf2CNXnWxRLW6mg7JyCCUcG0DtEGmL2ctUo1PNTin1PUil+r/+4r5MpVgC/fn1kjsx7mjSujKqIpw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "json-buffer": "3.0.1"
      }
    },
    "node_modules/levn": {
      "version": "0.4.1",
      "resolved": "https://registry.npmjs.org/levn/-/levn-0.4.1.tgz",
      "integrity": "sha512-+bT2uH4E5LGE7h/n3evcS/sQlJXCpIp6ym8OWJ5eV6+67Dsql/LaaT7qJBAt2rzfoa/5QBGBhxDix1dMt2kQKQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "prelude-ls": "^1.2.1",
        "type-check": "~0.4.0"
      },
      "engines": {
        "node": ">= 0.8.0"
      }
    },
    "node_modules/lightningcss": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss/-/lightningcss-1.32.0.tgz",
      "integrity": "sha512-NXYBzinNrblfraPGyrbPoD19C1h9lfI/1mzgWYvXUTe414Gz/X1FD2XBZSZM7rRTrMA8JL3OtAaGifrIKhQ5yQ==",
      "dev": true,
      "license": "MPL-2.0",
      "dependencies": {
        "detect-libc": "^2.0.3"
      },
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      },
      "optionalDependencies": {
        "lightningcss-android-arm64": "1.32.0",
        "lightningcss-darwin-arm64": "1.32.0",
        "lightningcss-darwin-x64": "1.32.0",
        "lightningcss-freebsd-x64": "1.32.0",
        "lightningcss-linux-arm-gnueabihf": "1.32.0",
        "lightningcss-linux-arm64-gnu": "1.32.0",
        "lightningcss-linux-arm64-musl": "1.32.0",
        "lightningcss-linux-x64-gnu": "1.32.0",
        "lightningcss-linux-x64-musl": "1.32.0",
        "lightningcss-win32-arm64-msvc": "1.32.0",
        "lightningcss-win32-x64-msvc": "1.32.0"
      }
    },
    "node_modules/lightningcss-android-arm64": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-android-arm64/-/lightningcss-android-arm64-1.32.0.tgz",
      "integrity": "sha512-YK7/ClTt4kAK0vo6w3X+Pnm0D2cf2vPHbhOXdoNti1Ga0al1P4TBZhwjATvjNwLEBCnKvjJc2jQgHXH0NEwlAg==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "android"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-darwin-arm64": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-darwin-arm64/-/lightningcss-darwin-arm64-1.32.0.tgz",
      "integrity": "sha512-RzeG9Ju5bag2Bv1/lwlVJvBE3q6TtXskdZLLCyfg5pt+HLz9BqlICO7LZM7VHNTTn/5PRhHFBSjk5lc4cmscPQ==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "darwin"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-darwin-x64": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-darwin-x64/-/lightningcss-darwin-x64-1.32.0.tgz",
      "integrity": "sha512-U+QsBp2m/s2wqpUYT/6wnlagdZbtZdndSmut/NJqlCcMLTWp5muCrID+K5UJ6jqD2BFshejCYXniPDbNh73V8w==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "darwin"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-freebsd-x64": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-freebsd-x64/-/lightningcss-freebsd-x64-1.32.0.tgz",
      "integrity": "sha512-JCTigedEksZk3tHTTthnMdVfGf61Fky8Ji2E4YjUTEQX14xiy/lTzXnu1vwiZe3bYe0q+SpsSH/CTeDXK6WHig==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "freebsd"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-linux-arm-gnueabihf": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-linux-arm-gnueabihf/-/lightningcss-linux-arm-gnueabihf-1.32.0.tgz",
      "integrity": "sha512-x6rnnpRa2GL0zQOkt6rts3YDPzduLpWvwAF6EMhXFVZXD4tPrBkEFqzGowzCsIWsPjqSK+tyNEODUBXeeVHSkw==",
      "cpu": [
        "arm"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-linux-arm64-gnu": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-linux-arm64-gnu/-/lightningcss-linux-arm64-gnu-1.32.0.tgz",
      "integrity": "sha512-0nnMyoyOLRJXfbMOilaSRcLH3Jw5z9HDNGfT/gwCPgaDjnx0i8w7vBzFLFR1f6CMLKF8gVbebmkUN3fa/kQJpQ==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-linux-arm64-musl": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-linux-arm64-musl/-/lightningcss-linux-arm64-musl-1.32.0.tgz",
      "integrity": "sha512-UpQkoenr4UJEzgVIYpI80lDFvRmPVg6oqboNHfoH4CQIfNA+HOrZ7Mo7KZP02dC6LjghPQJeBsvXhJod/wnIBg==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-linux-x64-gnu": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-linux-x64-gnu/-/lightningcss-linux-x64-gnu-1.32.0.tgz",
      "integrity": "sha512-V7Qr52IhZmdKPVr+Vtw8o+WLsQJYCTd8loIfpDaMRWGUZfBOYEJeyJIkqGIDMZPwPx24pUMfwSxxI8phr/MbOA==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-linux-x64-musl": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-linux-x64-musl/-/lightningcss-linux-x64-musl-1.32.0.tgz",
      "integrity": "sha512-bYcLp+Vb0awsiXg/80uCRezCYHNg1/l3mt0gzHnWV9XP1W5sKa5/TCdGWaR/zBM2PeF/HbsQv/j2URNOiVuxWg==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "linux"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-win32-arm64-msvc": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-win32-arm64-msvc/-/lightningcss-win32-arm64-msvc-1.32.0.tgz",
      "integrity": "sha512-8SbC8BR40pS6baCM8sbtYDSwEVQd4JlFTOlaD3gWGHfThTcABnNDBda6eTZeqbofalIJhFx0qKzgHJmcPTnGdw==",
      "cpu": [
        "arm64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "win32"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/lightningcss-win32-x64-msvc": {
      "version": "1.32.0",
      "resolved": "https://registry.npmjs.org/lightningcss-win32-x64-msvc/-/lightningcss-win32-x64-msvc-1.32.0.tgz",
      "integrity": "sha512-Amq9B/SoZYdDi1kFrojnoqPLxYhQ4Wo5XiL8EVJrVsB8ARoC1PWW6VGtT0WKCemjy8aC+louJnjS7U18x3b06Q==",
      "cpu": [
        "x64"
      ],
      "dev": true,
      "license": "MPL-2.0",
      "optional": true,
      "os": [
        "win32"
      ],
      "engines": {
        "node": ">= 12.0.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/parcel"
      }
    },
    "node_modules/locate-path": {
      "version": "6.0.0",
      "resolved": "https://registry.npmjs.org/locate-path/-/locate-path-6.0.0.tgz",
      "integrity": "sha512-iPZK6eYjbxRu3uB4/WZ3EsEIMJFMqAoopl3R+zuq0UjcAm/MO6KCweDgPfP3elTztoKP3KtnVHxTn2NHBSDVUw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "p-locate": "^5.0.0"
      },
      "engines": {
        "node": ">=10"
      },
      "funding": {
        "url": "https://github.com/sponsors/sindresorhus"
      }
    },
    "node_modules/lru-cache": {
      "version": "5.1.1",
      "resolved": "https://registry.npmjs.org/lru-cache/-/lru-cache-5.1.1.tgz",
      "integrity": "sha512-KpNARQA3Iwv+jTA0utUVVbrh+Jlrr1Fv0e56GGzAFOXN7dk/FviaDW8LHmK52DlcH4WP2n6gI8vN1aesBFgo9w==",
      "dev": true,
      "license": "ISC",
      "dependencies": {
        "yallist": "^3.0.2"
      }
    },
    "node_modules/minimatch": {
      "version": "10.2.5",
      "resolved": "https://registry.npmjs.org/minimatch/-/minimatch-10.2.5.tgz",
      "integrity": "sha512-MULkVLfKGYDFYejP07QOurDLLQpcjk7Fw+7jXS2R2czRQzR56yHRveU5NDJEOviH+hETZKSkIk5c+T23GjFUMg==",
      "dev": true,
      "license": "BlueOak-1.0.0",
      "dependencies": {
        "brace-expansion": "^5.0.5"
      },
      "engines": {
        "node": "18 || 20 || >=22"
      },
      "funding": {
        "url": "https://github.com/sponsors/isaacs"
      }
    },
    "node_modules/ms": {
      "version": "2.1.3",
      "resolved": "https://registry.npmjs.org/ms/-/ms-2.1.3.tgz",
      "integrity": "sha512-6FlzubTLZG3J2a/NVCAleEhjzq5oxgHyaCU9yYXvcLsvoVaHJq/s5xXI6/XXP6tz7R9xAOtHnSO/tXtF3WRTlA==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/nanoid": {
      "version": "3.3.11",
      "resolved": "https://registry.npmjs.org/nanoid/-/nanoid-3.3.11.tgz",
      "integrity": "sha512-N8SpfPUnUp1bK+PMYW8qSWdl9U+wwNWI4QKxOYDy9JAro3WMX7p2OeVRF9v+347pnakNevPmiHhNmZ2HbFA76w==",
      "dev": true,
      "funding": [
        {
          "type": "github",
          "url": "https://github.com/sponsors/ai"
        }
      ],
      "license": "MIT",
      "bin": {
        "nanoid": "bin/nanoid.cjs"
      },
      "engines": {
        "node": "^10 || ^12 || ^13.7 || ^14 || >=15.0.1"
      }
    },
    "node_modules/natural-compare": {
      "version": "1.4.0",
      "resolved": "https://registry.npmjs.org/natural-compare/-/natural-compare-1.4.0.tgz",
      "integrity": "sha512-OWND8ei3VtNC9h7V60qff3SVobHr996CTwgxubgyQYEpg290h9J0buyECNNJexkFm5sOajh5G116RYA1c8ZMSw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/node-releases": {
      "version": "2.0.38",
      "resolved": "https://registry.npmjs.org/node-releases/-/node-releases-2.0.38.tgz",
      "integrity": "sha512-3qT/88Y3FbH/Kx4szpQQ4HzUbVrHPKTLVpVocKiLfoYvw9XSGOX2FmD2d6DrXbVYyAQTF2HeF6My8jmzx7/CRw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/optionator": {
      "version": "0.9.4",
      "resolved": "https://registry.npmjs.org/optionator/-/optionator-0.9.4.tgz",
      "integrity": "sha512-6IpQ7mKUxRcZNLIObR0hz7lxsapSSIYNZJwXPGeF0mTVqGKFIXj1DQcMoT22S3ROcLyY/rz0PWaWZ9ayWmad9g==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "deep-is": "^0.1.3",
        "fast-levenshtein": "^2.0.6",
        "levn": "^0.4.1",
        "prelude-ls": "^1.2.1",
        "type-check": "^0.4.0",
        "word-wrap": "^1.2.5"
      },
      "engines": {
        "node": ">= 0.8.0"
      }
    },
    "node_modules/p-limit": {
      "version": "3.1.0",
      "resolved": "https://registry.npmjs.org/p-limit/-/p-limit-3.1.0.tgz",
      "integrity": "sha512-TYOanM3wGwNGsZN2cVTYPArw454xnXj5qmWF1bEoAc4+cU/ol7GVh7odevjp1FNHduHc3KZMcFduxU5Xc6uJRQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "yocto-queue": "^0.1.0"
      },
      "engines": {
        "node": ">=10"
      },
      "funding": {
        "url": "https://github.com/sponsors/sindresorhus"
      }
    },
    "node_modules/p-locate": {
      "version": "5.0.0",
      "resolved": "https://registry.npmjs.org/p-locate/-/p-locate-5.0.0.tgz",
      "integrity": "sha512-LaNjtRWUBY++zB5nE/NwcaoMylSPk+S+ZHNB1TzdbMJMny6dynpAGt7X/tl/QYq3TIeE6nxHppbo2LGymrG5Pw==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "p-limit": "^3.0.2"
      },
      "engines": {
        "node": ">=10"
      },
      "funding": {
        "url": "https://github.com/sponsors/sindresorhus"
      }
    },
    "node_modules/path-exists": {
      "version": "4.0.0",
      "resolved": "https://registry.npmjs.org/path-exists/-/path-exists-4.0.0.tgz",
      "integrity": "sha512-ak9Qy5Q7jYb2Wwcey5Fpvg2KoAc/ZIhLSLOSBmRmygPsGwkVVt0fZa0qrtMz+m6tJTAHfZQ8FnmB4MG4LWy7/w==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=8"
      }
    },
    "node_modules/path-key": {
      "version": "3.1.1",
      "resolved": "https://registry.npmjs.org/path-key/-/path-key-3.1.1.tgz",
      "integrity": "sha512-ojmeN0qd+y0jszEtoY48r0Peq5dwMEkIlCOu6Q5f41lfkswXuKtYrhgoTpLnyIcHm24Uhqx+5Tqm2InSwLhE6Q==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=8"
      }
    },
    "node_modules/picocolors": {
      "version": "1.1.1",
      "resolved": "https://registry.npmjs.org/picocolors/-/picocolors-1.1.1.tgz",
      "integrity": "sha512-xceH2snhtb5M9liqDsmEw56le376mTZkEX/jEb/RxNFyegNul7eNslCXP9FDj/Lcu0X8KEyMceP2ntpaHrDEVA==",
      "dev": true,
      "license": "ISC"
    },
    "node_modules/picomatch": {
      "version": "4.0.4",
      "resolved": "https://registry.npmjs.org/picomatch/-/picomatch-4.0.4.tgz",
      "integrity": "sha512-QP88BAKvMam/3NxH6vj2o21R6MjxZUAd6nlwAS/pnGvN9IVLocLHxGYIzFhg6fUQ+5th6P4dv4eW9jX3DSIj7A==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "engines": {
        "node": ">=12"
      },
      "funding": {
        "url": "https://github.com/sponsors/jonschlinkert"
      }
    },
    "node_modules/postcss": {
      "version": "8.5.13",
      "resolved": "https://registry.npmjs.org/postcss/-/postcss-8.5.13.tgz",
      "integrity": "sha512-qif0+jGGZoLWdHey3UFHHWP0H7Gbmsk8T5VEqyYFbWqPr1XqvLGBbk/sl8V5exGmcYJklJOhOQq1pV9IcsiFag==",
      "dev": true,
      "funding": [
        {
          "type": "opencollective",
          "url": "https://opencollective.com/postcss/"
        },
        {
          "type": "tidelift",
          "url": "https://tidelift.com/funding/github/npm/postcss"
        },
        {
          "type": "github",
          "url": "https://github.com/sponsors/ai"
        }
      ],
      "license": "MIT",
      "dependencies": {
        "nanoid": "^3.3.11",
        "picocolors": "^1.1.1",
        "source-map-js": "^1.2.1"
      },
      "engines": {
        "node": "^10 || ^12 || >=14"
      }
    },
    "node_modules/prelude-ls": {
      "version": "1.2.1",
      "resolved": "https://registry.npmjs.org/prelude-ls/-/prelude-ls-1.2.1.tgz",
      "integrity": "sha512-vkcDPrRZo1QZLbn5RLGPpg/WmIQ65qoWWhcGKf/b5eplkkarX0m9z8ppCat4mlOqUsWpyNuYgO3VRyrYHSzX5g==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">= 0.8.0"
      }
    },
    "node_modules/punycode": {
      "version": "2.3.1",
      "resolved": "https://registry.npmjs.org/punycode/-/punycode-2.3.1.tgz",
      "integrity": "sha512-vYt7UD1U9Wg6138shLtLOvdAu+8DsC/ilFtEVHcH+wydcSpNE20AfSOduf6MkRFahL5FY7X1oU7nKVZFtfq8Fg==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=6"
      }
    },
    "node_modules/react": {
      "version": "19.2.5",
      "resolved": "https://registry.npmjs.org/react/-/react-19.2.5.tgz",
      "integrity": "sha512-llUJLzz1zTUBrskt2pwZgLq59AemifIftw4aB7JxOqf1HY2FDaGDxgwpAPVzHU1kdWabH7FauP4i1oEeer2WCA==",
      "license": "MIT",
      "peer": true,
      "engines": {
        "node": ">=0.10.0"
      }
    },
    "node_modules/react-dom": {
      "version": "19.2.5",
      "resolved": "https://registry.npmjs.org/react-dom/-/react-dom-19.2.5.tgz",
      "integrity": "sha512-J5bAZz+DXMMwW/wV3xzKke59Af6CHY7G4uYLN1OvBcKEsWOs4pQExj86BBKamxl/Ik5bx9whOrvBlSDfWzgSag==",
      "license": "MIT",
      "dependencies": {
        "scheduler": "^0.27.0"
      },
      "peerDependencies": {
        "react": "^19.2.5"
      }
    },
    "node_modules/rolldown": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/rolldown/-/rolldown-1.0.0-rc.17.tgz",
      "integrity": "sha512-ZrT53oAKrtA4+YtBWPQbtPOxIbVDbxT0orcYERKd63VJTF13zPcgXTvD4843L8pcsI7M6MErt8QtON6lrB9tyA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@oxc-project/types": "=0.127.0",
        "@rolldown/pluginutils": "1.0.0-rc.17"
      },
      "bin": {
        "rolldown": "bin/cli.mjs"
      },
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      },
      "optionalDependencies": {
        "@rolldown/binding-android-arm64": "1.0.0-rc.17",
        "@rolldown/binding-darwin-arm64": "1.0.0-rc.17",
        "@rolldown/binding-darwin-x64": "1.0.0-rc.17",
        "@rolldown/binding-freebsd-x64": "1.0.0-rc.17",
        "@rolldown/binding-linux-arm-gnueabihf": "1.0.0-rc.17",
        "@rolldown/binding-linux-arm64-gnu": "1.0.0-rc.17",
        "@rolldown/binding-linux-arm64-musl": "1.0.0-rc.17",
        "@rolldown/binding-linux-ppc64-gnu": "1.0.0-rc.17",
        "@rolldown/binding-linux-s390x-gnu": "1.0.0-rc.17",
        "@rolldown/binding-linux-x64-gnu": "1.0.0-rc.17",
        "@rolldown/binding-linux-x64-musl": "1.0.0-rc.17",
        "@rolldown/binding-openharmony-arm64": "1.0.0-rc.17",
        "@rolldown/binding-wasm32-wasi": "1.0.0-rc.17",
        "@rolldown/binding-win32-arm64-msvc": "1.0.0-rc.17",
        "@rolldown/binding-win32-x64-msvc": "1.0.0-rc.17"
      }
    },
    "node_modules/rolldown/node_modules/@rolldown/pluginutils": {
      "version": "1.0.0-rc.17",
      "resolved": "https://registry.npmjs.org/@rolldown/pluginutils/-/pluginutils-1.0.0-rc.17.tgz",
      "integrity": "sha512-n8iosDOt6Ig1UhJ2AYqoIhHWh/isz0xpicHTzpKBeotdVsTEcxsSA/i3EVM7gQAj0rU27OLAxCjzlj15IWY7bg==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/scheduler": {
      "version": "0.27.0",
      "resolved": "https://registry.npmjs.org/scheduler/-/scheduler-0.27.0.tgz",
      "integrity": "sha512-eNv+WrVbKu1f3vbYJT/xtiF5syA5HPIMtf9IgY/nKg0sWqzAUEvqY/xm7OcZc/qafLx/iO9FgOmeSAp4v5ti/Q==",
      "license": "MIT"
    },
    "node_modules/semver": {
      "version": "6.3.1",
      "resolved": "https://registry.npmjs.org/semver/-/semver-6.3.1.tgz",
      "integrity": "sha512-BR7VvDCVHO+q2xBEWskxS6DJE1qRnb7DxzUrogb71CWoSficBxYsiAGd+Kl0mmq/MprG9yArRkyrQxTO6XjMzA==",
      "dev": true,
      "license": "ISC",
      "bin": {
        "semver": "bin/semver.js"
      }
    },
    "node_modules/shebang-command": {
      "version": "2.0.0",
      "resolved": "https://registry.npmjs.org/shebang-command/-/shebang-command-2.0.0.tgz",
      "integrity": "sha512-kHxr2zZpYtdmrN1qDjrrX/Z1rR1kG8Dx+gkpK1G4eXmvXswmcE1hTWBWYUzlraYw1/yZp6YuDY77YtvbN0dmDA==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "shebang-regex": "^3.0.0"
      },
      "engines": {
        "node": ">=8"
      }
    },
    "node_modules/shebang-regex": {
      "version": "3.0.0",
      "resolved": "https://registry.npmjs.org/shebang-regex/-/shebang-regex-3.0.0.tgz",
      "integrity": "sha512-7++dFhtcx3353uBaq8DDR4NuxBetBzC7ZQOhmTQInHEd6bSrXdiEyzCvG07Z44UYdLShWUyXt5M/yhz8ekcb1A==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=8"
      }
    },
    "node_modules/source-map-js": {
      "version": "1.2.1",
      "resolved": "https://registry.npmjs.org/source-map-js/-/source-map-js-1.2.1.tgz",
      "integrity": "sha512-UXWMKhLOwVKb728IUtQPXxfYU+usdybtUrK/8uGE8CQMvrhOpwvzDBwj0QhSL7MQc7vIsISBG8VQ8+IDQxpfQA==",
      "dev": true,
      "license": "BSD-3-Clause",
      "engines": {
        "node": ">=0.10.0"
      }
    },
    "node_modules/tinyglobby": {
      "version": "0.2.16",
      "resolved": "https://registry.npmjs.org/tinyglobby/-/tinyglobby-0.2.16.tgz",
      "integrity": "sha512-pn99VhoACYR8nFHhxqix+uvsbXineAasWm5ojXoN8xEwK5Kd3/TrhNn1wByuD52UxWRLy8pu+kRMniEi6Eq9Zg==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "fdir": "^6.5.0",
        "picomatch": "^4.0.4"
      },
      "engines": {
        "node": ">=12.0.0"
      },
      "funding": {
        "url": "https://github.com/sponsors/SuperchupuDev"
      }
    },
    "node_modules/ts-api-utils": {
      "version": "2.5.0",
      "resolved": "https://registry.npmjs.org/ts-api-utils/-/ts-api-utils-2.5.0.tgz",
      "integrity": "sha512-OJ/ibxhPlqrMM0UiNHJ/0CKQkoKF243/AEmplt3qpRgkW8VG7IfOS41h7V8TjITqdByHzrjcS/2si+y4lIh8NA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=18.12"
      },
      "peerDependencies": {
        "typescript": ">=4.8.4"
      }
    },
    "node_modules/tslib": {
      "version": "2.8.1",
      "resolved": "https://registry.npmjs.org/tslib/-/tslib-2.8.1.tgz",
      "integrity": "sha512-oJFu94HQb+KVduSUQL7wnpmqnfmLsOA/nAh6b6EH0wCEoK0/mPeXU6c3wKDV83MkOuHPRHtSXKKU99IBazS/2w==",
      "dev": true,
      "license": "0BSD",
      "optional": true
    },
    "node_modules/type-check": {
      "version": "0.4.0",
      "resolved": "https://registry.npmjs.org/type-check/-/type-check-0.4.0.tgz",
      "integrity": "sha512-XleUoc9uwGXqjWwXaUTZAmzMcFZ5858QA2vvx1Ur5xIcixXIP+8LnFDgRplU30us6teqdlskFfu+ae4K79Ooew==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "prelude-ls": "^1.2.1"
      },
      "engines": {
        "node": ">= 0.8.0"
      }
    },
    "node_modules/typescript": {
      "version": "6.0.3",
      "resolved": "https://registry.npmjs.org/typescript/-/typescript-6.0.3.tgz",
      "integrity": "sha512-y2TvuxSZPDyQakkFRPZHKFm+KKVqIisdg9/CZwm9ftvKXLP8NRWj38/ODjNbr43SsoXqNuAisEf1GdCxqWcdBw==",
      "dev": true,
      "license": "Apache-2.0",
      "peer": true,
      "bin": {
        "tsc": "bin/tsc",
        "tsserver": "bin/tsserver"
      },
      "engines": {
        "node": ">=14.17"
      }
    },
    "node_modules/typescript-eslint": {
      "version": "8.59.1",
      "resolved": "https://registry.npmjs.org/typescript-eslint/-/typescript-eslint-8.59.1.tgz",
      "integrity": "sha512-xqDcFVBmlrltH64lklOVp1wYxgJr6LVdg3NamBgH2OOQDLFdTKfIZXF5PfghrnXQKXZGTQs8tr1vL7fJvq8CTQ==",
      "dev": true,
      "license": "MIT",
      "dependencies": {
        "@typescript-eslint/eslint-plugin": "8.59.1",
        "@typescript-eslint/parser": "8.59.1",
        "@typescript-eslint/typescript-estree": "8.59.1",
        "@typescript-eslint/utils": "8.59.1"
      },
      "engines": {
        "node": "^18.18.0 || ^20.9.0 || >=21.1.0"
      },
      "funding": {
        "type": "opencollective",
        "url": "https://opencollective.com/typescript-eslint"
      },
      "peerDependencies": {
        "eslint": "^8.57.0 || ^9.0.0 || ^10.0.0",
        "typescript": ">=4.8.4 <6.1.0"
      }
    },
    "node_modules/undici-types": {
      "version": "7.16.0",
      "resolved": "https://registry.npmjs.org/undici-types/-/undici-types-7.16.0.tgz",
      "integrity": "sha512-Zz+aZWSj8LE6zoxD+xrjh4VfkIG8Ya6LvYkZqtUQGJPZjYl53ypCaUwWqo7eI0x66KBGeRo+mlBEkMSeSZ38Nw==",
      "dev": true,
      "license": "MIT"
    },
    "node_modules/update-browserslist-db": {
      "version": "1.2.3",
      "resolved": "https://registry.npmjs.org/update-browserslist-db/-/update-browserslist-db-1.2.3.tgz",
      "integrity": "sha512-Js0m9cx+qOgDxo0eMiFGEueWztz+d4+M3rGlmKPT+T4IS/jP4ylw3Nwpu6cpTTP8R1MAC1kF4VbdLt3ARf209w==",
      "dev": true,
      "funding": [
        {
          "type": "opencollective",
          "url": "https://opencollective.com/browserslist"
        },
        {
          "type": "tidelift",
          "url": "https://tidelift.com/funding/github/npm/browserslist"
        },
        {
          "type": "github",
          "url": "https://github.com/sponsors/ai"
        }
      ],
      "license": "MIT",
      "dependencies": {
        "escalade": "^3.2.0",
        "picocolors": "^1.1.1"
      },
      "bin": {
        "update-browserslist-db": "cli.js"
      },
      "peerDependencies": {
        "browserslist": ">= 4.21.0"
      }
    },
    "node_modules/uri-js": {
      "version": "4.4.1",
      "resolved": "https://registry.npmjs.org/uri-js/-/uri-js-4.4.1.tgz",
      "integrity": "sha512-7rKUyy33Q1yc98pQ1DAmLtwX109F7TIfWlW1Ydo8Wl1ii1SeHieeh0HHfPeL2fMXK6z0s8ecKs9frCuLJvndBg==",
      "dev": true,
      "license": "BSD-2-Clause",
      "dependencies": {
        "punycode": "^2.1.0"
      }
    },
    "node_modules/vite": {
      "version": "8.0.10",
      "resolved": "https://registry.npmjs.org/vite/-/vite-8.0.10.tgz",
      "integrity": "sha512-rZuUu9j6J5uotLDs+cAA4O5H4K1SfPliUlQwqa6YEwSrWDZzP4rhm00oJR5snMewjxF5V/K3D4kctsUTsIU9Mw==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "dependencies": {
        "lightningcss": "^1.32.0",
        "picomatch": "^4.0.4",
        "postcss": "^8.5.10",
        "rolldown": "1.0.0-rc.17",
        "tinyglobby": "^0.2.16"
      },
      "bin": {
        "vite": "bin/vite.js"
      },
      "engines": {
        "node": "^20.19.0 || >=22.12.0"
      },
      "funding": {
        "url": "https://github.com/vitejs/vite?sponsor=1"
      },
      "optionalDependencies": {
        "fsevents": "~2.3.3"
      },
      "peerDependencies": {
        "@types/node": "^20.19.0 || >=22.12.0",
        "@vitejs/devtools": "^0.1.0",
        "esbuild": "^0.27.0 || ^0.28.0",
        "jiti": ">=1.21.0",
        "less": "^4.0.0",
        "sass": "^1.70.0",
        "sass-embedded": "^1.70.0",
        "stylus": ">=0.54.8",
        "sugarss": "^5.0.0",
        "terser": "^5.16.0",
        "tsx": "^4.8.1",
        "yaml": "^2.4.2"
      },
      "peerDependenciesMeta": {
        "@types/node": {
          "optional": true
        },
        "@vitejs/devtools": {
          "optional": true
        },
        "esbuild": {
          "optional": true
        },
        "jiti": {
          "optional": true
        },
        "less": {
          "optional": true
        },
        "sass": {
          "optional": true
        },
        "sass-embedded": {
          "optional": true
        },
        "stylus": {
          "optional": true
        },
        "sugarss": {
          "optional": true
        },
        "terser": {
          "optional": true
        },
        "tsx": {
          "optional": true
        },
        "yaml": {
          "optional": true
        }
      }
    },
    "node_modules/which": {
      "version": "2.0.2",
      "resolved": "https://registry.npmjs.org/which/-/which-2.0.2.tgz",
      "integrity": "sha512-BLI3Tl1TW3Pvl70l3yq3Y64i+awpwXqsGBYWkkqMtnbXgrMD+yj7rhW0kuEDxzJaYXGjEW5ogapKNMEKNMjibA==",
      "dev": true,
      "license": "ISC",
      "dependencies": {
        "isexe": "^2.0.0"
      },
      "bin": {
        "node-which": "bin/node-which"
      },
      "engines": {
        "node": ">= 8"
      }
    },
    "node_modules/word-wrap": {
      "version": "1.2.5",
      "resolved": "https://registry.npmjs.org/word-wrap/-/word-wrap-1.2.5.tgz",
      "integrity": "sha512-BN22B5eaMMI9UMtjrGd5g5eCYPpCPDUy0FJXbYsaT5zYxjFOckS53SQDE3pWkVoWpHXVb3BrYcEN4Twa55B5cA==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=0.10.0"
      }
    },
    "node_modules/yallist": {
      "version": "3.1.1",
      "resolved": "https://registry.npmjs.org/yallist/-/yallist-3.1.1.tgz",
      "integrity": "sha512-a4UGQaWPH59mOXUYnAG2ewncQS4i4F43Tv3JoAM+s2VDAmS9NsK8GpDMLrCHPksFT7h3K6TOoUNn2pb7RoXx4g==",
      "dev": true,
      "license": "ISC"
    },
    "node_modules/yocto-queue": {
      "version": "0.1.0",
      "resolved": "https://registry.npmjs.org/yocto-queue/-/yocto-queue-0.1.0.tgz",
      "integrity": "sha512-rVksvsnNCdJ/ohGc6xgPwyN8eheCxsiLM8mxuE/t/mOVqJewPuO1miLpTHQiRgTKCLexL4MeAFVagts7HmNZ2Q==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=10"
      },
      "funding": {
        "url": "https://github.com/sponsors/sindresorhus"
      }
    },
    "node_modules/zod": {
      "version": "4.4.1",
      "resolved": "https://registry.npmjs.org/zod/-/zod-4.4.1.tgz",
      "integrity": "sha512-a6ENMBBGZBsnlSebQ/eKCguSBeGKSf4O7BPnqVPmYGtpBYI7VSqoVqw+QcB7kPRjbqPwhYTpFbVj/RqNz/CT0Q==",
      "dev": true,
      "license": "MIT",
      "peer": true,
      "funding": {
        "url": "https://github.com/sponsors/colinhacks"
      }
    },
    "node_modules/zod-validation-error": {
      "version": "4.0.2",
      "resolved": "https://registry.npmjs.org/zod-validation-error/-/zod-validation-error-4.0.2.tgz",
      "integrity": "sha512-Q6/nZLe6jxuU80qb/4uJ4t5v2VEZ44lzQjPDhYJNztRQ4wyWc6VF3D3Kb/fAuPetZQnhS3hnajCf9CsWesghLQ==",
      "dev": true,
      "license": "MIT",
      "engines": {
        "node": ">=18.0.0"
      },
      "peerDependencies": {
        "zod": "^3.25.0 || ^4.0.0"
      }
    }
  }
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\public`

- Project folder.

### File: `frontend\agpolicy-client\public\favicon.svg`

- Purpose: Project file.
- Note: SVG is XML-based vector markup, so it can be pasted and read as text.

~~~xml
<svg xmlns="http://www.w3.org/2000/svg" width="48" height="46" fill="none" viewBox="0 0 48 46"><path fill="#863bff" d="M25.946 44.938c-.664.845-2.021.375-2.021-.698V33.937a2.26 2.26 0 0 0-2.262-2.262H10.287c-.92 0-1.456-1.04-.92-1.788l7.48-10.471c1.07-1.497 0-3.578-1.842-3.578H1.237c-.92 0-1.456-1.04-.92-1.788L10.013.474c.214-.297.556-.474.92-.474h28.894c.92 0 1.456 1.04.92 1.788l-7.48 10.471c-1.07 1.498 0 3.579 1.842 3.579h11.377c.943 0 1.473 1.088.89 1.83L25.947 44.94z" style="fill:#863bff;fill:color(display-p3 .5252 .23 1);fill-opacity:1"/><mask id="a" width="48" height="46" x="0" y="0" maskUnits="userSpaceOnUse" style="mask-type:alpha"><path fill="#000" d="M25.842 44.938c-.664.844-2.021.375-2.021-.698V33.937a2.26 2.26 0 0 0-2.262-2.262H10.183c-.92 0-1.456-1.04-.92-1.788l7.48-10.471c1.07-1.498 0-3.579-1.842-3.579H1.133c-.92 0-1.456-1.04-.92-1.787L9.91.473c.214-.297.556-.474.92-.474h28.894c.92 0 1.456 1.04.92 1.788l-7.48 10.471c-1.07 1.498 0 3.578 1.842 3.578h11.377c.943 0 1.473 1.088.89 1.832L25.843 44.94z" style="fill:#000;fill-opacity:1"/></mask><g mask="url(#a)"><g filter="url(#b)"><ellipse cx="5.508" cy="14.704" fill="#ede6ff" rx="5.508" ry="14.704" style="fill:#ede6ff;fill:color(display-p3 .9275 .9033 1);fill-opacity:1" transform="matrix(.00324 1 1 -.00324 -4.47 31.516)"/></g><g filter="url(#c)"><ellipse cx="10.399" cy="29.851" fill="#ede6ff" rx="10.399" ry="29.851" style="fill:#ede6ff;fill:color(display-p3 .9275 .9033 1);fill-opacity:1" transform="matrix(.00324 1 1 -.00324 -39.328 7.883)"/></g><g filter="url(#d)"><ellipse cx="5.508" cy="30.487" fill="#7e14ff" rx="5.508" ry="30.487" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(89.814 -25.913 -14.639)scale(1 -1)"/></g><g filter="url(#e)"><ellipse cx="5.508" cy="30.599" fill="#7e14ff" rx="5.508" ry="30.599" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(89.814 -32.644 -3.334)scale(1 -1)"/></g><g filter="url(#f)"><ellipse cx="5.508" cy="30.599" fill="#7e14ff" rx="5.508" ry="30.599" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="matrix(.00324 1 1 -.00324 -34.34 30.47)"/></g><g filter="url(#g)"><ellipse cx="14.072" cy="22.078" fill="#ede6ff" rx="14.072" ry="22.078" style="fill:#ede6ff;fill:color(display-p3 .9275 .9033 1);fill-opacity:1" transform="rotate(93.35 24.506 48.493)scale(-1 1)"/></g><g filter="url(#h)"><ellipse cx="3.47" cy="21.501" fill="#7e14ff" rx="3.47" ry="21.501" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(89.009 28.708 47.59)scale(-1 1)"/></g><g filter="url(#i)"><ellipse cx="3.47" cy="21.501" fill="#7e14ff" rx="3.47" ry="21.501" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(89.009 28.708 47.59)scale(-1 1)"/></g><g filter="url(#j)"><ellipse cx=".387" cy="8.972" fill="#7e14ff" rx="4.407" ry="29.108" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(39.51 .387 8.972)"/></g><g filter="url(#k)"><ellipse cx="47.523" cy="-6.092" fill="#7e14ff" rx="4.407" ry="29.108" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(37.892 47.523 -6.092)"/></g><g filter="url(#l)"><ellipse cx="41.412" cy="6.333" fill="#47bfff" rx="5.971" ry="9.665" style="fill:#47bfff;fill:color(display-p3 .2799 .748 1);fill-opacity:1" transform="rotate(37.892 41.412 6.333)"/></g><g filter="url(#m)"><ellipse cx="-1.879" cy="38.332" fill="#7e14ff" rx="4.407" ry="29.108" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(37.892 -1.88 38.332)"/></g><g filter="url(#n)"><ellipse cx="-1.879" cy="38.332" fill="#7e14ff" rx="4.407" ry="29.108" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(37.892 -1.88 38.332)"/></g><g filter="url(#o)"><ellipse cx="35.651" cy="29.907" fill="#7e14ff" rx="4.407" ry="29.108" style="fill:#7e14ff;fill:color(display-p3 .4922 .0767 1);fill-opacity:1" transform="rotate(37.892 35.651 29.907)"/></g><g filter="url(#p)"><ellipse cx="38.418" cy="32.4" fill="#47bfff" rx="5.971" ry="15.297" style="fill:#47bfff;fill:color(display-p3 .2799 .748 1);fill-opacity:1" transform="rotate(37.892 38.418 32.4)"/></g></g><defs><filter id="b" width="60.045" height="41.654" x="-19.77" y="16.149" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="7.659"/></filter><filter id="c" width="90.34" height="51.437" x="-54.613" y="-7.533" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="7.659"/></filter><filter id="d" width="79.355" height="29.4" x="-49.64" y="2.03" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="e" width="79.579" height="29.4" x="-45.045" y="20.029" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="f" width="79.579" height="29.4" x="-43.513" y="21.178" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="g" width="74.749" height="58.852" x="15.756" y="-17.901" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="7.659"/></filter><filter id="h" width="61.377" height="25.362" x="23.548" y="2.284" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="i" width="61.377" height="25.362" x="23.548" y="2.284" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="j" width="56.045" height="63.649" x="-27.636" y="-22.853" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="k" width="54.814" height="64.646" x="20.116" y="-38.415" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="l" width="33.541" height="35.313" x="24.641" y="-11.323" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="m" width="54.814" height="64.646" x="-29.286" y="6.009" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="n" width="54.814" height="64.646" x="-29.286" y="6.009" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="o" width="54.814" height="64.646" x="8.244" y="-2.416" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter><filter id="p" width="39.409" height="43.623" x="18.713" y="10.588" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17158" stdDeviation="4.596"/></filter></defs></svg>
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\public` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\public\icons.svg`

- Purpose: Project file.
- Note: SVG is XML-based vector markup, so it can be pasted and read as text.

~~~xml
<svg xmlns="http://www.w3.org/2000/svg">
  <symbol id="bluesky-icon" viewBox="0 0 16 17">
    <g clip-path="url(#bluesky-clip)"><path fill="#08060d" d="M7.75 7.735c-.693-1.348-2.58-3.86-4.334-5.097-1.68-1.187-2.32-.981-2.74-.79C.188 2.065.1 2.812.1 3.251s.241 3.602.398 4.13c.52 1.744 2.367 2.333 4.07 2.145-2.495.37-4.71 1.278-1.805 4.512 3.196 3.309 4.38-.71 4.987-2.746.608 2.036 1.307 5.91 4.93 2.746 2.72-2.746.747-4.143-1.747-4.512 1.702.189 3.55-.4 4.07-2.145.156-.528.397-3.691.397-4.13s-.088-1.186-.575-1.406c-.42-.19-1.06-.395-2.741.79-1.755 1.24-3.64 3.752-4.334 5.099"/></g>
    <defs><clipPath id="bluesky-clip"><path fill="#fff" d="M.1.85h15.3v15.3H.1z"/></clipPath></defs>
  </symbol>
  <symbol id="discord-icon" viewBox="0 0 20 19">
    <path fill="#08060d" d="M16.224 3.768a14.5 14.5 0 0 0-3.67-1.153c-.158.286-.343.67-.47.976a13.5 13.5 0 0 0-4.067 0c-.128-.306-.317-.69-.476-.976A14.4 14.4 0 0 0 3.868 3.77C1.546 7.28.916 10.703 1.231 14.077a14.7 14.7 0 0 0 4.5 2.306q.545-.748.965-1.587a9.5 9.5 0 0 1-1.518-.74q.191-.14.372-.293c2.927 1.369 6.107 1.369 8.999 0q.183.152.372.294-.723.437-1.52.74.418.838.963 1.588a14.6 14.6 0 0 0 4.504-2.308c.37-3.911-.63-7.302-2.644-10.309m-9.13 8.234c-.878 0-1.599-.82-1.599-1.82 0-.998.705-1.82 1.6-1.82.894 0 1.614.82 1.599 1.82.001 1-.705 1.82-1.6 1.82m5.91 0c-.878 0-1.599-.82-1.599-1.82 0-.998.705-1.82 1.6-1.82.893 0 1.614.82 1.599 1.82 0 1-.706 1.82-1.6 1.82"/>
  </symbol>
  <symbol id="documentation-icon" viewBox="0 0 21 20">
    <path fill="none" stroke="#aa3bff" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.35" d="m15.5 13.333 1.533 1.322c.645.555.967.833.967 1.178s-.322.623-.967 1.179L15.5 18.333m-3.333-5-1.534 1.322c-.644.555-.966.833-.966 1.178s.322.623.966 1.179l1.534 1.321"/>
    <path fill="none" stroke="#aa3bff" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.35" d="M17.167 10.836v-4.32c0-1.41 0-2.117-.224-2.68-.359-.906-1.118-1.621-2.08-1.96-.599-.21-1.349-.21-2.848-.21-2.623 0-3.935 0-4.983.369-1.684.591-3.013 1.842-3.641 3.428C3 6.449 3 7.684 3 10.154v2.122c0 2.558 0 3.838.706 4.726q.306.383.713.671c.76.536 1.79.64 3.581.66"/>
    <path fill="none" stroke="#aa3bff" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.35" d="M3 10a2.78 2.78 0 0 1 2.778-2.778c.555 0 1.209.097 1.748-.047.48-.129.854-.503.982-.982.145-.54.048-1.194.048-1.749a2.78 2.78 0 0 1 2.777-2.777"/>
  </symbol>
  <symbol id="github-icon" viewBox="0 0 19 19">
    <path fill="#08060d" fill-rule="evenodd" d="M9.356 1.85C5.05 1.85 1.57 5.356 1.57 9.694a7.84 7.84 0 0 0 5.324 7.44c.387.079.528-.168.528-.376 0-.182-.013-.805-.013-1.454-2.165.467-2.616-.935-2.616-.935-.349-.91-.864-1.143-.864-1.143-.71-.48.051-.48.051-.48.787.051 1.2.805 1.2.805.695 1.194 1.817.857 2.268.649.064-.507.27-.857.49-1.052-1.728-.182-3.545-.857-3.545-3.87 0-.857.31-1.558.8-2.104-.078-.195-.349-1 .077-2.078 0 0 .657-.208 2.14.805a7.5 7.5 0 0 1 1.946-.26c.657 0 1.328.092 1.946.26 1.483-1.013 2.14-.805 2.14-.805.426 1.078.155 1.883.078 2.078.502.546.799 1.247.799 2.104 0 3.013-1.818 3.675-3.558 3.87.284.247.528.714.528 1.454 0 1.052-.012 1.896-.012 2.156 0 .208.142.455.528.377a7.84 7.84 0 0 0 5.324-7.441c.013-4.338-3.48-7.844-7.773-7.844" clip-rule="evenodd"/>
  </symbol>
  <symbol id="social-icon" viewBox="0 0 20 20">
    <path fill="none" stroke="#aa3bff" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.35" d="M12.5 6.667a4.167 4.167 0 1 0-8.334 0 4.167 4.167 0 0 0 8.334 0"/>
    <path fill="none" stroke="#aa3bff" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.35" d="M2.5 16.667a5.833 5.833 0 0 1 8.75-5.053m3.837.474.513 1.035c.07.144.257.282.414.309l.93.155c.596.1.736.536.307.965l-.723.73a.64.64 0 0 0-.152.531l.207.903c.164.715-.213.991-.84.618l-.872-.52a.63.63 0 0 0-.577 0l-.872.52c-.624.373-1.003.094-.84-.618l.207-.903a.64.64 0 0 0-.152-.532l-.723-.729c-.426-.43-.289-.864.306-.964l.93-.156a.64.64 0 0 0 .412-.31l.513-1.034c.28-.562.735-.562 1.012 0"/>
  </symbol>
  <symbol id="x-icon" viewBox="0 0 19 19">
    <path fill="#08060d" fill-rule="evenodd" d="M1.893 1.98c.052.072 1.245 1.769 2.653 3.77l2.892 4.114c.183.261.333.48.333.486s-.068.089-.152.183l-.522.593-.765.867-3.597 4.087c-.375.426-.734.834-.798.905a1 1 0 0 0-.118.148c0 .01.236.017.664.017h.663l.729-.83c.4-.457.796-.906.879-.999a692 692 0 0 0 1.794-2.038c.034-.037.301-.34.594-.675l.551-.624.345-.392a7 7 0 0 1 .34-.374c.006 0 .93 1.306 2.052 2.903l2.084 2.965.045.063h2.275c1.87 0 2.273-.003 2.266-.021-.008-.02-1.098-1.572-3.894-5.547-2.013-2.862-2.28-3.246-2.273-3.266.008-.019.282-.332 2.085-2.38l2-2.274 1.567-1.782c.022-.028-.016-.03-.65-.03h-.674l-.3.342a871 871 0 0 1-1.782 2.025c-.067.075-.405.458-.75.852a100 100 0 0 1-.803.91c-.148.172-.299.344-.99 1.127-.304.343-.32.358-.345.327-.015-.019-.904-1.282-1.976-2.808L6.365 1.85H1.8zm1.782.91 8.078 11.294c.772 1.08 1.413 1.973 1.425 1.984.016.017.241.02 1.05.017l1.03-.004-2.694-3.766L7.796 5.75 5.722 2.852l-1.039-.004-1.039-.004z" clip-rule="evenodd"/>
  </symbol>
</svg>
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\public` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client`

- Project folder.

### File: `frontend\agpolicy-client\README.md`

- Purpose: Project file.
- Note: Markdown documentation for humans, not runtime code.

~~~markdown
# React + TypeScript + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Oxc](https://oxc.rs)
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/)

## React Compiler

The React Compiler is not enabled on this template because of its impact on dev & build performances. To add it, see [this documentation](https://react.dev/learn/react-compiler/installation).

## Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type-aware lint rules:

```js
export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...

      // Remove tseslint.configs.recommended and replace with this
      tseslint.configs.recommendedTypeChecked,
      // Alternatively, use this for stricter rules
      tseslint.configs.strictTypeChecked,
      // Optionally, add this for stylistic rules
      tseslint.configs.stylisticTypeChecked,

      // Other configs...
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```

You can also install [eslint-plugin-react-x](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-x) and [eslint-plugin-react-dom](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-dom) for React-specific lint rules:

```js
// eslint.config.js
import reactX from 'eslint-plugin-react-x'
import reactDom from 'eslint-plugin-react-dom'

export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...
      // Enable lint rules for React
      reactX.configs['recommended-typescript'],
      // Enable lint rules for React DOM
      reactDom.configs.recommended,
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\src\api`

- Project folder.

### File: `frontend\agpolicy-client\src\api\claimsApi.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
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
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\api\farmersApi.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
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
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\api\http.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5056/api'

interface ApiErrorResponse {
  message?: string
}

export async function apiRequest<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...init,
    headers: {
      'Content-Type': 'application/json',
      ...init?.headers,
    },
  })

  if (!response.ok) {
    let message = `Request failed with ${response.status}`

    try {
      const error = (await response.json()) as ApiErrorResponse
      message = error.message || message
    } catch {
      message = response.statusText || message
    }

    throw new Error(message)
  }

  if (response.status === 204) {
    return undefined as T
  }

  return response.json() as Promise<T>
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\api\policiesApi.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
import { apiRequest } from './http'
import type { Policy } from '../types/policy'

export function getPolicies(): Promise<Policy[]> {
  return apiRequest<Policy[]>('/policies')
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\api\quotesApi.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
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
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\api` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\src`

- Project folder.

### File: `frontend\agpolicy-client\src\App.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
import { useMemo, useState } from 'react'
import { ClaimsPage } from './pages/ClaimsPage'
import { DashboardPage } from './pages/DashboardPage'
import { FarmersPage } from './pages/FarmersPage'
import { PoliciesPage } from './pages/PoliciesPage'
import { QuotesPage } from './pages/QuotesPage'

type PageId = 'dashboard' | 'farmers' | 'quotes' | 'policies' | 'claims'

const navItems: Array<{ id: PageId; label: string }> = [
  { id: 'dashboard', label: 'Dashboard' },
  { id: 'farmers', label: 'Farmers' },
  { id: 'quotes', label: 'Quotes' },
  { id: 'policies', label: 'Policies' },
  { id: 'claims', label: 'Claims' },
]

function App() {
  const [activePage, setActivePage] = useState<PageId>('dashboard')
  const [refreshKey, setRefreshKey] = useState(0)

  const page = useMemo(() => {
    const onChanged = () => setRefreshKey((key) => key + 1)

    switch (activePage) {
      case 'farmers':
        return <FarmersPage refreshKey={refreshKey} onChanged={onChanged} />
      case 'quotes':
        return <QuotesPage refreshKey={refreshKey} onChanged={onChanged} />
      case 'policies':
        return <PoliciesPage refreshKey={refreshKey} />
      case 'claims':
        return <ClaimsPage refreshKey={refreshKey} onChanged={onChanged} />
      default:
        return <DashboardPage refreshKey={refreshKey} />
    }
  }, [activePage, refreshKey])

  return (
    <div className="app-shell">
      <header className="topbar">
        <div>
          <p className="eyebrow">AgPolicy Manager</p>
          <h1>Crop insurance operations</h1>
        </div>
        <nav aria-label="Primary navigation">
          {navItems.map((item) => (
            <button
              key={item.id}
              className={activePage === item.id ? 'active' : ''}
              type="button"
              onClick={() => setActivePage(item.id)}
            >
              {item.label}
            </button>
          ))}
        </nav>
      </header>
      <main>{page}</main>
    </div>
  )
}

export default App
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\src\assets`

- Project folder.

### File: `frontend\agpolicy-client\src\assets\hero.png`

- Purpose: Project file.
- Note: PNG is binary image data, so the guide records its purpose instead of embedding unreadable bytes.

- Code/content: binary asset, not embedded as text.

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\assets` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\assets\react.svg`

- Purpose: Project file.
- Note: SVG is XML-based vector markup, so it can be pasted and read as text.

~~~xml
<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" class="iconify iconify--logos" width="35.93" height="32" preserveAspectRatio="xMidYMid meet" viewBox="0 0 256 228"><path fill="#00D8FF" d="M210.483 73.824a171.49 171.49 0 0 0-8.24-2.597c.465-1.9.893-3.777 1.273-5.621c6.238-30.281 2.16-54.676-11.769-62.708c-13.355-7.7-35.196.329-57.254 19.526a171.23 171.23 0 0 0-6.375 5.848a155.866 155.866 0 0 0-4.241-3.917C100.759 3.829 77.587-4.822 63.673 3.233C50.33 10.957 46.379 33.89 51.995 62.588a170.974 170.974 0 0 0 1.892 8.48c-3.28.932-6.445 1.924-9.474 2.98C17.309 83.498 0 98.307 0 113.668c0 15.865 18.582 31.778 46.812 41.427a145.52 145.52 0 0 0 6.921 2.165a167.467 167.467 0 0 0-2.01 9.138c-5.354 28.2-1.173 50.591 12.134 58.266c13.744 7.926 36.812-.22 59.273-19.855a145.567 145.567 0 0 0 5.342-4.923a168.064 168.064 0 0 0 6.92 6.314c21.758 18.722 43.246 26.282 56.54 18.586c13.731-7.949 18.194-32.003 12.4-61.268a145.016 145.016 0 0 0-1.535-6.842c1.62-.48 3.21-.974 4.76-1.488c29.348-9.723 48.443-25.443 48.443-41.52c0-15.417-17.868-30.326-45.517-39.844Zm-6.365 70.984c-1.4.463-2.836.91-4.3 1.345c-3.24-10.257-7.612-21.163-12.963-32.432c5.106-11 9.31-21.767 12.459-31.957c2.619.758 5.16 1.557 7.61 2.4c23.69 8.156 38.14 20.213 38.14 29.504c0 9.896-15.606 22.743-40.946 31.14Zm-10.514 20.834c2.562 12.94 2.927 24.64 1.23 33.787c-1.524 8.219-4.59 13.698-8.382 15.893c-8.067 4.67-25.32-1.4-43.927-17.412a156.726 156.726 0 0 1-6.437-5.87c7.214-7.889 14.423-17.06 21.459-27.246c12.376-1.098 24.068-2.894 34.671-5.345a134.17 134.17 0 0 1 1.386 6.193ZM87.276 214.515c-7.882 2.783-14.16 2.863-17.955.675c-8.075-4.657-11.432-22.636-6.853-46.752a156.923 156.923 0 0 1 1.869-8.499c10.486 2.32 22.093 3.988 34.498 4.994c7.084 9.967 14.501 19.128 21.976 27.15a134.668 134.668 0 0 1-4.877 4.492c-9.933 8.682-19.886 14.842-28.658 17.94ZM50.35 144.747c-12.483-4.267-22.792-9.812-29.858-15.863c-6.35-5.437-9.555-10.836-9.555-15.216c0-9.322 13.897-21.212 37.076-29.293c2.813-.98 5.757-1.905 8.812-2.773c3.204 10.42 7.406 21.315 12.477 32.332c-5.137 11.18-9.399 22.249-12.634 32.792a134.718 134.718 0 0 1-6.318-1.979Zm12.378-84.26c-4.811-24.587-1.616-43.134 6.425-47.789c8.564-4.958 27.502 2.111 47.463 19.835a144.318 144.318 0 0 1 3.841 3.545c-7.438 7.987-14.787 17.08-21.808 26.988c-12.04 1.116-23.565 2.908-34.161 5.309a160.342 160.342 0 0 1-1.76-7.887Zm110.427 27.268a347.8 347.8 0 0 0-7.785-12.803c8.168 1.033 15.994 2.404 23.343 4.08c-2.206 7.072-4.956 14.465-8.193 22.045a381.151 381.151 0 0 0-7.365-13.322Zm-45.032-43.861c5.044 5.465 10.096 11.566 15.065 18.186a322.04 322.04 0 0 0-30.257-.006c4.974-6.559 10.069-12.652 15.192-18.18ZM82.802 87.83a323.167 323.167 0 0 0-7.227 13.238c-3.184-7.553-5.909-14.98-8.134-22.152c7.304-1.634 15.093-2.97 23.209-3.984a321.524 321.524 0 0 0-7.848 12.897Zm8.081 65.352c-8.385-.936-16.291-2.203-23.593-3.793c2.26-7.3 5.045-14.885 8.298-22.6a321.187 321.187 0 0 0 7.257 13.246c2.594 4.48 5.28 8.868 8.038 13.147Zm37.542 31.03c-5.184-5.592-10.354-11.779-15.403-18.433c4.902.192 9.899.29 14.978.29c5.218 0 10.376-.117 15.453-.343c-4.985 6.774-10.018 12.97-15.028 18.486Zm52.198-57.817c3.422 7.8 6.306 15.345 8.596 22.52c-7.422 1.694-15.436 3.058-23.88 4.071a382.417 382.417 0 0 0 7.859-13.026a347.403 347.403 0 0 0 7.425-13.565Zm-16.898 8.101a358.557 358.557 0 0 1-12.281 19.815a329.4 329.4 0 0 1-23.444.823c-7.967 0-15.716-.248-23.178-.732a310.202 310.202 0 0 1-12.513-19.846h.001a307.41 307.41 0 0 1-10.923-20.627a310.278 310.278 0 0 1 10.89-20.637l-.001.001a307.318 307.318 0 0 1 12.413-19.761c7.613-.576 15.42-.876 23.31-.876H128c7.926 0 15.743.303 23.354.883a329.357 329.357 0 0 1 12.335 19.695a358.489 358.489 0 0 1 11.036 20.54a329.472 329.472 0 0 1-11 20.722Zm22.56-122.124c8.572 4.944 11.906 24.881 6.52 51.026c-.344 1.668-.73 3.367-1.15 5.09c-10.622-2.452-22.155-4.275-34.23-5.408c-7.034-10.017-14.323-19.124-21.64-27.008a160.789 160.789 0 0 1 5.888-5.4c18.9-16.447 36.564-22.941 44.612-18.3ZM128 90.808c12.625 0 22.86 10.235 22.86 22.86s-10.235 22.86-22.86 22.86s-22.86-10.235-22.86-22.86s10.235-22.86 22.86-22.86Z"></path></svg>
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\assets` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\assets\vite.svg`

- Purpose: Project file.
- Note: SVG is XML-based vector markup, so it can be pasted and read as text.

~~~xml
<svg xmlns="http://www.w3.org/2000/svg" width="77" height="47" fill="none" aria-labelledby="vite-logo-title" viewBox="0 0 77 47"><title id="vite-logo-title">Vite</title><style>.parenthesis{fill:#000}@media (prefers-color-scheme:dark){.parenthesis{fill:#fff}}</style><path fill="#9135ff" d="M40.151 45.71c-.663.844-2.02.374-2.02-.699V34.708a2.26 2.26 0 0 0-2.262-2.262H24.493c-.92 0-1.457-1.04-.92-1.788l7.479-10.471c1.07-1.498 0-3.578-1.842-3.578H15.443c-.92 0-1.456-1.04-.92-1.788l9.696-13.576c.213-.297.556-.474.92-.474h28.894c.92 0 1.456 1.04.92 1.788l-7.48 10.472c-1.07 1.497 0 3.578 1.842 3.578h11.376c.944 0 1.474 1.087.89 1.83L40.153 45.712z"/><mask id="a" width="48" height="47" x="14" y="0" maskUnits="userSpaceOnUse" style="mask-type:alpha"><path fill="#000" d="M40.047 45.71c-.663.843-2.02.374-2.02-.699V34.708a2.26 2.26 0 0 0-2.262-2.262H24.389c-.92 0-1.457-1.04-.92-1.788l7.479-10.472c1.07-1.497 0-3.578-1.842-3.578H15.34c-.92 0-1.456-1.04-.92-1.788l9.696-13.575c.213-.297.556-.474.92-.474H53.93c.92 0 1.456 1.04.92 1.788L47.37 13.03c-1.07 1.498 0 3.578 1.842 3.578h11.376c.944 0 1.474 1.088.89 1.831L40.049 45.712z"/></mask><g mask="url(#a)"><g filter="url(#b)"><ellipse cx="5.508" cy="14.704" fill="#eee6ff" rx="5.508" ry="14.704" transform="rotate(269.814 20.96 11.29)scale(-1 1)"/></g><g filter="url(#c)"><ellipse cx="10.399" cy="29.851" fill="#eee6ff" rx="10.399" ry="29.851" transform="rotate(89.814 -16.902 -8.275)scale(1 -1)"/></g><g filter="url(#d)"><ellipse cx="5.508" cy="30.487" fill="#8900ff" rx="5.508" ry="30.487" transform="rotate(89.814 -19.197 -7.127)scale(1 -1)"/></g><g filter="url(#e)"><ellipse cx="5.508" cy="30.599" fill="#8900ff" rx="5.508" ry="30.599" transform="rotate(89.814 -25.928 4.177)scale(1 -1)"/></g><g filter="url(#f)"><ellipse cx="5.508" cy="30.599" fill="#8900ff" rx="5.508" ry="30.599" transform="rotate(89.814 -25.738 5.52)scale(1 -1)"/></g><g filter="url(#g)"><ellipse cx="14.072" cy="22.078" fill="#eee6ff" rx="14.072" ry="22.078" transform="rotate(93.35 31.245 55.578)scale(-1 1)"/></g><g filter="url(#h)"><ellipse cx="3.47" cy="21.501" fill="#8900ff" rx="3.47" ry="21.501" transform="rotate(89.009 35.419 55.202)scale(-1 1)"/></g><g filter="url(#i)"><ellipse cx="3.47" cy="21.501" fill="#8900ff" rx="3.47" ry="21.501" transform="rotate(89.009 35.419 55.202)scale(-1 1)"/></g><g filter="url(#j)"><ellipse cx="14.592" cy="9.743" fill="#8900ff" rx="4.407" ry="29.108" transform="rotate(39.51 14.592 9.743)"/></g><g filter="url(#k)"><ellipse cx="61.728" cy="-5.321" fill="#8900ff" rx="4.407" ry="29.108" transform="rotate(37.892 61.728 -5.32)"/></g><g filter="url(#l)"><ellipse cx="55.618" cy="7.104" fill="#00c2ff" rx="5.971" ry="9.665" transform="rotate(37.892 55.618 7.104)"/></g><g filter="url(#m)"><ellipse cx="12.326" cy="39.103" fill="#8900ff" rx="4.407" ry="29.108" transform="rotate(37.892 12.326 39.103)"/></g><g filter="url(#n)"><ellipse cx="12.326" cy="39.103" fill="#8900ff" rx="4.407" ry="29.108" transform="rotate(37.892 12.326 39.103)"/></g><g filter="url(#o)"><ellipse cx="49.857" cy="30.678" fill="#8900ff" rx="4.407" ry="29.108" transform="rotate(37.892 49.857 30.678)"/></g><g filter="url(#p)"><ellipse cx="52.623" cy="33.171" fill="#00c2ff" rx="5.971" ry="15.297" transform="rotate(37.892 52.623 33.17)"/></g></g><path d="M6.919 0c-9.198 13.166-9.252 33.575 0 46.789h6.215c-9.25-13.214-9.196-33.623 0-46.789zm62.424 0h-6.215c9.198 13.166 9.252 33.575 0 46.789h6.215c9.25-13.214 9.196-33.623 0-46.789" class="parenthesis"/><defs><filter id="b" width="60.045" height="41.654" x="-5.564" y="16.92" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="7.659"/></filter><filter id="c" width="90.34" height="51.437" x="-40.407" y="-6.762" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="7.659"/></filter><filter id="d" width="79.355" height="29.4" x="-35.435" y="2.801" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="e" width="79.579" height="29.4" x="-30.84" y="20.8" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="f" width="79.579" height="29.4" x="-29.307" y="21.949" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="g" width="74.749" height="58.852" x="29.961" y="-17.13" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="7.659"/></filter><filter id="h" width="61.377" height="25.362" x="37.754" y="3.055" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="i" width="61.377" height="25.362" x="37.754" y="3.055" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="j" width="56.045" height="63.649" x="-13.43" y="-22.082" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="k" width="54.814" height="64.646" x="34.321" y="-37.644" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="l" width="33.541" height="35.313" x="38.847" y="-10.552" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="m" width="54.814" height="64.646" x="-15.081" y="6.78" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="n" width="54.814" height="64.646" x="-15.081" y="6.78" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="o" width="54.814" height="64.646" x="22.45" y="-1.645" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter><filter id="p" width="39.409" height="43.623" x="32.919" y="11.36" color-interpolation-filters="sRGB" filterUnits="userSpaceOnUse"><feFlood flood-opacity="0" result="BackgroundImageFix"/><feBlend in="SourceGraphic" in2="BackgroundImageFix" result="shape"/><feGaussianBlur result="effect1_foregroundBlur_2002_17286" stdDeviation="4.596"/></filter></defs></svg>
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\assets` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\src\components`

- Project folder.

### File: `frontend\agpolicy-client\src\components\EmptyState.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
interface EmptyStateProps {
  message: string
}

export function EmptyState({ message }: EmptyStateProps) {
  return <p className="empty-state">{message}</p>
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\components` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\components\Field.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
import type { ReactNode } from 'react'

interface FieldProps {
  label: string
  children: ReactNode
}

export function Field({ label, children }: FieldProps) {
  return (
    <label className="field">
      <span>{label}</span>
      {children}
    </label>
  )
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\components` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\components\PageHeader.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
interface PageHeaderProps {
  title: string
  description: string
}

export function PageHeader({ title, description }: PageHeaderProps) {
  return (
    <div className="page-header">
      <h2>{title}</h2>
      <p>{description}</p>
    </div>
  )
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\components` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\components\StatGrid.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
interface StatItem {
  label: string
  value: string | number
}

interface StatGridProps {
  items: StatItem[]
}

export function StatGrid({ items }: StatGridProps) {
  return (
    <div className="stat-grid">
      {items.map((item) => (
        <div className="stat-card" key={item.label}>
          <span>{item.label}</span>
          <strong>{item.value}</strong>
        </div>
      ))}
    </div>
  )
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\components` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\components\StatusMessage.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
interface StatusMessageProps {
  message?: string
  tone?: 'success' | 'error' | 'info'
}

export function StatusMessage({ message, tone = 'info' }: StatusMessageProps) {
  if (!message) {
    return null
  }

  return <p className={`status-message ${tone}`}>{message}</p>
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\components` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\components\useAsyncData.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
import { useEffect, useRef, useState } from 'react'

export function useAsyncData<T>(loader: () => Promise<T>, reloadKey: unknown) {
  const [data, setData] = useState<T | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')
  const loaderRef = useRef(loader)

  useEffect(() => {
    loaderRef.current = loader
  }, [loader])

  useEffect(() => {
    let ignore = false

    async function load() {
      setLoading(true)
      setError('')

      try {
        const result = await loaderRef.current()
        if (!ignore) {
          setData(result)
        }
      } catch (err) {
        if (!ignore) {
          setError(err instanceof Error ? err.message : 'Unable to load data.')
        }
      } finally {
        if (!ignore) {
          setLoading(false)
        }
      }
    }

    void load()

    return () => {
      ignore = true
    }
  }, [reloadKey])

  return { data, loading, error }
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\components` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\src`

- Project folder.

### File: `frontend\agpolicy-client\src\index.css`

- Purpose: Project file.
- Note: Plain text/configuration file used by project tooling.

~~~css
:root {
  --bg: #f7f8f3;
  --surface: #ffffff;
  --surface-strong: #edf2e8;
  --text: #23312a;
  --muted: #69756e;
  --border: #d8ded5;
  --primary: #276749;
  --primary-dark: #1f513a;
  --accent: #b8872e;
  --danger: #a43f3f;
  --success: #2f7d46;
  --shadow: 0 10px 28px rgba(35, 49, 42, 0.08);
  font-family: Inter, ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
  color: var(--text);
  background: var(--bg);
  font-synthesis: none;
  text-rendering: optimizeLegibility;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

* {
  box-sizing: border-box;
}

body {
  margin: 0;
}

button,
input,
select {
  font: inherit;
}

button {
  border: 0;
  border-radius: 6px;
  background: var(--primary);
  color: #fff;
  cursor: pointer;
  font-weight: 700;
  min-height: 42px;
  padding: 0 16px;
}

button:hover:not(:disabled) {
  background: var(--primary-dark);
}

button:disabled {
  cursor: not-allowed;
  opacity: 0.55;
}

input,
select {
  width: 100%;
  min-height: 40px;
  border: 1px solid var(--border);
  border-radius: 6px;
  background: #fff;
  color: var(--text);
  padding: 8px 10px;
}

table {
  width: 100%;
  border-collapse: collapse;
  font-size: 0.94rem;
}

th,
td {
  border-bottom: 1px solid var(--border);
  padding: 12px;
  text-align: left;
  white-space: nowrap;
}

th {
  color: var(--muted);
  font-size: 0.78rem;
  text-transform: uppercase;
}

.app-shell {
  width: min(1280px, calc(100% - 32px));
  margin: 0 auto;
  padding: 24px 0 48px;
}

.topbar {
  display: flex;
  align-items: end;
  justify-content: space-between;
  gap: 24px;
  margin-bottom: 24px;
}

.eyebrow {
  color: var(--accent);
  font-size: 0.78rem;
  font-weight: 800;
  letter-spacing: 0;
  margin: 0 0 4px;
  text-transform: uppercase;
}

h1,
h2,
h3,
p {
  margin-top: 0;
}

h1 {
  font-size: 2rem;
  line-height: 1.15;
  margin-bottom: 0;
}

h2 {
  font-size: 1.55rem;
  margin-bottom: 6px;
}

h3 {
  font-size: 1.05rem;
  margin-bottom: 16px;
}

nav {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
}

nav button {
  background: var(--surface);
  border: 1px solid var(--border);
  color: var(--text);
}

nav button.active {
  background: var(--primary);
  color: #fff;
}

.page-section {
  display: grid;
  gap: 18px;
}

.page-header p,
.empty-state,
.field span {
  color: var(--muted);
}

.panel {
  background: var(--surface);
  border: 1px solid var(--border);
  border-radius: 8px;
  box-shadow: var(--shadow);
  padding: 18px;
}

.two-column {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 18px;
}

.form-grid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 14px;
  margin-bottom: 16px;
}

.form-grid.wide {
  grid-template-columns: repeat(3, minmax(0, 1fr));
}

.field {
  display: grid;
  gap: 6px;
  font-size: 0.9rem;
  font-weight: 700;
}

.readonly-field {
  display: grid;
  gap: 6px;
  min-height: 68px;
}

.readonly-field span {
  color: var(--muted);
  font-size: 0.9rem;
  font-weight: 700;
}

.readonly-field strong {
  align-items: center;
  background: var(--surface-strong);
  border: 1px solid var(--border);
  border-radius: 6px;
  display: flex;
  min-height: 40px;
  padding: 8px 10px;
}

.status-message {
  border-radius: 6px;
  font-weight: 700;
  margin: 0;
  padding: 10px 12px;
}

.status-message.error {
  background: #fae9e9;
  color: var(--danger);
}

.status-message.success {
  background: #e8f5ec;
  color: var(--success);
}

.status-message.info,
.empty-state {
  background: var(--surface-strong);
  color: var(--muted);
}

.empty-state {
  border-radius: 6px;
  margin: 0;
  padding: 12px;
}

.table-wrap {
  overflow-x: auto;
}

.selected-row {
  background: #f5efd9;
}

.badge {
  display: inline-flex;
  align-items: center;
  min-height: 24px;
  border-radius: 999px;
  background: var(--surface-strong);
  color: var(--primary-dark);
  font-size: 0.82rem;
  font-weight: 800;
  padding: 2px 10px;
}

.detail-list {
  display: grid;
  gap: 10px;
}

.detail-list p {
  display: flex;
  justify-content: space-between;
  gap: 12px;
  margin: 0;
}

.detail-list span {
  color: var(--muted);
}

.stat-grid {
  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  gap: 14px;
}

.stat-card {
  background: var(--surface);
  border: 1px solid var(--border);
  border-radius: 8px;
  box-shadow: var(--shadow);
  padding: 16px;
}

.stat-card span {
  color: var(--muted);
  display: block;
  font-size: 0.82rem;
  font-weight: 800;
  margin-bottom: 8px;
  text-transform: uppercase;
}

.stat-card strong {
  font-size: 1.45rem;
}

@media (max-width: 900px) {
  .topbar,
  .two-column {
    grid-template-columns: 1fr;
    display: grid;
    align-items: start;
  }

  .form-grid,
  .form-grid.wide,
  .stat-grid {
    grid-template-columns: 1fr;
  }

  .app-shell {
    width: min(100% - 20px, 1280px);
  }
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\main.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\src\pages`

- Project folder.

### File: `frontend\agpolicy-client\src\pages\ClaimsPage.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
import type { FormEvent } from 'react'
import { useState } from 'react'
import { createClaim, getClaims, updateClaimStatus } from '../api/claimsApi'
import { getPolicies } from '../api/policiesApi'
import { EmptyState } from '../components/EmptyState'
import { Field } from '../components/Field'
import { PageHeader } from '../components/PageHeader'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'

interface ClaimsPageProps {
  refreshKey: number
  onChanged: () => void
}

const claimStatuses = ['Open', 'InReview', 'Approved', 'Denied', 'Closed']

const blankClaim = {
  policyId: '',
  lossDate: '',
  lossReason: '',
  estimatedLossAmount: '',
  notes: '',
}

export function ClaimsPage({ refreshKey, onChanged }: ClaimsPageProps) {
  const { data: claims, loading, error } = useAsyncData(getClaims, refreshKey)
  const { data: policies } = useAsyncData(getPolicies, refreshKey)
  const [form, setForm] = useState(blankClaim)
  const [message, setMessage] = useState('')
  const [formError, setFormError] = useState('')

  async function submitClaim(event: FormEvent) {
    event.preventDefault()
    setMessage('')
    setFormError('')

    try {
      const created = await createClaim({
        policyId: Number(form.policyId),
        lossDate: form.lossDate,
        lossReason: form.lossReason,
        estimatedLossAmount: Number(form.estimatedLossAmount),
        notes: form.notes,
      })
      setForm(blankClaim)
      setMessage(`Claim ${created.id} opened.`)
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to create claim.')
    }
  }

  async function setStatus(id: number, status: string) {
    setMessage('')
    setFormError('')

    try {
      const updated = await updateClaimStatus(id, status)
      setMessage(`Claim ${updated.id} moved to ${updated.status}.`)
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to update claim status.')
    }
  }

  return (
    <section className="page-section">
      <PageHeader title="Claims" description="Open claims against active policies and manage claim status." />
      <StatusMessage message={error || formError} tone="error" />
      <StatusMessage message={message} tone="success" />

      <form className="panel" onSubmit={submitClaim}>
        <h3>Create claim</h3>
        <div className="form-grid wide">
          <Field label="Policy">
            <select required value={form.policyId} onChange={(event) => setForm({ ...form, policyId: event.target.value })}>
              <option value="">Select policy</option>
              {(policies ?? []).map((policy) => (
                <option key={policy.id} value={policy.id}>
                  Policy {policy.id} - {policy.cropType} - {currency(policy.premium)}
                </option>
              ))}
            </select>
          </Field>
          <Field label="Loss date">
            <input required type="date" value={form.lossDate} onChange={(event) => setForm({ ...form, lossDate: event.target.value })} />
          </Field>
          <Field label="Estimated loss">
            <input required min="0" step="0.01" type="number" value={form.estimatedLossAmount} onChange={(event) => setForm({ ...form, estimatedLossAmount: event.target.value })} />
          </Field>
          <Field label="Loss reason">
            <input required value={form.lossReason} onChange={(event) => setForm({ ...form, lossReason: event.target.value })} />
          </Field>
          <Field label="Notes">
            <input value={form.notes} onChange={(event) => setForm({ ...form, notes: event.target.value })} />
          </Field>
        </div>
        <button type="submit">Open claim</button>
      </form>

      <div className="panel">
        <h3>Claims</h3>
        {loading && <p>Loading claims...</p>}
        {!loading && claims?.length === 0 && <EmptyState message="No claims have been opened yet." />}
        {claims && claims.length > 0 && (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Claim</th>
                  <th>Policy</th>
                  <th>Loss date</th>
                  <th>Reason</th>
                  <th>Estimated loss</th>
                  <th>Status</th>
                  <th>Update</th>
                </tr>
              </thead>
              <tbody>
                {claims.map((claim) => (
                  <tr key={claim.id}>
                    <td>{claim.id}</td>
                    <td>{claim.policyId}</td>
                    <td>{new Date(claim.lossDate).toLocaleDateString()}</td>
                    <td>{claim.lossReason}</td>
                    <td>{currency(claim.estimatedLossAmount)}</td>
                    <td><span className="badge">{claim.status}</span></td>
                    <td>
                      <select value={claim.status} onChange={(event) => void setStatus(claim.id, event.target.value)}>
                        {claimStatuses.map((status) => (
                          <option key={status} value={status}>
                            {status}
                          </option>
                        ))}
                      </select>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </section>
  )
}

function currency(value: number) {
  return value.toLocaleString(undefined, { style: 'currency', currency: 'USD' })
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\pages` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\pages\DashboardPage.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
import { getClaims } from '../api/claimsApi'
import { getFarmers } from '../api/farmersApi'
import { getPolicies } from '../api/policiesApi'
import { getQuotes } from '../api/quotesApi'
import { EmptyState } from '../components/EmptyState'
import { PageHeader } from '../components/PageHeader'
import { StatGrid } from '../components/StatGrid'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'

interface DashboardPageProps {
  refreshKey: number
}

export function DashboardPage({ refreshKey }: DashboardPageProps) {
  const { data, loading, error } = useAsyncData(async () => {
    const [farmers, quotes, policies, claims] = await Promise.all([
      getFarmers(),
      getQuotes(),
      getPolicies(),
      getClaims(),
    ])

    return { farmers, quotes, policies, claims }
  }, refreshKey)

  const totalPremium = data?.policies.reduce((sum, policy) => sum + policy.premium, 0) ?? 0

  return (
    <section className="page-section">
      <PageHeader
        title="Dashboard"
        description="Operational snapshot for farmers, quotes, policies, and claims."
      />
      <StatusMessage message={error} tone="error" />
      {loading && <p>Loading dashboard...</p>}
      {data && (
        <>
          <StatGrid
            items={[
              { label: 'Farmers', value: data.farmers.length },
              { label: 'Quotes', value: data.quotes.length },
              { label: 'Active policies', value: data.policies.filter((policy) => policy.status === 'Active').length },
              { label: 'Total premium', value: currency(totalPremium) },
              { label: 'Open claims', value: data.claims.filter((claim) => claim.status === 'Open').length },
            ]}
          />
          {data.quotes.length === 0 && <EmptyState message="Create a farmer, farm, and quote to start the workflow." />}
        </>
      )}
    </section>
  )
}

function currency(value: number) {
  return value.toLocaleString(undefined, { style: 'currency', currency: 'USD' })
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\pages` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\pages\FarmersPage.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
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
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\pages` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\pages\PoliciesPage.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
import { getPolicies } from '../api/policiesApi'
import { EmptyState } from '../components/EmptyState'
import { PageHeader } from '../components/PageHeader'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'

interface PoliciesPageProps {
  refreshKey: number
}

export function PoliciesPage({ refreshKey }: PoliciesPageProps) {
  const { data: policies, loading, error } = useAsyncData(getPolicies, refreshKey)

  return (
    <section className="page-section">
      <PageHeader title="Policies" description="Active policies created from converted quotes." />
      <StatusMessage message={error} tone="error" />
      <div className="panel">
        {loading && <p>Loading policies...</p>}
        {!loading && policies?.length === 0 && <EmptyState message="No policies have been created yet." />}
        {policies && policies.length > 0 && (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Policy</th>
                  <th>Quote</th>
                  <th>Farmer</th>
                  <th>Farm</th>
                  <th>Crop</th>
                  <th>Acres</th>
                  <th>Premium</th>
                  <th>Status</th>
                  <th>Effective</th>
                  <th>Expires</th>
                </tr>
              </thead>
              <tbody>
                {policies.map((policy) => (
                  <tr key={policy.id}>
                    <td>{policy.id}</td>
                    <td>{policy.quoteId}</td>
                    <td>{policy.farmerName || `Farmer ${policy.farmerId}`}</td>
                    <td>{policy.farmName || `Farm ${policy.farmId}`}</td>
                    <td>{policy.cropType}</td>
                    <td>{policy.insuredAcres}</td>
                    <td>{currency(policy.premium)}</td>
                    <td><span className="badge">{policy.status}</span></td>
                    <td>{date(policy.effectiveDate)}</td>
                    <td>{date(policy.expirationDate)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </section>
  )
}

function date(value: string) {
  return new Date(value).toLocaleDateString()
}

function currency(value: number) {
  return value.toLocaleString(undefined, { style: 'currency', currency: 'USD' })
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\pages` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\pages\QuotesPage.tsx`

- Purpose: Project file.
- Note: TSX is TypeScript plus JSX markup. It fills the same role as a Vue single-file component template plus script section.
- Note: React controlled inputs use value plus onChange, similar in goal to Vue v-model but more explicit.
- Note: Props and API data are typed with TypeScript interfaces so mistakes are caught during npm run build.

~~~tsx
import type { FormEvent } from 'react'
import { useState } from 'react'
import { getFarmers, getFarms } from '../api/farmersApi'
import { convertQuote, createQuote, getQuotes } from '../api/quotesApi'
import { EmptyState } from '../components/EmptyState'
import { Field } from '../components/Field'
import { PageHeader } from '../components/PageHeader'
import { StatusMessage } from '../components/StatusMessage'
import { useAsyncData } from '../components/useAsyncData'
import type { Farm } from '../types/farmer'
import type { Quote } from '../types/quote'

interface QuotesPageProps {
  refreshKey: number
  onChanged: () => void
}

const cropTypes = ['Corn', 'Soybeans', 'Wheat', 'Cotton']
const coverageLevels = [50, 65, 75, 85]

const blankQuote = {
  farmerId: '',
  farmId: '',
  cropType: 'Corn',
  coverageLevel: '75',
}

export function QuotesPage({ refreshKey, onChanged }: QuotesPageProps) {
  const { data: quotes, loading, error } = useAsyncData(getQuotes, refreshKey)
  const { data: farmers } = useAsyncData(getFarmers, refreshKey)
  const [farms, setFarms] = useState<Farm[]>([])
  const [form, setForm] = useState(blankQuote)
  const [selectedQuote, setSelectedQuote] = useState<Quote | null>(null)
  const [message, setMessage] = useState('')
  const [formError, setFormError] = useState('')
  const selectedFarm = farms.find((farm) => String(farm.id) === form.farmId)

  async function farmerChanged(farmerId: string) {
    setForm({ ...form, farmerId, farmId: '' })
    setSelectedQuote(null)
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

  async function submitQuote(event: FormEvent) {
    event.preventDefault()
    setMessage('')
    setFormError('')

    try {
      const created = await createQuote({
        farmerId: Number(form.farmerId),
        farmId: Number(form.farmId),
        cropType: form.cropType,
        coverageLevel: Number(form.coverageLevel),
      })
      setSelectedQuote(created)
      setForm({ ...blankQuote, farmerId: form.farmerId })
      setMessage(`Quote ${created.id} created with estimated premium ${currency(created.estimatedPremium)}.`)
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to create quote.')
    }
  }

  async function convertSelectedQuote() {
    if (!selectedQuote) {
      return
    }

    setMessage('')
    setFormError('')

    try {
      const result = await convertQuote(selectedQuote.id)
      setMessage(`${result.message} Policy ${result.policyId} is now active.`)
      setSelectedQuote({ ...selectedQuote, status: 'Converted', convertedPolicyId: result.policyId })
      onChanged()
    } catch (err) {
      setFormError(err instanceof Error ? err.message : 'Unable to convert quote.')
    }
  }

  return (
    <section className="page-section">
      <PageHeader title="Quotes" description="Calculate premiums and convert accepted quotes into active policies." />
      <StatusMessage message={error || formError} tone="error" />
      <StatusMessage message={message} tone="success" />

      <div className="two-column">
        <form className="panel" onSubmit={submitQuote}>
          <h3>Create quote</h3>
          <div className="form-grid">
            <Field label="Farmer">
              <select required value={form.farmerId} onChange={(event) => void farmerChanged(event.target.value)}>
                <option value="">Select farmer</option>
                {(farmers ?? []).map((farmer) => (
                  <option key={farmer.id} value={farmer.id}>
                    {farmer.firstName} {farmer.lastName}
                  </option>
                ))}
              </select>
            </Field>
            <Field label="Farm">
              <select required value={form.farmId} onChange={(event) => setForm({ ...form, farmId: event.target.value })}>
                <option value="">Select farm</option>
                {farms.map((farm) => (
                  <option key={farm.id} value={farm.id}>
                    {farm.farmName} ({farm.acres} acres)
                  </option>
                ))}
              </select>
            </Field>
            <div className="readonly-field">
              <span>Quoted acres</span>
              <strong>{selectedFarm ? selectedFarm.acres : 'Select a farm'}</strong>
            </div>
            <Field label="Crop type">
              <select value={form.cropType} onChange={(event) => setForm({ ...form, cropType: event.target.value })}>
                {cropTypes.map((cropType) => (
                  <option key={cropType} value={cropType}>
                    {cropType}
                  </option>
                ))}
              </select>
            </Field>
            <Field label="Coverage level">
              <select value={form.coverageLevel} onChange={(event) => setForm({ ...form, coverageLevel: event.target.value })}>
                {coverageLevels.map((level) => (
                  <option key={level} value={level}>
                    {level}%
                  </option>
                ))}
              </select>
            </Field>
          </div>
          <button type="submit">Create quote</button>
        </form>

        <div className="panel">
          <h3>Quote details</h3>
          {!selectedQuote && <EmptyState message="Select a quote from the table or create a new one." />}
          {selectedQuote && (
            <div className="detail-list">
              <p><span>Quote</span><strong>{selectedQuote.id}</strong></p>
              <p><span>Crop</span><strong>{selectedQuote.cropType}</strong></p>
              <p><span>Acres</span><strong>{selectedQuote.acres}</strong></p>
              <p><span>Coverage</span><strong>{selectedQuote.coverageLevel}%</strong></p>
              <p><span>Premium</span><strong>{currency(selectedQuote.estimatedPremium)}</strong></p>
              <p><span>Status</span><strong>{selectedQuote.status}</strong></p>
              <button type="button" disabled={selectedQuote.status === 'Converted'} onClick={() => void convertSelectedQuote()}>
                Convert to policy
              </button>
            </div>
          )}
        </div>
      </div>

      <div className="panel">
        <h3>Quote pipeline</h3>
        {loading && <p>Loading quotes...</p>}
        {!loading && quotes?.length === 0 && <EmptyState message="No quotes have been created yet." />}
        {quotes && quotes.length > 0 && (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Quote</th>
                  <th>Farmer</th>
                  <th>Farm</th>
                  <th>Crop</th>
                  <th>Acres</th>
                  <th>Coverage</th>
                  <th>Premium</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>
                {quotes.map((quote) => (
                  <tr key={quote.id} className={selectedQuote?.id === quote.id ? 'selected-row' : ''} onClick={() => setSelectedQuote(quote)}>
                    <td>{quote.id}</td>
                    <td>{quote.farmerName || `Farmer ${quote.farmerId}`}</td>
                    <td>{quote.farmName || `Farm ${quote.farmId}`}</td>
                    <td>{quote.cropType}</td>
                    <td>{quote.acres}</td>
                    <td>{quote.coverageLevel}%</td>
                    <td>{currency(quote.estimatedPremium)}</td>
                    <td><span className="badge">{quote.status}</span></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </section>
  )
}

function currency(value: number) {
  return value.toLocaleString(undefined, { style: 'currency', currency: 'USD' })
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\pages` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client\src\types`

- Project folder.

### File: `frontend\agpolicy-client\src\types\claim.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
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
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\types` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\types\farmer.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
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
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\types` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\types\policy.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
export interface Policy {
  id: number
  farmerId: number
  farmerName: string
  farmId: number
  farmName: string
  quoteId: number
  cropType: string
  coverageLevel: number
  insuredAcres: number
  premium: number
  status: string
  effectiveDate: string
  expirationDate: string
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\types` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\src\types\quote.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
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
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client\src\types` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `frontend\agpolicy-client`

- Project folder.

### File: `frontend\agpolicy-client\tsconfig.app.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "compilerOptions": {
    "tsBuildInfoFile": "./node_modules/.tmp/tsconfig.app.tsbuildinfo",
    "target": "es2023",
    "lib": ["ES2023", "DOM"],
    "module": "esnext",
    "types": ["vite/client"],
    "skipLibCheck": true,

    /* Bundler mode */
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "verbatimModuleSyntax": true,
    "moduleDetection": "force",
    "noEmit": true,
    "jsx": "react-jsx",

    /* Linting */
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "erasableSyntaxOnly": true,
    "noFallthroughCasesInSwitch": true
  },
  "include": ["src"]
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\tsconfig.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "files": [],
  "references": [
    { "path": "./tsconfig.app.json" },
    { "path": "./tsconfig.node.json" }
  ]
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\tsconfig.node.json`

- Purpose: Project file.
- Note: JSON configuration is consumed by .NET, TypeScript, npm, or Vite tooling.
- Note: Configuration files affect build/runtime behavior but generally do not execute app logic themselves.

~~~json
{
  "compilerOptions": {
    "tsBuildInfoFile": "./node_modules/.tmp/tsconfig.node.tsbuildinfo",
    "target": "es2023",
    "lib": ["ES2023"],
    "module": "esnext",
    "types": ["node"],
    "skipLibCheck": true,

    /* Bundler mode */
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "verbatimModuleSyntax": true,
    "moduleDetection": "force",
    "noEmit": true,

    /* Linting */
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "erasableSyntaxOnly": true,
    "noFallthroughCasesInSwitch": true
  },
  "include": ["vite.config.ts"]
}
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `frontend\agpolicy-client\vite.config.ts`

- Purpose: Project file.
- Note: TypeScript adds compile-time types to JavaScript.
- Note: These files are either config, API wrappers, hooks, or shared data interfaces used by the React app.

~~~ts
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
})
~~~

- Explanation:
  - This file belongs to `frontend\agpolicy-client` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.


## Folder: `.`

- Project root. This is the full-stack workspace tying together the .NET backend, React frontend, documentation, setup scripts, and Git metadata.

### File: `README.md`

- Purpose: Main project overview and run instructions. This is the first file a new developer should read.
- Note: Markdown documentation for humans, not runtime code.

~~~markdown
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
~~~

- Explanation:
  - This file belongs to `.` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `run-dev.ps1`

- Purpose: Windows helper that starts backend and frontend dev servers.
- Note: PowerShell automation for Windows development workflows.

~~~powershell
# Runs AgPolicy backend and frontend in separate PowerShell windows.
# Usage:
#   powershell -ExecutionPolicy Bypass -File .\run-dev.ps1

$ErrorActionPreference = "Stop"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectRoot = $ScriptDir

if ((Split-Path -Leaf $ScriptDir) -eq "scripts") {
    $ProjectRoot = Split-Path -Parent $ScriptDir
}

$BackendDir = Join-Path $ProjectRoot "backend\AgPolicy.Api"
$FrontendDir = Join-Path $ProjectRoot "frontend\agpolicy-client"

if (-not (Test-Path $BackendDir)) {
    Write-Host "Backend folder not found: $BackendDir" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $FrontendDir)) {
    Write-Host "Frontend folder not found: $FrontendDir" -ForegroundColor Red
    exit 1
}

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd `"$BackendDir`"; dotnet run"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd `"$FrontendDir`"; npm run dev"

Write-Host "Started backend and frontend in separate PowerShell windows." -ForegroundColor Green
~~~

- Explanation:
  - This file belongs to `.` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `run-dev.sh`

- Purpose: Bash helper that starts backend and frontend dev servers and cleans them up together.
- Note: Bash automation for Unix-like shells and Git Bash/WSL.

~~~bash
#!/usr/bin/env bash
set -euo pipefail

# Runs AgPolicy backend and frontend in parallel on macOS/Linux/Git Bash/WSL.
# Usage:
#   chmod +x run-dev.sh
#   ./run-dev.sh

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$SCRIPT_DIR"

if [[ "$(basename "$SCRIPT_DIR")" == "scripts" ]]; then
  PROJECT_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
fi

BACKEND_DIR="$PROJECT_ROOT/backend/AgPolicy.Api"
FRONTEND_DIR="$PROJECT_ROOT/frontend/agpolicy-client"

if [[ ! -d "$BACKEND_DIR" ]]; then
  echo "Backend folder not found: $BACKEND_DIR"
  exit 1
fi

if [[ ! -d "$FRONTEND_DIR" ]]; then
  echo "Frontend folder not found: $FRONTEND_DIR"
  exit 1
fi

cleanup() {
  echo ""
  echo "Stopping dev servers..."
  jobs -p | xargs -r kill
}

trap cleanup EXIT

echo "Starting backend..."
(
  cd "$BACKEND_DIR"
  dotnet run
) &

echo "Starting frontend..."
(
  cd "$FRONTEND_DIR"
  npm run dev
) &

wait
~~~

- Explanation:
  - This file belongs to `.` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `setup.ps1`

- Purpose: Windows setup automation. Checks tools, restores NuGet packages, builds backend, applies EF migrations, and runs npm install.
- Note: PowerShell automation for Windows development workflows.

~~~powershell
# AgPolicy Manager setup script for Windows PowerShell
#
# What this script does:
# 1. Checks that .NET SDK, Node.js, and npm are installed.
# 2. Installs or updates the dotnet-ef CLI tool.
# 3. Restores backend NuGet packages.
# 4. Creates/updates the SQLite database with EF Core migrations.
# 5. Installs frontend npm packages.
#
# Usage from the project root:
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1
#
# Optional:
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1 -SkipDb
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1 -BackendOnly
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1 -FrontendOnly

param(
    [switch]$SkipDb,
    [switch]$BackendOnly,
    [switch]$FrontendOnly
)

$ErrorActionPreference = "Stop"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectRoot = $ScriptDir

# If the script lives in a scripts/ folder, use the parent as root.
if ((Split-Path -Leaf $ScriptDir) -eq "scripts") {
    $ProjectRoot = Split-Path -Parent $ScriptDir
}

$BackendDir = Join-Path $ProjectRoot "backend\AgPolicy.Api"
$FrontendDir = Join-Path $ProjectRoot "frontend\agpolicy-client"

function Write-Step {
    param([string]$Message)
    Write-Host ""
    Write-Host "==> $Message" -ForegroundColor Cyan
}

function Fail {
    param([string]$Message)
    Write-Host ""
    Write-Host "ERROR: $Message" -ForegroundColor Red
    exit 1
}

function Test-CommandExists {
    param([string]$Command)
    $null -ne (Get-Command $Command -ErrorAction SilentlyContinue)
}

function Check-RequiredTool {
    param(
        [string]$Command,
        [string]$InstallHint
    )

    if (-not (Test-CommandExists $Command)) {
        Fail "$Command is not installed or not on PATH.

Install it, then rerun this script.

$InstallHint"
    }
}

Write-Step "AgPolicy Manager setup starting"
Write-Host "Project root: $ProjectRoot"

if (-not $FrontendOnly) {
    Check-RequiredTool "dotnet" "Download the .NET SDK from: https://dotnet.microsoft.com/download"
}

if (-not $BackendOnly) {
    Check-RequiredTool "node" "Download Node.js LTS from: https://nodejs.org/"
    Check-RequiredTool "npm" "npm is included with Node.js: https://nodejs.org/"
}

if (-not $FrontendOnly) {
    if (-not (Test-Path $BackendDir)) {
        Fail "Backend folder not found: $BackendDir"
    }

    Write-Step "Checking .NET SDK"
    dotnet --version

    Write-Step "Installing/updating dotnet-ef CLI tool"
    $toolList = dotnet tool list --global
    if ($toolList -match "^dotnet-ef\s") {
        dotnet tool update --global dotnet-ef
    }
    else {
        dotnet tool install --global dotnet-ef
    }

    # Ensure global dotnet tools path is available for the current PowerShell session.
    $DotnetTools = Join-Path $env:USERPROFILE ".dotnet\tools"
    if ($env:Path -notlike "*$DotnetTools*") {
        $env:Path = "$env:Path;$DotnetTools"
    }

    Write-Step "Restoring backend NuGet packages"
    dotnet restore (Join-Path $BackendDir "AgPolicy.Api.csproj")

    Write-Step "Building backend"
    dotnet build (Join-Path $BackendDir "AgPolicy.Api.csproj") --no-restore

    if (-not $SkipDb) {
        Write-Step "Applying EF Core migrations / creating SQLite database"
        Push-Location $BackendDir
        dotnet ef database update
        Pop-Location
    }
    else {
        Write-Step "Skipping database migration because -SkipDb was provided"
    }
}

if (-not $BackendOnly) {
    if (-not (Test-Path $FrontendDir)) {
        Fail "Frontend folder not found: $FrontendDir"
    }

    Write-Step "Checking Node and npm"
    node --version
    npm --version

    Write-Step "Installing frontend npm packages"
    Push-Location $FrontendDir
    npm install
    Pop-Location
}

Write-Step "Setup complete"
Write-Host ""
Write-Host "To run the backend:"
Write-Host "  cd backend\AgPolicy.Api"
Write-Host "  dotnet run"
Write-Host ""
Write-Host "To run the frontend in another terminal:"
Write-Host "  cd frontend\agpolicy-client"
Write-Host "  npm run dev"
Write-Host ""
Write-Host "If the frontend cannot reach the backend, confirm the API base URL in src/api/*.ts matches the backend URL printed by dotnet run."
~~~

- Explanation:
  - This file belongs to `.` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

### File: `setup.sh`

- Purpose: Bash version of the setup automation for macOS/Linux/Git Bash/WSL.
- Note: Bash automation for Unix-like shells and Git Bash/WSL.

~~~bash
#!/usr/bin/env bash
set -euo pipefail

# AgPolicy Manager setup script for macOS/Linux/Git Bash/WSL
#
# What this script does:
# 1. Checks that .NET SDK, Node.js, and npm are installed.
# 2. Installs or updates the dotnet-ef CLI tool.
# 3. Restores backend NuGet packages.
# 4. Creates/updates the SQLite database with EF Core migrations.
# 5. Installs frontend npm packages.
#
# Usage:
#   chmod +x setup.sh
#   ./setup.sh
#
# Optional:
#   ./setup.sh --skip-db
#   ./setup.sh --backend-only
#   ./setup.sh --frontend-only

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$SCRIPT_DIR"

# If the script lives in a scripts/ folder, use the parent as root.
if [[ "$(basename "$SCRIPT_DIR")" == "scripts" ]]; then
  PROJECT_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
fi

BACKEND_DIR="$PROJECT_ROOT/backend/AgPolicy.Api"
FRONTEND_DIR="$PROJECT_ROOT/frontend/agpolicy-client"

SKIP_DB=false
BACKEND_ONLY=false
FRONTEND_ONLY=false

for arg in "$@"; do
  case "$arg" in
    --skip-db)
      SKIP_DB=true
      ;;
    --backend-only)
      BACKEND_ONLY=true
      ;;
    --frontend-only)
      FRONTEND_ONLY=true
      ;;
    *)
      echo "Unknown option: $arg"
      echo "Valid options: --skip-db --backend-only --frontend-only"
      exit 1
      ;;
  esac
done

info() {
  echo ""
  echo "==> $1"
}

fail() {
  echo ""
  echo "ERROR: $1"
  exit 1
}

command_exists() {
  command -v "$1" >/dev/null 2>&1
}

check_required_tool() {
  local cmd="$1"
  local install_hint="$2"

  if ! command_exists "$cmd"; then
    fail "$cmd is not installed or not on PATH.

Install it, then rerun this script.

$install_hint"
  fi
}

info "AgPolicy Manager setup starting"
echo "Project root: $PROJECT_ROOT"

if [[ "$FRONTEND_ONLY" == false ]]; then
  check_required_tool "dotnet" "Download the .NET SDK from:
https://dotnet.microsoft.com/download"
fi

if [[ "$BACKEND_ONLY" == false ]]; then
  check_required_tool "node" "Download Node.js LTS from:
https://nodejs.org/"
  check_required_tool "npm" "npm is included with Node.js:
https://nodejs.org/"
fi

if [[ "$FRONTEND_ONLY" == false ]]; then
  [[ -d "$BACKEND_DIR" ]] || fail "Backend folder not found: $BACKEND_DIR"

  info "Checking .NET SDK"
  dotnet --version

  info "Installing/updating dotnet-ef CLI tool"
  if dotnet tool list --global | grep -q "^dotnet-ef"; then
    dotnet tool update --global dotnet-ef
  else
    dotnet tool install --global dotnet-ef
  fi

  # Ensure global dotnet tools path is available for the current shell.
  export PATH="$PATH:$HOME/.dotnet/tools"

  info "Restoring backend NuGet packages"
  dotnet restore "$BACKEND_DIR/AgPolicy.Api.csproj"

  info "Building backend"
  dotnet build "$BACKEND_DIR/AgPolicy.Api.csproj" --no-restore

  if [[ "$SKIP_DB" == false ]]; then
    info "Applying EF Core migrations / creating SQLite database"
    (
      cd "$BACKEND_DIR"
      dotnet ef database update
    )
  else
    info "Skipping database migration because --skip-db was provided"
  fi
fi

if [[ "$BACKEND_ONLY" == false ]]; then
  [[ -d "$FRONTEND_DIR" ]] || fail "Frontend folder not found: $FRONTEND_DIR"

  info "Checking Node and npm"
  node --version
  npm --version

  info "Installing frontend npm packages"
  (
    cd "$FRONTEND_DIR"
    npm install
  )
fi

info "Setup complete"
echo ""
echo "To run the backend:"
echo "  cd backend/AgPolicy.Api"
echo "  dotnet run"
echo ""
echo "To run the frontend in another terminal:"
echo "  cd frontend/agpolicy-client"
echo "  npm run dev"
echo ""
echo "If the frontend cannot reach the backend, confirm the API base URL in src/api/*.ts matches the backend URL printed by dotnet run."
~~~

- Explanation:
  - This file belongs to `.` and supports the responsibility described above.
  - Review the code block above together with the purpose notes to understand how this file contributes to the app.

