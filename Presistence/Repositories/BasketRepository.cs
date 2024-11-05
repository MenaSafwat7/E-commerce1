using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string Id) => await _database.KeyDeleteAsync(Id);

        public async Task<CustomerBasket?> GetBasketAsync(string Id) 
        {
            var value = await _database.StringGetAsync(Id);
            if(value.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket?>(value);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, TimeToLive?? TimeSpan.FromDays(30));
            return isCreatedOrUpdated? await GetBasketAsync(basket.Id) : null;
        }
    }
}
