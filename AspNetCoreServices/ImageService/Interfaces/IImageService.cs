using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageService.Models;

namespace ImageService.Interfaces
{
    public interface IImageService
    {
        Task<ImageModel> Get(Guid id);
        Task<IEnumerable<ImageModel>> GetAll();
        
        Task Create(ImageModel entity);
        Task CreateMany(IEnumerable<ImageModel> entities);

        Task Update(ImageModel entity);
        Task UpdateMany(IEnumerable<ImageModel> entities);

        Task Delete(Guid id);
        Task DeleteMany(IEnumerable<Guid> entityIds);

        Task Restore(Guid id);
        Task RestoreMany(IEnumerable<Guid> entityIds);
    }
}