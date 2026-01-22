using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EGAR.Api.Controllers.Base;

public abstract class ApiControllerBase : Controller
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
