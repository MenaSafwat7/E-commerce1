using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises
{
    public class CahceService(ICacheRepository cacheRepository) : ICahceService
    {
        public async Task<string?> GetCachedValueAsync(string Cachekey) => await cacheRepository.GetAsync(Cachekey);

        public Task SetValueCaheAsync(string Cachekey, object value, TimeSpan duration) => cacheRepository.SetAsync(Cachekey, value, duration);
    }
}
