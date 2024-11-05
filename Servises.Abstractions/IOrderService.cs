using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Abstractions
{
    public interface IOrderService
    {
        public Task<OrderResult> GetOrderByIDAsync(Guid id);
        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email);
        public Task<OrderRequest> CreateOrderAsync(OrderRequest request, string UserEmail);
        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
