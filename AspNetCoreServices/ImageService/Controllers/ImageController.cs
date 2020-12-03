using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ImageService.Entities;
using ImageService.Interfaces;
using ImageService.Models;
using ImageService.Services;
using Microsoft.AspNetCore.Authorization;
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

        public ImageController(IImageService imageService, ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }



        [HttpGet("{id}")]
        public async Task<ImageModel> Get(Guid id)
        {
            return await _imageService.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<ImageModel>> GetAll()
        {
            return await _imageService.GetAll();
        }



        [Authorize]
        [HttpPost("{id}")]
        public async Task Create(ImageModel image)
        {
            await _imageService.Create(image);
        }

        [Authorize]
        [HttpPost]
        public async Task CreateMany(IEnumerable<ImageModel> images)
        {
            await _imageService.CreateMany(images);
        }



        [Authorize]
        [HttpPut("{id}")]
        public async Task Update(ImageModel image)
        {
            await _imageService.Update(image);
        }

        [Authorize]
        [HttpPut]
        public async Task UpdateMany(IEnumerable<ImageModel> images)
        {
            await _imageService.UpdateMany(images);
        }



        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _imageService.Delete(id);
        }

        [Authorize]
        [HttpDelete]
        public async Task DeleteMany(IEnumerable<Guid> entityIds)
        {
            await _imageService.DeleteMany(entityIds);
        }



        [Authorize]
        [HttpPut("/api/Image/restore/{id}")]
        public async Task Restore(Guid id)
        {
            await _imageService.Restore(id);
        }

        [Authorize]
        [HttpPut("/api/Image/restore")]
        public async Task RestoreMany(IEnumerable<Guid> entityIds)
        {
            await _imageService.RestoreMany(entityIds);
        }
    }
}