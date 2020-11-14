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


        public ProductModel Get(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id); 
            if (product == null)
            {
                return null;
            }
            return product;
        }        

        Guid ProductGuid;
        private Guid GetGuid() => ProductGuid = Guid.NewGuid();

        public void Init()
        {
             var _images = _imageClient.GetAll();
             var _prices = _priceClient.GetAll();

            _products = new List<ProductModel>
            {
                new ProductModel() 
                {
                    Id = GetGuid(), 
                    Brand = "Samsung", 
                    Model = "A71", 
                    Category = "Phone",
                    Images = _images.Result.Where(x => x.ProductId == ProductGuid),
                    Prices = _prices.Result.Where(x => x.ProductId == ProductGuid)
                },
                new ProductModel() 
                {
                    Id = GetGuid(), 
                    Brand = "Samsung", 
                    Model = "S10", 
                    Category = "Phone",
                    Images = _images.Result.Where(x => x.ProductId == ProductGuid),
                    Prices = _prices.Result.Where(x => x.ProductId == ProductGuid)
                },
                new ProductModel() 
                {
                    Id = GetGuid(), 
                    Brand = "Samsung", 
                    Model = "UE24H", 
                    Category = "TV",
                    Images = _images.Result.Where(x => x.ProductId == ProductGuid),
                    Prices = _prices.Result.Where(x => x.ProductId == ProductGuid)
                },
                new ProductModel() 
                {
                    Id = GetGuid(), 
                    Brand = "Acer", 
                    Model = "Aspire7", 
                    Category = "Notebook",
                    Images = _images.Result.Where(x => x.ProductId == ProductGuid),
                    Prices = _prices.Result.Where(x => x.ProductId == ProductGuid)
                },
                new ProductModel() 
                {
                    Id = GetGuid(), 
                    Brand = "Dell", 
                    Model = "Inspirion", 
                    Category = "Notebook",
                    Images = _images.Result.Where(x => x.ProductId == ProductGuid),
                    Prices = _prices.Result.Where(x => x.ProductId == ProductGuid)
                },
                new ProductModel() 
                {
                    Id = GetGuid(), 
                    Brand = "Asus", 
                    Model = "ZenBook", 
                    Category = "Notebook",
                    Images = _images.Result.Where(x => x.ProductId == ProductGuid),
                    Prices = _prices.Result.Where(x => x.ProductId == ProductGuid)
                },



                new ProductModel()
                {
                    Id = Guid.Parse("e44fd6dd-2bf3-477a-b11c-cd1c289295f3"),
                    Brand = "test1", 
                    Model = "test1", 
                    Category = "test1",
                    Images = _images.Result.Where(x => x.ProductId == Guid.Parse("e44fd6dd-2bf3-477a-b11c-cd1c289295f3")),
                    Prices = _prices.Result.Where(x => x.ProductId == Guid.Parse("e44fd6dd-2bf3-477a-b11c-cd1c289295f3"))
                },
                new ProductModel()
                {
                    Id = Guid.Parse("f75787b0-9f30-4549-b2d9-10dd6dddc930"),
                    Brand = "test2", 
                    Model = "test2", 
                    Category = "test2",
                    Images = _images.Result.Where(x => x.ProductId == Guid.Parse("f75787b0-9f30-4549-b2d9-10dd6dddc930")),
                    Prices = _prices.Result.Where(x => x.ProductId == Guid.Parse("f75787b0-9f30-4549-b2d9-10dd6dddc930"))
                }

            };
        }
    }
}