using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/farms")]
public class FarmsController(FarmService farmService) : ApiControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FarmResponse>> GetFarm(int id)
    {
        return ToActionResult(await farmService.GetByIdAsync(id));
    }
}
