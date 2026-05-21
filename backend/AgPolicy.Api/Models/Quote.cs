namespace AgPolicy.Api.Models;

public class Quote
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }
    public int FarmId { get; set; }
    public Farm? Farm { get; set; }
    public string CropType { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public int CoverageLevel { get; set; }
    public decimal EstimatedPremium { get; set; }
    public string Status { get; set; } = "Draft";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? ConvertedPolicyId { get; set; }
}
