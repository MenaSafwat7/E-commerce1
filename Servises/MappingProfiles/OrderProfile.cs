using Domain.Entities.Identity;
using Domain.Entities.OrderEntities;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.MappingProfiles
{
    class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.product.ProductId))
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.product.ProductName))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => s.product.PictureUrl));


            CreateMap<Order, OrderResult>()
                .ForMember(d => d.paymentStatus, options => options.MapFrom(s => s.ToString()))
                .ForMember(d => d.deliveryMethod, options => options.MapFrom(s => s.deliveryMethod.ShortName))
                .ForMember(d => d.Total, options => options.MapFrom(s => s.Subtotal + s.deliveryMethod.Price));

            CreateMap<DeliveryMethod, DeliveryMethodResult>();
        }
    }
}
