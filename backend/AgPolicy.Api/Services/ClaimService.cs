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
