global using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Abstractions
{
    public interface IProductService
    {
        public Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationsParameter parameters);
        public Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();
        public Task<ProductResultDTO?> GetProductByIdAsync(int id);
    }
}
