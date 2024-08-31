using FluentResults;
using Users.Infrastructure.DTO;

namespace Users.Infrastructure.Services.Interfaces;

public interface IService<T, ID> where T : class, IDTO<ID>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(ID id);
    Task<Result<T>> Add(T entity);
    Task<Result> Update(ID id, T entity);
    Task<bool> Delete(ID id);
}
