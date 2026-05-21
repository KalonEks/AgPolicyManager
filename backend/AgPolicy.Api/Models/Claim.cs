namespace AgPolicy.Api.Models;

public class Claim
{
    public int Id { get; set; }
    public int PolicyId { get; set; }
    public CropPolicy? Policy { get; set; }
    public DateTime LossDate { get; set; }
    public string LossReason { get; set; } = string.Empty;
    public decimal EstimatedLossAmount { get; set; }
    public string Status { get; set; } = "Open";
    public string? Notes { get; set; }
}
