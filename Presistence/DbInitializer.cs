using Domain.Entities.Identity;
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;
using Presistence.Data;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task initializeAsync()
        {
            try
            {
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();

                if (!_storeContext.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null && types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.ProductBrands.Any())
                {
                    var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");

                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    if (Brands is not null && Brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(Brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                //if (!_storeContext.products.Any())
                //{
                //    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");

                //    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                //    if (products is not null && products.Any())
                //    {
                //        await _storeContext.products.AddRangeAsync(products);
                //        await _storeContext.SaveChangesAsync();
                //    }
                //}

                //if (!_storeContext.deliveryMethods.Any())
                //{
                //    var Data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\delivery.json");

                //    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(Data);

                //    if (methods is not null && methods.Any())
                //    {
                //        await _storeContext.deliveryMethods.AddRangeAsync(methods);
                //        await _storeContext.SaveChangesAsync();
                //    }
                //}
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task initializeIdentityAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!_userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    DisplayName = "Super Admin User",
                    Email = "SuperAdminUser@gmail.com",
                    UserName = "SuperAdminUser",
                    PhoneNumber = "1234567890",
                };
                var AdminUser = new User
                {
                    DisplayName = "Admin User",
                    Email = "AdminUser@gmail.com",
                    UserName = "AdminUser",
                    PhoneNumber = "1234567890",
                };

                await _userManager.CreateAsync(superAdminUser,"passw0rd");
                await _userManager.CreateAsync(AdminUser,"passw0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
            }
        }
    }
}
// C:\Users\Electronica Care\source\repos\E-commerce\Infrastructure\Presistence\Data\Seeding\types.json