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
