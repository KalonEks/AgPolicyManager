using AgPolicy.Api.DTOs;
using AgPolicy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgPolicy.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult<T> ToActionResult<T>(ServiceResult<T> result)
    {
        return result.Status switch
        {
            ServiceResultStatus.Success => Ok(result.Value),
            ServiceResultStatus.Invalid => BadRequest(new ApiErrorResponse { Message = result.Error ?? "Invalid request." }),
            ServiceResultStatus.NotFound => NotFound(new ApiErrorResponse { Message = result.Error ?? "Resource not found." }),
            ServiceResultStatus.Conflict => Conflict(new ApiErrorResponse { Message = result.Error ?? "Request conflicts with current state." }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResponse { Message = "Unexpected server error." })
        };
    }

    protected IActionResult ToNoContentResult(ServiceResult result)
    {
        return result.Status switch
        {
            ServiceResultStatus.Success => NoContent(),
            ServiceResultStatus.Invalid => BadRequest(new ApiErrorResponse { Message = result.Error ?? "Invalid request." }),
            ServiceResultStatus.NotFound => NotFound(new ApiErrorResponse { Message = result.Error ?? "Resource not found." }),
            ServiceResultStatus.Conflict => Conflict(new ApiErrorResponse { Message = result.Error ?? "Request conflicts with current state." }),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new ApiErrorResponse { Message = "Unexpected server error." })
        };
    }
}
