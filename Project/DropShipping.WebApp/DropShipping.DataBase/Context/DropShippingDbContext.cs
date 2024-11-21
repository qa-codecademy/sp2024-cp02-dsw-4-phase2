using DropShipping.DataBase.Extensions;
using DropShipping.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DropShipping.DataBase.Context
{
    public class DropShippingDbContext : DbContext
    {
        public DropShippingDbContext(DbContextOptions<DropShippingDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserEntity();

            modelBuilder.ConfigureProductEntity();

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }
    }
}
