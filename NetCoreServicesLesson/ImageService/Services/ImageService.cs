using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageService.Clients;
using ImageService.Entities;
using ImageService.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ImageService.Services
{
    public class ImageService : IImageService
    {
        private readonly ImageContext _imageContext;
        private readonly IPoligonClient _poligonClient;

        public ImageService(ImageContext imageContet, IPoligonClient poligonClient)
        {
            _imageContext = imageContet;
            _poligonClient = poligonClient;
        }



        public async Task<PoligonModel> GetAllPoligon()
        {
            return await _poligonClient.GetAll();
        }





        public async Task<Image> Get(Guid id)
        {
            return await _imageContext.Images.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            return await _imageContext.Images.Where(i => i.IsDeleted == false).ToListAsync();
        }



        public async Task Create(Image entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            entity.CreatedDate = DateTime.UtcNow;
            entity.LastSavedDate = DateTime.UtcNow;

            entity.CreatedBy = Guid.NewGuid();        //Заглушка
            entity.LastSavedBy = Guid.NewGuid();      //Заглушка

            await _imageContext.Images.AddAsync(entity);
            await _imageContext.SaveChangesAsync();
        }

        public async Task CreateMany(IEnumerable<Image> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreatedDate = DateTime.UtcNow;
                entity.LastSavedDate = DateTime.UtcNow;

                entity.CreatedBy = Guid.NewGuid();        //Заглушка
                entity.LastSavedBy = Guid.NewGuid();      //Заглушка
            }

            await _imageContext.Images.AddRangeAsync(entities);
            await _imageContext.SaveChangesAsync();
        }



        public async Task Update(Image entity)
        {
            entity.LastSavedDate = DateTime.UtcNow;
            entity.LastSavedBy = Guid.NewGuid();   //Заглушка

            _imageContext.Images.Update(entity);
            await _imageContext.SaveChangesAsync();
        }

        public async Task UpdateMany(IEnumerable<Image> entities)
        {
            foreach (var entity in entities)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();      //Заглушка
            }

            _imageContext.Images.UpdateRange(entities);
            await _imageContext.SaveChangesAsync();
        }



        public async Task Delete(Guid id)
        {
            var entity = await _imageContext.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (entity != null)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                entity.IsDeleted = true;

                _imageContext.Images.Update(entity);
                await _imageContext.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(IEnumerable<Guid> entityIds)
        {
            var entities = _imageContext.Images.Where(i => entityIds.Contains(i.Id));

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    entity.LastSavedDate = DateTime.UtcNow;
                    entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                    entity.IsDeleted = true;
                }
                _imageContext.Images.UpdateRange(entities);
                await _imageContext.SaveChangesAsync();
            }
        }

        

        public async Task Restore(Guid id)
        {
            var entity = await _imageContext.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (entity != null)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                entity.IsDeleted = false;

                _imageContext.Images.Update(entity);
                await _imageContext.SaveChangesAsync();
            }
        }

        public async Task RestoreMany(IEnumerable<Guid> entityIds)
        {
            var entities = _imageContext.Images.Where(i => entityIds.Contains(i.Id));

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    entity.LastSavedDate = DateTime.UtcNow;
                    entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                    entity.IsDeleted = false;
                }
                _imageContext.Images.UpdateRange(entities);
                await _imageContext.SaveChangesAsync();
            }
        }
    }
}