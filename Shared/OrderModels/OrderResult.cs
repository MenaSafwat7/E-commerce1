using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderResult
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public ICollection<OrderItemDTO> orderItems { get; set; } = new List<OrderItemDTO>();
        public string paymentStatus { get; set; }
        public string deliveryMethod { get; set; }
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal Total { get; set; }
    }
}
