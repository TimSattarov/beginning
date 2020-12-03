using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImageService.Entities;
using ImageService.Interfaces;
using ImageService.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageService.Services
{
    public class ImageService : IImageService
    {
        private readonly ImageContext _imageContext;
        private readonly IMapper _mapper;
        private readonly IYaDiskService _yaDiskService;

        public ImageService(ImageContext imageContext, 
            IMapper mapper, 
            IYaDiskService yaDiskService)
        {
            _imageContext = imageContext;
            _mapper = mapper;
            _yaDiskService = yaDiskService;
        }



        public async Task<ImageModel> Get(Guid id)
        {
            var imageEntity = await _imageContext.Image.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            var image = _mapper.Map<ImageModel>(imageEntity);

            return image;
        }

        public async Task<IEnumerable<ImageModel>> GetAll()
        {
            var imageEntities = await _imageContext.Image.Where(i => i.IsDeleted == false).ToListAsync();
            var images = _mapper.Map<IEnumerable<ImageModel>>(imageEntities);

            return images;
        }



        public async Task Create(ImageModel image)
        {
            if (image.Id == Guid.Empty)
            {
                image.Id = Guid.NewGuid();
            }

            await _yaDiskService.PostImage(image.Path);
            image.Url = await _yaDiskService.GetUrl(image.Path);

            var entity = _mapper.Map<Image>(image);

            entity.CreatedDate = DateTime.UtcNow;
            entity.LastSavedDate = DateTime.UtcNow;

            entity.CreatedBy = Guid.NewGuid();        //Заглушка
            entity.LastSavedBy = Guid.NewGuid();      //Заглушка

            await _imageContext.Image.AddAsync(entity);
            await _imageContext.SaveChangesAsync();
        }

        public async Task CreateMany(IEnumerable<ImageModel> images)
        {
            foreach (var image in images)
            {
                var pathOnYaDisk = await _yaDiskService.PostImage(image.Path);
                image.Url = await _yaDiskService.GetUrl(pathOnYaDisk);
            }

            var entities = _mapper.Map<IEnumerable<Image>>(images);

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



        public async Task Update(ImageModel image)
        {
            var entity = _mapper.Map<Image>(image);

            entity.LastSavedDate = DateTime.UtcNow;
            entity.LastSavedBy = Guid.NewGuid();   //Заглушка

            _imageContext.Image.Update(entity);
            await _imageContext.SaveChangesAsync();
        }

        public async Task UpdateMany(IEnumerable<ImageModel> images)
        {
            var entities = _mapper.Map<IEnumerable<Image>>(images);

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