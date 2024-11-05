using Servises.Abstractions;
using Servises;
using E_commerce.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace E_commerce.Api.Extentions
{
    public static class PresentationServiceExtention
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection Services)
        {
            Services.AddControllers()
                    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            Services.ConfigureSwagger();
            return Services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter Bearer Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                    {
                    }
                }});
            });
            return Services;
        }
    }
}
