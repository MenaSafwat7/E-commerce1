using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class OrderItem :BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem product, int quantity, decimal price)
        {
            this.product = product;
            Quantity = quantity;
            Price = price;
        }

        public ProductInOrderItem product {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
