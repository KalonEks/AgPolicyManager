using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/policies")]
public class PoliciesController(PolicyService policyService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PolicyResponse>>> GetPolicies()
    {
        return Ok(await policyService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PolicyResponse>> GetPolicy(int id)
    {
        return ToActionResult(await policyService.GetByIdAsync(id));
    }
}
