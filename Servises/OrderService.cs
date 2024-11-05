using Domain.Entities.Identity;
using Domain.Entities.OrderEntities;
using Domain.Exeptions;
using Servises.Specifications;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises
{
    internal class OrderService (IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository)
        : IOrderService
    {
        public async Task<OrderRequest> CreateOrderAsync(OrderRequest request, string UserEmail)
        {
            var address = mapper.Map<ShippingAddress>(request.ShippingAddress);
            var basket = await basketRepository.GetBasketAsync(request.BasketId) 
                ?? throw new BasketNotFoundExeption(request.BasketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id) 
                    ?? throw new ProductNotFoundExeption(item.Id);

                orderItems.Add(CreateOrderItem(item, product));
            }

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(request.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundExeption(request.DeliveryMethodId);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);


            var order = new Order(UserEmail, address, orderItems, deliveryMethod, subtotal);
            await unitOfWork.GetRepository<Order, Guid>()
                .AddAsync(order);
            await unitOfWork.SaveChangesAsync();


            return mapper.Map<OrderRequest>(order);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
            => new OrderItem(new ProductInOrderItem(product.Id, product.Name , product.PictureURL), item.Quantity, product.Price);

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var methods = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(methods); 
        }

        public async Task<OrderResult> GetOrderByIDAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithIncludeSpecification(id))
                ?? throw new OrderNotFoundException(id);
            return mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(new OrderWithIncludeSpecification(email));

            return mapper.Map<IEnumerable<OrderResult>>(order);
        }
    }
}
