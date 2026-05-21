namespace AgPolicy.Api.DTOs;

public class CreateFarmRequest
{
    public string FarmName { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class FarmResponse
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}
