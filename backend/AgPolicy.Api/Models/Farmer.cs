namespace AgPolicy.Api.Models;

public class Farmer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public List<Farm> Farms { get; set; } = [];
    public List<Quote> Quotes { get; set; } = [];
    public List<CropPolicy> Policies { get; set; } = [];
}
