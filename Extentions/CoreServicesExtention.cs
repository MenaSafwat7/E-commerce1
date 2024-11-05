using Servises.Abstractions;
using Servises;
using Shared;
using Presistence.Repositories;
using Domain.Contracts;

namespace E_commerce.Api.Extentions
{
    public static class CoreServicesExtention
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAutoMapper(typeof(Servises.AssemblyReference).Assembly);
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddScoped<ICacheRepository, CacheRepository>();

            Services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            return Services;
        }
    }
}
