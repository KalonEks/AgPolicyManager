using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[Route("api/quotes")]
public class QuotesController(QuoteService quoteService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<QuoteResponse>>> GetQuotes()
    {
        return Ok(await quoteService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuoteResponse>> GetQuote(int id)
    {
        return ToActionResult(await quoteService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<QuoteResponse>> CreateQuote(CreateQuoteRequest request)
    {
        var result = await quoteService.CreateAsync(request);
        if (result.Status != ServiceResultStatus.Success || result.Value is null)
        {
            return ToActionResult(result);
        }

        return CreatedAtAction(nameof(GetQuote), new { id = result.Value.Id }, result.Value);
    }

    [HttpPost("{id:int}/convert-to-policy")]
    public async Task<ActionResult<ConvertQuoteResponse>> ConvertToPolicy(int id)
    {
        return ToActionResult(await quoteService.ConvertToPolicyAsync(id));
    }
}
