namespace AgPolicy.Api.DTOs;

public class CreateFarmerRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class UpdateFarmerRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}

public class FarmerResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string County { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}
