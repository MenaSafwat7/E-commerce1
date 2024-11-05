global using OrderEntity = Domain.Entities.OrderEntities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntities;

namespace Presistence.Data.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, address => address.WithOwner());

            builder.HasMany(order => order.orderItems).WithOne();

            builder.Property(order => order.paymentStatus)
                .HasConversion(
                    s => s.ToString(),
                    s => Enum.Parse<OrderPaymentStatus>(s));

            builder.HasOne(order => order.deliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(order => order.Subtotal)
                .HasColumnType("decimal(18,3)");
        }
    }
}
