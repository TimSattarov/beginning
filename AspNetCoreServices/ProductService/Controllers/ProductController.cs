using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Interfaces;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }



        [HttpGet("{id}")]
        public async Task<ProductModel> Get(Guid id)
        {
            return await _productService.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetAll()
        {            
            return await _productService.GetAll();
        }



        [Authorize]
        [HttpPost("{id}")]
        public async Task Create(ProductModel product)
        {
            await _productService.Create(product);            
        }

        [Authorize]
        [HttpPost]
        public async Task CreateMany(IEnumerable<ProductModel> products)
        {
            await _productService.CreateMany(products);
        }



        [Authorize]
        [HttpPut("{id}")]
        public async Task Update(ProductModel product)
        {
            await _productService.Update(product);
        }

        [Authorize]
        [HttpPut]
        public async Task UpdateMany(IEnumerable<ProductModel> products)
        {            
            await _productService.UpdateMany(products);
        }



        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _productService.Delete(id);
        }

        [Authorize]
        [HttpDelete]
        public async Task DeleteMany(IEnumerable<Guid> entityIds)
        {
            await _productService.DeleteMany(entityIds);
        }



        [Authorize]
        [HttpPut("/api/Product/restore/{id}")]
        public async Task Restore(Guid id)
        {
            await _productService.Restore(id);
        }

        [Authorize]
        [HttpPut("/api/Product/restore")]
        public async Task RestoreMany(IEnumerable<Guid> entityIds)
        {
            await _productService.RestoreMany(entityIds);
        }
    }
}