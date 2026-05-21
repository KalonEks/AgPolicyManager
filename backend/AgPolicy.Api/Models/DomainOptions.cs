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
