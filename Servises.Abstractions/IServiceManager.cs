using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Abstractions
{
    public interface IServiceManager
    {
        public IProductService productService { get; }
        public IBasketServise basketServise { get; }
        public IAutionticationService autionticationService { get; }
        public IOrderService orderService { get; }
        public ICahceService cahceService { get; }
    }
}
