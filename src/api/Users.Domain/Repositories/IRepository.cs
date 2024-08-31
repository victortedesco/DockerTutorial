using Users.Domain.Models;

namespace Users.Domain.Repositories;

public interface IRepository<T, ID> where T : class, IEntity<ID>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(ID id);
    Task<T?> Add(T entity);
    Task<bool> Update(ID id, T entity);
    Task<bool> Delete(ID id);
}
