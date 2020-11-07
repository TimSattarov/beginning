using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;        
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> GetAll()
        {
            var collection = _productService.GetAll();

            if (collection == null)
            {
                return BadRequest("Products not found");
            }

            return Ok(collection);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductModel> Get(int id)
        {
            var model = _productService.Get(id);

            if (model == null)
            {
                return BadRequest("Product not found");
            }

            return Ok(model);
        }
    }
}