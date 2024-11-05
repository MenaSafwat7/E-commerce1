using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    internal class RedisCacheAttribute(int durationInSec = 60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cahceService;
            string cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetCachedValueAsync(cacheKey);

            if(result is not null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "Application/json",
                    StatusCode = (int)HttpStatusCode.OK,
                };
                return;
            }

            var resultContext = await next.Invoke();
            if(resultContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetValueCaheAsync(cacheKey, okObjectResult, TimeSpan.FromSeconds(durationInSec));
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);

            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                KeyBuilder.Append($"{item.Key}-{item.Value}");
            }
            return KeyBuilder.ToString();
        }
    }
}
