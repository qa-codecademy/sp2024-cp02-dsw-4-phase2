using DropShipping.Domain.Models;

namespace DropShipping.DataBase.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByIds(IEnumerable<int> ids);
    }
}
