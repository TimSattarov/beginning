using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace repository
{
    public interface IBaseRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task CreateMany(IEnumerable<T> entities);
        Task Update(T entity);
        Task UpdateMany(IEnumerable<T> entities);
        Task Delete(Guid id);
        Task DeleteMany(IEnumerable<Guid> entityIds);
        Task Restore(Guid id);
        Task RestoreMany(IEnumerable<Guid> entityIds);
    }
}