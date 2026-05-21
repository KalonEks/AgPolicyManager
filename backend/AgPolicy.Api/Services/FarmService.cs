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
