using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Clients;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private IEnumerable<ProductModel> _products;

        private readonly IImageClient _imageClient;
        private readonly IPriceClient _priceClient;

        public ProductService(IImageClient imageClient, IPriceClient priceClient)
        {
            _imageClient = imageClient;
            _priceClient = priceClient;
            Init();
        }

        public IEnumerable<ProductModel> GetAll() => _products;


        public ProductModel Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id); 
            if (product == null)
            {
                return null;
            }
            return product;
        }        

        public void Init()
        {
            _products = new List<ProductModel>
            {
                new ProductModel() 
                {
                    Id = 0, 
                    Brand = "Samsung", 
                    Model = "A71", 
                    Category = "Phone",
                    Images = _imageClient.GetAll().Result.Where(x => x.ProductId == 0),
                    Prices = _priceClient.GetAll().Result.Where(x => x.ProductId == 0)
                },
                new ProductModel() 
                {
                    Id = 1, 
                    Brand = "Samsung", 
                    Model = "S10", 
                    Category = "Phone",
                    Images = _imageClient.GetAll().Result.Where(x => x.ProductId == 1),
                    Prices = _priceClient.GetAll().Result.Where(x => x.ProductId == 1)
                },
                new ProductModel() 
                {
                    Id = 2, 
                    Brand = "Samsung", 
                    Model = "UE24H", 
                    Category = "TV",
                    Images = _imageClient.GetAll().Result.Where(x => x.ProductId == 2),
                    Prices = _priceClient.GetAll().Result.Where(x => x.ProductId == 2)
                },
                new ProductModel() 
                {
                    Id = 3, 
                    Brand = "Acer", 
                    Model = "Aspire7", 
                    Category = "Notebook",
                    Images = _imageClient.GetAll().Result.Where(x => x.ProductId == 3),
                    Prices = _priceClient.GetAll().Result.Where(x => x.ProductId == 3)
                },
                new ProductModel() 
                {
                    Id = 4, 
                    Brand = "Dell", 
                    Model = "Inspirion", 
                    Category = "Notebook",
                    Images = _imageClient.GetAll().Result.Where(x => x.ProductId == 4),
                    Prices = _priceClient.GetAll().Result.Where(x => x.ProductId == 4)
                },
                new ProductModel() 
                {
                    Id = 5, 
                    Brand = "Asus", 
                    Model = "ZenBook", 
                    Category = "Notebook",
                    Images = _imageClient.GetAll().Result.Where(x => x.ProductId == 5),
                    Prices = _priceClient.GetAll().Result.Where(x => x.ProductId == 5)
                }
            };
        }
    }
}