global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;

namespace Presistence.Identity
{
    public class StoreIdentityContext : IdentityDbContext
    {
        public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable(nameof(Address));
        }
    }
}
