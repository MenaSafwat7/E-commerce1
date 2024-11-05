using Servises.Abstractions;
using Servises;
using Domain.Contracts;
using Presistence.Data;
using Presistence.Repositories;
using Presistence;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Presistence.Identity;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Entities.Identity;

namespace E_commerce.Api.Extentions
{
    public static class InfraStructureExtention
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<IDbInitializer, DbInitializer>();
            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSqlConnection"));
            });
            
            Services.AddDbContext<StoreIdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentitySqlConnection"));
            });
            Services.AddSingleton<IConnectionMultiplexer>(_=> ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

            Services.ConfigureIdentityService();
            Services.ConfigureJWT(configuration);
            return Services;
        }

        public static IServiceCollection ConfigureIdentityService(this IServiceCollection Services)
        {
            Services.AddIdentity<User, IdentityRole>(Options =>
            {
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequiredLength = 8;
                Options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<StoreIdentityContext>();

            return Services;
        }

        public static IServiceCollection ConfigureJWT(this IServiceCollection Services, IConfiguration configuration)
        {
            var jwtoptions = configuration.GetSection("JWTOptions").Get<JWTOptions>();
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtoptions.Issure,
                    ValidAudience = jwtoptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey))
                };
            });

            Services.AddAuthorization();
                
            return Services;
        }
    }
}
