using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Abstractions
{
    public interface IBasketServise
    {
        public Task<BasketDTO?> GetBasketAsync(string id);
        public Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket);
        public Task<bool> DeleteBasketAsync(string id);
    }
}
