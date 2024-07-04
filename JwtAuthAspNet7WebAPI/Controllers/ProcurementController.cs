using JwtAuthAspNet7WebAPI.Core.Entities;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using JwtAuthAspNet7WebAPI.Core.OtherObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwtAuthAspNet7WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcurementController : ControllerBase
    {
        private readonly IProcurementService _procurementService;

        public ProcurementController(IProcurementService procurementService)
        {
            _procurementService = procurementService;
        }

        [HttpGet("orders")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PROCUREMENT + "," + StaticUserRoles.PRODUCTION_WORKER)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _procurementService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("orders/{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PROCUREMENT)]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _procurementService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("orders")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PROCUREMENT)]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var createdOrder = await _procurementService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("orders/{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PROCUREMENT)]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var updatedOrder = await _procurementService.UpdateOrderAsync(order);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return Ok(updatedOrder);
        }

        [HttpDelete("orders/{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PROCUREMENT)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _procurementService.DeleteOrderAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("orders/total")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN + "," + StaticUserRoles.PROCUREMENT + "," + StaticUserRoles.PRODUCTION_WORKER)]
        public async Task<ActionResult<decimal>> GetTotalOrderValue([FromQuery] string productName)
        {
            var totalValue = await _procurementService.GetTotalOrderValueByProductAsync(productName);
            return Ok(totalValue);
        }

    }
}
