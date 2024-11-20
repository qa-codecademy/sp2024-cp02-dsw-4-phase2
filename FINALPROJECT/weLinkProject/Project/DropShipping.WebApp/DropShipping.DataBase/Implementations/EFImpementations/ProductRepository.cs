using DropShipping.DataBase.Context;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropShipping.DataBase.Implementations.EFImpementations
{
    public class ProductRepository : IProductRepository
    {

        private readonly DropShippingDbContext _context;

        public ProductRepository(DropShippingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Product;
        }

        public Product GetById(int id)
        {
            return _context.Product.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Product entity)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == entity.Id);

            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Add(Product entity)
        {
            _context.Product.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _context.Product.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetByIds(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                Console.WriteLine("GetByIds: No IDs provided.");
                return new List<Product>();
            }

            //Console.WriteLine("GetByIds: Querying for IDs - " + string.Join(",", ids));


            var products = _context.Product.Where(p => ids.Contains(p.Id)).ToList();

            Console.WriteLine("GetByIds: Products found - " + products.Count);
            return products;
        }
    }
}
