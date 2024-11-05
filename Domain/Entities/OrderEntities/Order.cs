using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, ShippingAddress shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            this.orderItems = orderItems;
            this.deliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus paymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod deliveryMethod { get; set; }
        public int? deliveryMethodId { get; set; }
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    }
}
