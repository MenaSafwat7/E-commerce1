using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servises.Abstractions
{
    public interface ICahceService
    {
        public Task<string?> GetCachedValueAsync(string Cachekey);
        public Task SetValueCaheAsync(string Cachekey, object value, TimeSpan duration);
    }
}
