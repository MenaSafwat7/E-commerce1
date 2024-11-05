using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servises.Abstractions;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.orderService.CreateOrderAsync(request, email);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManager.orderService.GetOrdersByEmailAsync(email);
            return Ok(orders);
        }

        [HttpGet("id")]
        public async Task<ActionResult<OrderResult>> GetOrder(Guid id)
        {
            var orders = await serviceManager.orderService.GetOrderByIDAsync(id);
            return Ok(orders);
        }

        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<DeliveryMethodResult>> GetDeliveryMethods()
        {
            var methods = await serviceManager.orderService.GetDeliveryMethodsAsync();
            return Ok(methods);

        }
    }
}
