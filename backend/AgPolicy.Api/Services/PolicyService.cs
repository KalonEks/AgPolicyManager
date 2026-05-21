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
