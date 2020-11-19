using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Services
{
    public class ImageService : IImageService
    {
        private readonly ImageContext _imageContext;

        public ImageService(ImageContext imageContet)
        {
            _imageContext = imageContet;            
        }



        public async Task<Image> Get(Guid id)
        {
            return await _imageContext.Image.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            return await _imageContext.Image.Where(i => i.IsDeleted == false).ToListAsync();
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

            await _imageContext.Image.AddAsync(entity);
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

            await _imageContext.Image.AddRangeAsync(entities);
            await _imageContext.SaveChangesAsync();
        }



        public async Task Update(Image entity)
        {
            entity.LastSavedDate = DateTime.UtcNow;
            entity.LastSavedBy = Guid.NewGuid();   //Заглушка

            _imageContext.Image.Update(entity);
            await _imageContext.SaveChangesAsync();
        }

        public async Task UpdateMany(IEnumerable<Image> entities)
        {
            foreach (var entity in entities)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();      //Заглушка
            }

            _imageContext.Image.UpdateRange(entities);
            await _imageContext.SaveChangesAsync();
        }



        public async Task Delete(Guid id)
        {
            var entity = await _imageContext.Image.FirstOrDefaultAsync(i => i.Id == id);

            if (entity != null)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                entity.IsDeleted = true;

                _imageContext.Image.Update(entity);
                await _imageContext.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(IEnumerable<Guid> entityIds)
        {
            var entities = _imageContext.Image.Where(i => entityIds.Contains(i.Id));

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    entity.LastSavedDate = DateTime.UtcNow;
                    entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                    entity.IsDeleted = true;
                }
                _imageContext.Image.UpdateRange(entities);
                await _imageContext.SaveChangesAsync();
            }
        }

        

        public async Task Restore(Guid id)
        {
            var entity = await _imageContext.Image.FirstOrDefaultAsync(i => i.Id == id);

            if (entity != null)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                entity.IsDeleted = false;

                _imageContext.Image.Update(entity);
                await _imageContext.SaveChangesAsync();
            }
        }

        public async Task RestoreMany(IEnumerable<Guid> entityIds)
        {
            var entities = _imageContext.Image.Where(i => entityIds.Contains(i.Id));

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    entity.LastSavedDate = DateTime.UtcNow;
                    entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                    entity.IsDeleted = false;
                }
                _imageContext.Image.UpdateRange(entities);
                await _imageContext.SaveChangesAsync();
            }
        }
    }
}