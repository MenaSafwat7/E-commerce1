using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Specifications
{
    internal class ProductCountSpecification : Specification<Product>
    {
        public ProductCountSpecification(ProductSpecificationsParameter parameters)
            : base(Product =>
            (!parameters.BrandId.HasValue || Product.BrandID == parameters.BrandId.Value) &&
            (!parameters.typeId.HasValue || Product.TypeID == parameters.typeId.Value) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || Product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
        }
    }
}
