using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ImageService.Entities;
using ImageService.Models;
using ImageService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageServiceTest _imageServiceTest;
        private readonly IMapper _mapper;


        public ImageController(IImageServiceTest imageServiceTest, IMapper mapper)
        {
            _imageServiceTest = imageServiceTest;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ImageModel>> Get()
        {
            var imageEntities = await _imageServiceTest.GetAll();
            var images = _mapper.Map<IEnumerable<ImageModel>>(imageEntities);
            return images;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<ImageModel>> GetAll()
        //{
        //    var collection = _imageService.GetAll();

        //    if (collection == null)
        //    {
        //        return BadRequest("Images not found");
        //    }

        //    return Ok(collection);
        //}

        //[HttpGet("{id}")]
        //public ActionResult<ImageModel> Get(int id)
        //{
        //    var model = _imageService.Get(id);

        //    if (model == null)
        //    {
        //        return BadRequest("Image not found");
        //    }

        //    return Ok(model);
        //}

    }
}