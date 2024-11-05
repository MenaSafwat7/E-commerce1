using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Specifications
{
    internal class OrderWithIncludeSpecification : Specification<Order>
    {
        public OrderWithIncludeSpecification(Guid id) : base(order => order.Id == id)
        {
            AddInclude(order => order.deliveryMethod);
            AddInclude(order => order.orderItems);
        }

        public OrderWithIncludeSpecification(string email) : base(order => order.UserEmail == email)
        {
            AddInclude(order => order.deliveryMethod);
            AddInclude(order => order.orderItems);

            SetOrderBy(o => o.OrderDate);
        }
    }
}
