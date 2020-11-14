using System;
using System.Collections.Generic;
using ProductService.Models;

namespace ProductService.Services
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAll();
        ProductModel Get(Guid id);
    }
}