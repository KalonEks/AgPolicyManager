using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/farmers")]
public class FarmersController(FarmerService farmerService, FarmService farmService, PolicyService policyService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FarmerResponse>>> GetFarmers()
    {
        return Ok(await farmerService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FarmerResponse>> GetFarmer(int id)
    {
        return ToActionResult(await farmerService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<FarmerResponse>> CreateFarmer(CreateFarmerRequest request)
    {
        var result = await farmerService.CreateAsync(request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(GetFarmer), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<FarmerResponse>> UpdateFarmer(int id, UpdateFarmerRequest request)
    {
        return ToActionResult(await farmerService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFarmer(int id)
    {
        return ToNoContentResult(await farmerService.DeleteAsync(id));
    }

    [HttpGet("{farmerId:int}/farms")]
    public async Task<ActionResult<IReadOnlyList<FarmResponse>>> GetFarms(int farmerId)
    {
        return ToActionResult(await farmService.GetByFarmerAsync(farmerId));
    }

    [HttpPost("{farmerId:int}/farms")]
    public async Task<ActionResult<FarmResponse>> CreateFarm(int farmerId, CreateFarmRequest request)
    {
        var result = await farmService.CreateAsync(farmerId, request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(FarmsController.GetFarm), "Farms", new { id = result.Value.Id }, result.Value);
    }

    [HttpGet("{farmerId:int}/policies")]
    public async Task<ActionResult<IReadOnlyList<PolicyResponse>>> GetPolicies(int farmerId)
    {
        return ToActionResult(await policyService.GetByFarmerAsync(farmerId));
    }
}
