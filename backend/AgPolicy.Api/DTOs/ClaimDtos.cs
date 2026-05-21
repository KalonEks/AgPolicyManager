namespace AgPolicy.Api.DTOs;

public class CreateClaimRequest
{
    public int PolicyId { get; set; }
    public DateTime LossDate { get; set; }
    public string LossReason { get; set; } = string.Empty;
    public decimal EstimatedLossAmount { get; set; }
    public string? Notes { get; set; }
}

public class UpdateClaimStatusRequest
{
    public string Status { get; set; } = string.Empty;
}

public class ClaimResponse
{
    public int Id { get; set; }
    public int PolicyId { get; set; }
    public DateTime LossDate { get; set; }
    public string LossReason { get; set; } = string.Empty;
    public decimal EstimatedLossAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
