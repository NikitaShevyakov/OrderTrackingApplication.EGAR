using EGAR.SharedKernel.Enums;
using EGAR.SharedKernel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EGAR.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult(this Result result)
        => result.IsSuccess
            ? new OkObjectResult(result)
            : CreateErrorResult(result.ErrorType, result.ErrorMessage);

    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
        => result.IsSuccess
            ? new OkObjectResult(result.Value)
            : CreateErrorResult(result.ErrorType, result.ErrorMessage);

    private static ActionResult CreateErrorResult(ErrorType type, string message)
        => type switch
        {
            ErrorType.Validation => new BadRequestObjectResult(new { error = message }),
            ErrorType.NotFound => new NotFoundObjectResult(new { error = message }),
            ErrorType.Conflict => new ConflictObjectResult(new { error = message }),
            ErrorType.Unauthorized => new UnauthorizedObjectResult(new { error = "Unauthorized" }),
            ErrorType.Forbidden => new ForbidResult(),
            _ => new ObjectResult(new { error = message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
        };
}
