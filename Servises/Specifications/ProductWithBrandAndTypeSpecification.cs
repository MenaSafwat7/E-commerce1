using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Specifications
{
    public class ProductWithBrandAndTypeSpecification : Specification<Product>
    {
        public ProductWithBrandAndTypeSpecification(int id)
            : base(Product=> Product.Id == id)
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);
        }
        public ProductWithBrandAndTypeSpecification(ProductSpecificationsParameter parameters)
            :base(Product => 
            (!parameters.BrandId.HasValue || Product.BrandID == parameters.BrandId.Value) &&
            (!parameters.typeId.HasValue  || Product.TypeID  == parameters.typeId.Value) && 
            (string.IsNullOrWhiteSpace(parameters.Search) || Product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);


            ApplyPagination(parameters.PageIndex, parameters.PageSize);

            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortingOptions.PriceDesc:
                        SetOrderByDescending(Product => Product.Price);
                        break;
                    case ProductSortingOptions.PriceAsc:
                        SetOrderBy(Product => Product.Price);
                        break;
                    case ProductSortingOptions.NameDesc:
                        SetOrderByDescending(Product => Product.Name);
                        break;
                    case ProductSortingOptions.NameAsc:
                        SetOrderBy(Product => Product.Name);
                        break;
                    default:
                        break;
                }
            }
            

        }
    }
}
