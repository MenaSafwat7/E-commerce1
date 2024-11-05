global using AutoMapper;
global using Domain.Contracts;
global using Servises.Abstractions;
global using Shared;
using Domain.Entities;
using Domain.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Servises.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises
{
    class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsResult = mapper.Map<IEnumerable<BrandResultDTO>>(brands); 
            return brandsResult;
        }

        public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationsParameter parameters)
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecification(parameters));
            var productsResult = mapper.Map<IEnumerable<ProductResultDTO>>(products);
            var count = productsResult.Count();
            var Totalcount = await unitOfWork.GetRepository<Product, int>().CoungAsync(new ProductCountSpecification(parameters));

            var result = new PaginatedResult<ProductResultDTO>
                (parameters.PageIndex,
                count,
               Totalcount
               , productsResult
                );
            return result;
        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesResult = mapper.Map<IEnumerable<TypeResultDTO>>(types);
            return typesResult;
        }

        public async Task<ProductResultDTO?> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(new ProductWithBrandAndTypeSpecification(id));
            return product is null ? throw new ProductNotFoundExeption(id) : mapper.Map<ProductResultDTO?>(product);
        }
    }
}
