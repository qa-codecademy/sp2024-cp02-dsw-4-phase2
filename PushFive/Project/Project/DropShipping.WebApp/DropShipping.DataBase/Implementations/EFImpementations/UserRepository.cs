using DropShipping.DataBase.Context;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;

namespace DropShipping.DataBase.Implementations.EFImpementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DropShippingDbContext _context;

        public UserRepository(DropShippingDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.User.ToList();
        }

        public User GetById(int id)
        {
            return _context.User.FirstOrDefault(x => x.Id == id);
        }

        public void Update(User entity)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == entity.Id);

            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Add(User entity)
        {
            _context.User.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.User.Remove(entity);
            _context.SaveChanges();
        }

        public User LogInUser(string username, string hashedPassword)
        {
            return _context.User.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);
        }

        public User GetUserByUsername(string username)
        {
            return _context.User.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());

        }
    }
}
