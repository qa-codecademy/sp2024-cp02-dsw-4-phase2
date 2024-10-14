using DropShipping.DataBase.Implementations.EFImpementations;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using DropShipping.Services.Implementations;
using DropShipping.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DropShipping.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Category>, CategoryRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IContactService, ContactService>(); // DELETE IF NOT WORK
        }
    }
}