using Domain.Contracts;
using E_commerce.Api.Middlewares;

namespace E_commerce.Api.Extentions
{
    public static class WebAppExtentions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.initializeAsync();
            await DbInitializer.initializeIdentityAsync();
            return app;
        }

        public static WebApplication UseCustomExeptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
