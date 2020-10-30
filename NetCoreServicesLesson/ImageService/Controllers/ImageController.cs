using System.Collections.Generic;
using ImageService.Models;
using ImageService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;


        public ImageController(IImageService imageService)
        {
            _imageService = imageService;        
        }

        [HttpGet]
        public ActionResult<IEnumerable<ImageModel>> GetAll()
        {
            var collection = _imageService.GetAll();

            if (collection == null)
            {
                return BadRequest("Products not found");
            }

            return Ok(collection);
        }

        [HttpGet("{id}")]
        public ActionResult<ImageModel> Get(int id)
        {
            var model = _imageService.Get(id);

            if (model == null)
            {
                return BadRequest("Product not found");
            }

            return Ok(model);
        }
    }
}