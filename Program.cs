
using Domain.Contracts;
using E_commerce.Api.Extentions;
using E_commerce.Api.Factories;
using E_commerce.Api.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using Servises;
using Servises.Abstractions;

namespace E_commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            #region Services
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddPresentationServices();
            #endregion
            var app = builder.Build();

            #region PipeLines
            app.UseCustomExeptionMiddleware();

            await app.SeedDbAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            app.Run(); 
            #endregion

        }
    }
}
