using Domain.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises
{
    class BasketServise(IBasketRepository basketRepository, IMapper mapper) : IBasketServise
    {
        public async Task<bool> DeleteBasketAsync(string id) => await basketRepository.DeleteBasketAsync(id); 

        public async Task<BasketDTO?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundExeption(id) : mapper.Map<BasketDTO?>(basket);
        }

        public async Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket)
        {
            var CustomerBasket = mapper.Map<CustomerBasket>(basket);
            var UpdatedBasket = await basketRepository.UpdateBasketAsync(CustomerBasket);
            return UpdatedBasket is null ? throw new Exception("Can't Update Basket Now") 
                : mapper.Map<BasketDTO>(UpdatedBasket); 
        }
    }
}
