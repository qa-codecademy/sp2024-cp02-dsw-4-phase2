using DropShipping.Domain.Enums;
using DropShipping.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropShipping.DataBase.Extensions
{
    public static class EFExtensions
    {
        //public static void ConfigureProductEntity(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>(entity =>
        //    {
        //        entity.ToTable("Product");

        //        entity.HasKey(e => e.Id);

        //        entity.Property(e => e.Name)
        //              .IsRequired();

        //        entity.Property(e => e.Description)
        //              .IsRequired();

        //        entity.Property(e => e.Price)
        //              .IsRequired();

        //        entity.Property(e => e.Quantity)
        //              .IsRequired();

        //        entity.HasOne(x => x.Category)
        //              .WithMany(y => y.Products)
        //              .HasForeignKey(i => i.CategoryId);
        //    });
        //}

        //public static void ConfigureUserEntity(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.ToTable("User");

        //        entity.HasKey(e => e.Id);

        //        entity.Property(x => x.FirstName)
        //              .IsRequired()
        //              .HasMaxLength(30);

        //        entity.Property(x => x.LastName)
        //              .IsRequired()
        //              .HasMaxLength(50);

        //        entity.Property(x => x.Username)
        //              .IsRequired()
        //              .HasMaxLength(50);

        //        entity.Property(x => x.Email)
        //              .IsRequired();

        //        entity.Property(x => x.Password)
        //              .IsRequired()
        //              .HasMaxLength(50);

        //        entity.Property(x => x.PhoneNumber)
        //              .IsRequired();

        //        entity.Property(x => x.Role)
        //              .HasConversion<int>()
        //              .HasDefaultValue(Roles.User);

        //        entity.HasMany(x => x.Orders)
        //              .WithOne(y => y.User)
        //              .HasForeignKey(e => e.UserId);
        //    });
        //}

        //public static void ConfigureCategoryEntity(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>(entity =>
        //    {
        //        entity.ToTable("Category");

        //        entity.HasKey(e => e.Id);

        //        entity.Property(x => x.Name)
        //              .IsRequired();

        //        entity.HasMany(x => x.Products)
        //              .WithOne(y => y.Category)
        //              .HasForeignKey(e => e.CategoryId);
        //    });
        //}

        //public static void ConfigureOrderEntity(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Order>(entity =>
        //    {
        //        entity.ToTable("Orders");

        //        entity.HasKey(x => x.Id);

        //        entity.HasOne(x => x.User)
        //              .WithMany(y => y.Orders)
        //              .HasForeignKey(e => e.UserId);
        //    });
        //}

        //public static void ConfigureContactEntity(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Contact>(entity =>
        //    {
        //        entity.ToTable("Contact");

        //        entity.HasKey(x => x.Id);

        //        entity.Property(x => x.Email)
        //              .IsRequired();

        //        entity.Property(x => x.Description)
        //              .IsRequired();
        //    });
        //}

        //public static void ConfigureOrderProductEntity(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderProduct>(entity =>
        //    {
        //        entity.ToTable("OrderProduct");

        //        entity.HasKey(op => new { op.OrdersId, op.ProductsId });

        //        entity.HasOne(op => op.Order)
        //              .WithMany(o => o.OrderProducts)
        //              .HasForeignKey(op => op.OrdersId);

        //        entity.HasOne(op => op.Product)
        //              .WithMany(p => p.OrderProducts)
        //              .HasForeignKey(op => op.ProductsId);
        //    });
        //}
        // Configure Product entity
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

                entity.HasOne(x => x.Category)
                      .WithMany(y => y.Products)
                      .HasForeignKey(i => i.CategoryId);

                entity.HasMany(x => x.OrderProducts)
                      .WithOne(y => y.Product)
                      .HasForeignKey(y => y.ProductsId);
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

                entity.Property(x => x.Username)
                      .IsRequired()
                      .HasMaxLength(50);

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

                entity.HasMany(x => x.Orders)
                      .WithOne(y => y.User)
                      .HasForeignKey(e => e.UserId);
            });
        }

        public static void ConfigureCategoryEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(e => e.Id);

                entity.Property(x => x.Name)
                      .IsRequired();

                entity.HasMany(x => x.Products)
                      .WithOne(y => y.Category)
                      .HasForeignKey(e => e.CategoryId);

            });
        }

        public static void ConfigureOrderEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.User)
                      .WithMany(y => y.Orders)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(x => x.OrderProducts)
                      .WithOne(y => y.Order)
                      .HasForeignKey(y => y.OrdersId);
            });
        }

        public static void ConfigureContactEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Email)
                      .IsRequired();

                entity.Property(x => x.Description)
                      .IsRequired();
            });
        }

        public static void ConfigureOrderProductEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("OrderProduct");

                entity.HasKey(op => new { op.OrdersId, op.ProductsId });

                entity.HasOne(op => op.Order)
                      .WithMany(o => o.OrderProducts)
                      .HasForeignKey(op => op.OrdersId);

                entity.HasOne(op => op.Product)
                      .WithMany(p => p.OrderProducts)
                      .HasForeignKey(op => op.ProductsId);
            });
        }
    }
}