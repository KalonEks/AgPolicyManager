using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/claims")]
public class ClaimsController(ClaimService claimService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClaimResponse>>> GetClaims()
    {
        return Ok(await claimService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClaimResponse>> GetClaim(int id)
    {
        return ToActionResult(await claimService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<ClaimResponse>> CreateClaim(CreateClaimRequest request)
    {
        var result = await claimService.CreateAsync(request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(GetClaim), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<ClaimResponse>> UpdateStatus(int id, UpdateClaimStatusRequest request)
    {
        return ToActionResult(await claimService.UpdateStatusAsync(id, request));
    }
}
