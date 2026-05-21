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
