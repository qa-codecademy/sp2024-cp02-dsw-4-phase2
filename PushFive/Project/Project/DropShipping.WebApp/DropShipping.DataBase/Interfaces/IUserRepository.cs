using DropShipping.Domain.Models;

namespace DropShipping.DataBase.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User LogInUser(string username, string hashedPassword);

        User GetUserByUsername(string username);
    }
}
