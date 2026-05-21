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
