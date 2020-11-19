using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageService.Entities;

namespace ImageService.Services
{
    public interface IImageService
    {
        Task<Image> Get(Guid id);
        Task<IEnumerable<Image>> GetAll();
        
        Task Create(Image entity);
        Task CreateMany(IEnumerable<Image> entities);

        Task Update(Image entity);
        Task UpdateMany(IEnumerable<Image> entities);

        Task Delete(Guid id);
        Task DeleteMany(IEnumerable<Guid> entityIds);

        Task Restore(Guid id);
        Task RestoreMany(IEnumerable<Guid> entityIds);
    }
}