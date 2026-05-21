namespace AgPolicy.Api.Models;

public class Farm
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public Farmer? Farmer { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public decimal Acres { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public List<Quote> Quotes { get; set; } = [];
    public List<CropPolicy> Policies { get; set; } = [];
}
