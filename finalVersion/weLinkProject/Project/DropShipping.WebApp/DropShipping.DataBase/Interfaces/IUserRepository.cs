using DropShipping.Domain.Models;

namespace DropShipping.DataBase.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User LogInUser(string email, string hashedPassword);

        User GetUserByUsername(string username);
    }
}
