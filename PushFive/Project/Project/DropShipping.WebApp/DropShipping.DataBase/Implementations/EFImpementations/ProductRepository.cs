using DropShipping.DataBase.Context;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;

namespace DropShipping.DataBase.Implementations.EFImpementations
{
    public class ProductRepository : IRepository<Product>
    {

        private readonly DropShippingDbContext _context;

        public ProductRepository(DropShippingDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Product.ToList();
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
    }
}
