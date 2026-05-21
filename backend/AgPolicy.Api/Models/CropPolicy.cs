namespace AgPolicy.Api.Models;

public class CropPolicy
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }
    public int FarmId { get; set; }
    public Farm? Farm { get; set; }
    public int QuoteId { get; set; }
    public Quote? Quote { get; set; }
    public string CropType { get; set; } = string.Empty;
    public int CoverageLevel { get; set; }
    public decimal InsuredAcres { get; set; }
    public decimal Premium { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Claim> Claims { get; set; } = [];
}
