namespace DropShipping.DataBase
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();

        T GetById(int id);

        void Update(T entity);

        void Add(T entity);

        void Delete(T entity);
    }
}
