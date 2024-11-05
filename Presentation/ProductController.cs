global using Microsoft.AspNetCore.Mvc;
global using Servises.Abstractions;
global using Shared;
global using System.Net;
using Microsoft.AspNetCore.Authorization;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize()]
    public class ProductController(IServiceManager serviceManager) : ApiController
    {
        [RedisCache]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDTO>>> GetAllProducts([FromQuery] ProductSpecificationsParameter parameters)
        {
            var products = await serviceManager.productService.GetAllProductsAsync(parameters);
            return Ok(products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetAllBrands()
        {
            var Brands = await serviceManager.productService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetAllTypes()
        {
            var types = await serviceManager.productService.GetAllTypesAsync();
            return Ok(types);
        }
        
        [ProducesResponseType(typeof(ProductResultDTO),(int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDTO>> GetProduct(int id)
        {
            var product = await serviceManager.productService.GetProductByIdAsync(id);
            return Ok(product); 
        }
    }
}
