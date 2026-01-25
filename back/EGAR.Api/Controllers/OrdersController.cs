using EGAR.Api.Controllers.Base;
using EGAR.Api.Extensions;
using EGAR.Application.Events;
using EGAR.Application.Features.Orders.Commands;
using EGAR.Application.Features.Orders.Queries;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EGAR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ApiControllerBase
    {
        private readonly IOrderStatusEventStream _orderStatusEventStream;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderStatusEventStream orderStatusEventStream,
            ILogger<OrdersController> logger)
        {
            _orderStatusEventStream = orderStatusEventStream;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<Order>>> GetAll(
            [FromQuery] OrdersListQuery query,
            CancellationToken ct)
            => (await Mediator.Send(query, ct)).ToActionResult();

        [HttpPost]
        public async Task<ActionResult<Order>> Create(
            [FromBody] CreateOrderCommand command,
            CancellationToken ct)
            => (await Mediator.Send(command, ct)).ToActionResult();        

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
            => (await Mediator.Send(new OrderQuery(id))).ToActionResult();

        [HttpPost("{id}/status")]
        public async Task<ActionResult<Order>> ChangeStatus(
            [FromBody] UpdateOrderStatusCommand command,
            CancellationToken ct)
            => (await Mediator.Send(command, ct)).ToActionResult();        

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
            => (await Mediator.Send(new DeleteOrderCommand(id), ct)).ToActionResult();        

        [HttpGet("{orderId}/stream")]
        public async Task Stream(int orderId, CancellationToken ct)
        {
            Response.Headers.ContentType = "text/event-stream";
            Response.Headers.CacheControl = "no-cache";
            Response.Headers.Connection = "keep-alive";
            HttpContext.Response.Headers.Add("X-Accel-Buffering", "no");

            await foreach (var evt in _orderStatusEventStream.Subscribe(ct))
            {
                if (evt.OrderId != orderId) continue;

                var json = JsonSerializer.Serialize(evt);
                await Response.WriteAsync($"data: {json}\n\n", ct);
                await Response.Body.FlushAsync(ct);
            }            
        }
    }
}
