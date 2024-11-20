using DropShipping.DataBase.Context;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.DataBase.Implementations.EFImpementations
{
    public class OrderProductRepository : IRepository<OrderProduct>
    {
        private readonly DropShippingDbContext _context;

        public OrderProductRepository(DropShippingDbContext context)
        {
            _context = context;
        }

        public IQueryable<OrderProduct> GetAll()
        {
            return _context.Set<OrderProduct>();
        }

        public OrderProduct GetById(int id)
        {
            return _context.Set<OrderProduct>().Find(id);
        }

        public void Update(OrderProduct entity)
        {
            _context.Set<OrderProduct>().Update(entity);
        }

        public void Add(OrderProduct entity)
        {
            _context.Set<OrderProduct>().Add(entity);
        }

        public void Delete(OrderProduct entity)
        {
            _context.Set<OrderProduct>().Remove(entity);
        }
    }
}
