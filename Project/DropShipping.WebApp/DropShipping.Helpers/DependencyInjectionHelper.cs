using DropShipping.DataBase.Implementations.EFImpementations;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
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

        }
    }
}
