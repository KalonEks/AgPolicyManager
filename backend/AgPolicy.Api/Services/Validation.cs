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
