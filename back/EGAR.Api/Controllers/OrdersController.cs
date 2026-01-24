using EGAR.Api.Controllers.Base;
using EGAR.Api.Extensions;
using EGAR.Application.Features.Orders.Commands;
using EGAR.Application.Features.Orders.Queries;
using EGAR.Domain.Enums;
using EGAR.Domain.Models;
using EGAR.SharedKernel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EGAR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(){}

        [HttpGet]
        public async Task<ActionResult<PageResult<Order>>> GetAll([FromQuery] OrdersListQuery query, CancellationToken ct)
            => (await Mediator.Send(query, ct)).ToActionResult();

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] CreateOrderCommand command, CancellationToken ct)
            => (await Mediator.Send(command, ct)).ToActionResult();        

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
            => (await Mediator.Send(new OrderQuery { Id = id })).ToActionResult();

        [HttpPost("{id}/status")]
        public async Task<ActionResult<Order>> ChangeStatus([FromBody] UpdateOrderStatusCommand command, CancellationToken ct)
        {
            var result = await Mediator.Send(command, ct);
            //await _stream.PushAsync(order);
            return result.ToActionResult();
        }

        //[HttpPost]
        //public async Task<ActionResult<Order>> Create(CreateOrderDto dto, CancellationToken ct)
        //{
        //    var order = await _service.CreateAsync(dto.OrderNumber, dto.Description, ct);
        //    await _stream.PushAsync(order); // локальна€ трансл€ци€ в SSE
        //    return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        //}




        //[HttpGet("stream")]
        //public async Task Stream(CancellationToken ct)
        //{
        //    Response.Headers.Add("Content-Type", "text/event-stream");
        //    await _stream.SubscribeAsync(Response.Body, ct);
        //}

    }
}
