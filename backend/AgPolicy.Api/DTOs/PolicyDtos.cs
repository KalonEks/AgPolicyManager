namespace AgPolicy.Api.DTOs;

public class PolicyResponse
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public string FarmerName { get; set; } = string.Empty;
    public int FarmId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public int QuoteId { get; set; }
    public string CropType { get; set; } = string.Empty;
    public int CoverageLevel { get; set; }
    public decimal InsuredAcres { get; set; }
    public decimal Premium { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}
