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
