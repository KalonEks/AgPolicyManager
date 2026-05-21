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
