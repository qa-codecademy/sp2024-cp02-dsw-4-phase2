using DropShipping.DataBase.Context;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;

namespace DropShipping.DataBase.Implementations.EFImpementations
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly DropShippingDbContext _context;

        public OrderRepository(DropShippingDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Order.ToList();
        }

        public Order GetById(int id)
        {
            return _context.Order.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Order entity)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == entity.Id);

            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Add(Order entity)
        {
            _context.Order.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Order entity)
        {
            _context.Order.Remove(entity);
            _context.SaveChanges();
        }
    }
}
