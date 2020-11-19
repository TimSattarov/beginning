using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task<ProductModel> Get(Guid id);
        Task<IEnumerable<ProductModel>> GetAll();

        Task Create(ProductModel entity);
        Task CreateMany(IEnumerable<ProductModel> entities);

        Task Update(ProductModel entity);
        Task UpdateMany(IEnumerable<ProductModel> entities);

        Task Delete(Guid id);
        Task DeleteMany(IEnumerable<Guid> entityIds);

        Task Restore(Guid id);
        Task RestoreMany(IEnumerable<Guid> entityIds);

    }
}
