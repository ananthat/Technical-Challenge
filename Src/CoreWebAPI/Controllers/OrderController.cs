using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Orders.Commands.DeleteOrder;
using Northwind.Application.Orders.Commands.CreateOrder;
using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Application.Orders.Queries.GetOrderList;
using Northwind.Application.Orders.Queries.GetOrder;
using Northwind.Application.Orders.Commands.UpdateOrder;

namespace Northwind.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ananth_OrderController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<OrderLookupDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetOrderListQuery()));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderVm>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetOrderQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddProductsToOrder([FromBody] AddProductsToOrderCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteOrderCommand { Id = id });

            return NoContent();
        }
    }
}
