using DropShipping.Domain.Enums;
using DropShipping.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropShipping.DataBase.Extensions
{
    public static class EFExtensions
    {
        public static void ConfigureProductEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .IsRequired();

                entity.Property(e => e.Price)
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .IsRequired();

                entity.HasMany(x => x.Users)
                      .WithMany(y => y.Products)
                      .UsingEntity(j => j.ToTable("UserProducts"));
            });
        }

        public static void ConfigureUserEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.Id);

                entity.Property(x => x.FirstName)
                      .IsRequired()
                      .HasMaxLength(30);

                entity.Property(x => x.LastName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(x => x.Age)
                      .IsRequired();

                entity.Property(x => x.CardNumber)
                      .IsRequired();

                entity.Property(x => x.Email)
                      .IsRequired();

                entity.Property(x => x.Password)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(x => x.PhoneNumber)
                      .IsRequired();

                entity.Property(x => x.Role)
                      .HasConversion<int>()
                      .HasDefaultValue(Roles.User);
            });
        }

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Bob", LastName = "Bobsky", Age = 26, CardNumber = "1234-5678-1512-3242", Email = "bober@gmail.com", Password = "bobersky", PhoneNumber = "123456789", Role = Roles.User },
                new User { Id = 2, FirstName = "Dime", LastName = "Dimesky", Age = 28, CardNumber = "2341-5322-6432-4233", Email = "dime@gmail.com", Password = "dimesky", PhoneNumber = "987654321", Role = Roles.Admin }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "kolichka", Description = "mnogu brza", Price = 20.99, Quantity = 15, UserId = 1 },
                new Product { Id = 2, Name = "motorche", Description = "mnogu brz", Price = 21.99, Quantity = 13, UserId = 2 }
                );
        }
    }
}
