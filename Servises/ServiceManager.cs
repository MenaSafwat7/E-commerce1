using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketServise> _basketServise;
        private readonly Lazy<IAutionticationService> _Lazyautiontication;
        private readonly Lazy<IOrderService> _LazyOrderService;
        private readonly Lazy<ICahceService> _LazyCacheService;
        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<User> userManager, IOptions<JWTOptions> options, ICacheRepository cacheRepository)
        {
            _productService = new Lazy<IProductService>(()=> new ProductService(unitOfWork, mapper));
            _basketServise = new Lazy<IBasketServise>(() => new BasketServise(basketRepository, mapper));
            _Lazyautiontication = new Lazy<IAutionticationService>(() => new AuthunticationService(userManager, options, mapper));

            _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork, mapper, basketRepository));
        }
        public IProductService productService => _productService.Value;

        public IBasketServise basketServise => _basketServise.Value;

        public IAutionticationService autionticationService => _Lazyautiontication.Value;

        public IOrderService orderService => _LazyOrderService.Value;

        public ICahceService cahceService => _LazyCacheService.Value;
    }
}
