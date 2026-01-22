using EGAR.SharedKernel;
using EGAR.SharedKernel.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EGAR.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
        => result.IsSuccess 
            ? new OkObjectResult(result.Value)
            : result.ErrorType switch
                {
                    ErrorType.Validation => new BadRequestObjectResult(new { error = result.ErrorMessage }),
                    ErrorType.NotFound => new NotFoundObjectResult(new { error = result.ErrorMessage }),
                    ErrorType.Conflict => new ConflictObjectResult(new { error = result.ErrorMessage }),
                    ErrorType.Unauthorized => new UnauthorizedObjectResult(new { error = "Unauthorized" }),
                    ErrorType.Forbidden => new ForbidResult(),
                    _ => new ObjectResult(new { error = result.ErrorMessage }) { StatusCode = StatusCodes.Status500InternalServerError }
                };
}

