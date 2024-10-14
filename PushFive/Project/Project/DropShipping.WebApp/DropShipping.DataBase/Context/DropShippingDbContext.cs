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
            //asd // NAPRAVI MIGRACIJA
            //email
        }

        public DbSet<User> User { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Contact> Contact { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserEntity();

            modelBuilder.ConfigureProductEntity();

            modelBuilder.ConfigureCategoryEntity();

            modelBuilder.ConfigureOrderEntity();

            modelBuilder.ConfigureContactEntity();

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }
    }
}
