using CoffeeManager.Model;

namespace CoffeeManager.Interfaces
{
    public interface IManagerService<T> 
    {
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
