using EGAR.Api.Controllers.Base;
using EGAR.Api.Extensions;
using EGAR.Application.Features.Orders.Commands;
using EGAR.Application.Features.Orders.Queries;
using EGAR.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EGAR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(){}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll([FromQuery] OrdersListQuery query)
        {
            var result = await Mediator.Send(query);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ToActionResult();
        }

        //[HttpPost]
        //public async Task<ActionResult<Order>> Create(CreateOrderDto dto, CancellationToken ct)
        //{
        //    var order = await _service.CreateAsync(dto.OrderNumber, dto.Description, ct);
        //    await _stream.PushAsync(order); // локальна€ трансл€ци€ в SSE
        //    return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Order>> GetById(Guid id, CancellationToken ct)
        //{
        //    var order = await _service.GetByIdAsync(id, ct);
        //    return order is null ? NotFound() : Ok(order);
        //}

        //[HttpPost("{id}/status")]
        //public async Task<ActionResult<Order>> ChangeStatus(Guid id, ChangeStatusDto dto, CancellationToken ct)
        //{
        //    var order = await _service.ChangeStatusAsync(id, dto.Status, ct);
        //    await _stream.PushAsync(order);
        //    return Ok(order);
        //}

        //[HttpGet("stream")]
        //public async Task Stream(CancellationToken ct)
        //{
        //    Response.Headers.Add("Content-Type", "text/event-stream");
        //    await _stream.SubscribeAsync(Response.Body, ct);
        //}

    }
}
