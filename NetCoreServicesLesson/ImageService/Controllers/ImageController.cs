using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ImageService.Entities;
using ImageService.Models;
using ImageService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImageController> _logger;
        private readonly IMapper _mapper;

        public ImageController(IImageService imageService, ILogger<ImageController> logger, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
            _logger = logger;
        }



        [HttpGet("{id}")]
        public async Task<ImageModel> Get(Guid id)
        {
            var imageEntity = await _imageService.Get(id);
            var image = _mapper.Map<ImageModel>(imageEntity);
            return image;
        }

        [HttpGet]
        public async Task<IEnumerable<ImageModel>> GetAll()
        {
            var imageEntities = await _imageService.GetAll();
            var images = _mapper.Map<IEnumerable<ImageModel>>(imageEntities);
            return images;
        }



        [HttpPost("{id}")]
        public async Task Create(ImageModel image)
        {
            var imageEntity = _mapper.Map<Image>(image);
            await _imageService.Create(imageEntity);
        }

        [HttpPost]
        public async Task CreateMany(IEnumerable<ImageModel> images)
        {
            var imageEntities = _mapper.Map<IEnumerable<Image>>(images);
            await _imageService.CreateMany(imageEntities);
        }



        [HttpPut("{id}")]
        public async Task Update(ImageModel image)
        {
            var imageEntity = _mapper.Map<Image>(image);
            await _imageService.Update(imageEntity);
        }

        [HttpPut]
        public async Task UpdateMany(IEnumerable<ImageModel> images)
        {
            var imageEntities = _mapper.Map<IEnumerable<Image>>(images);
            await _imageService.UpdateMany(imageEntities);
        }



        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _imageService.Delete(id);
        }

        [HttpDelete]
        public async Task DeleteMany(IEnumerable<Guid> entityIds)
        {
            await _imageService.DeleteMany(entityIds);
        }
    }
}