using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductService.Clients;
using ProductService.Entity;
using ProductService.Interfaces;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;
        private readonly IImageClient _imageClient;
        private readonly IPriceClient _priceClient;
        


        public ProductService(
            ProductContext productContext, 
            IMapper mapper, 
            IImageClient imageClient, 
            IPriceClient priceClient)
        {
            _productContext = productContext;
            _mapper = mapper;         
            _priceClient = priceClient; 
            _imageClient = imageClient;
        }


        public async Task<ProductModel> Get(Guid id)
        {
            var _images = await _imageClient.GetAll();
            var _prices = await _priceClient.GetAll();

            var productEntity = await _productContext.Product.FirstOrDefaultAsync(i => i.Id == id && i.IsDeleted == false);
            var product = _mapper.Map<ProductModel>(productEntity);

            product.Images = _images.Where(x => x.ProductId == product.Id);
            product.Prices = _prices.Where(x => x.ProductId == product.Id);

            return product;
        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            var _images = await _imageClient.GetAll();
            var _prices = await _priceClient.GetAll();

            var productEntities = await _productContext.Product.Where(i => i.IsDeleted == false).ToListAsync();
            var products = _mapper.Map<IEnumerable<ProductModel>>(productEntities);

            foreach (var item in products)
            {
                item.Images = _images.Where(x => x.ProductId == item.Id);
                item.Prices = _prices.Where(x => x.ProductId == item.Id);
            }

            return products;
        }



        public async Task Create(ProductModel product)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
                foreach (var itemImage in product.Images)
                {
                    itemImage.ProductId = product.Id;
                }
                foreach (var itemPrice in product.Prices)
                {
                    itemPrice.ProductId = product.Id;
                }
            }

            await _imageClient.CreateMany(product.Images);
            await _priceClient.CreateMany(product.Prices);

            var entity = _mapper.Map<ProductDbModel>(product);

            entity.CreatedDate = DateTime.UtcNow;
            entity.LastSavedDate = DateTime.UtcNow;

            entity.CreatedBy = Guid.NewGuid();        //Заглушка
            entity.LastSavedBy = Guid.NewGuid();      //Заглушка

            await _productContext.Product.AddAsync(entity);
            await _productContext.SaveChangesAsync();
        }

        public async Task CreateMany(IEnumerable<ProductModel> products)
        {
            foreach (var itemProduct in products)
            {
                if (itemProduct.Id == Guid.Empty)
                {
                    itemProduct.Id = Guid.NewGuid();
                    foreach (var itemImage in itemProduct.Images)
                    {
                        itemImage.ProductId = itemProduct.Id;
                    }

                    foreach (var itemPrice in itemProduct.Prices)
                    {
                        itemPrice.ProductId = itemProduct.Id;
                    }
                }

                await _imageClient.CreateMany(itemProduct.Images);
                await _priceClient.CreateMany(itemProduct.Prices);
            }

            var entities = _mapper.Map<IEnumerable<ProductDbModel>>(products);

            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.LastSavedDate = DateTime.UtcNow;

                entity.CreatedBy = Guid.NewGuid();        //Заглушка
                entity.LastSavedBy = Guid.NewGuid();      //Заглушка
            }

            await _productContext.Product.AddRangeAsync(entities);
            await _productContext.SaveChangesAsync();
        }



        public async Task Update(ProductModel product)
        {
            var entity = _mapper.Map<ProductDbModel>(product);

            entity.LastSavedDate = DateTime.UtcNow;
            entity.LastSavedBy = Guid.NewGuid();   //Заглушка

            _productContext.Product.Update(entity);
            await _productContext.SaveChangesAsync();
        }

        public async Task UpdateMany(IEnumerable<ProductModel> products)
        {
            var entities = _mapper.Map<IEnumerable<ProductDbModel>>(products);

            foreach (var entity in entities)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();      //Заглушка
            }

            _productContext.Product.UpdateRange(entities);
            await _productContext.SaveChangesAsync();
        }



        public async Task Delete(Guid id)
        {
            var entity = await _productContext.Product.FirstOrDefaultAsync(i => i.Id == id);

            if (entity != null)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                entity.IsDeleted = true;

                _productContext.Product.Update(entity);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(IEnumerable<Guid> entityIds)
        {
            var entities = _productContext.Product.Where(i => entityIds.Contains(i.Id));

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    entity.LastSavedDate = DateTime.UtcNow;
                    entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                    entity.IsDeleted = true;
                }
                _productContext.Product.UpdateRange(entities);
                await _productContext.SaveChangesAsync();
            }
        }



        public async Task Restore(Guid id)
        {
            var entity = await _productContext.Product.FirstOrDefaultAsync(i => i.Id == id);

            if (entity != null)
            {
                entity.LastSavedDate = DateTime.UtcNow;
                entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                entity.IsDeleted = false;

                _productContext.Product.Update(entity);
                await _productContext.SaveChangesAsync();
            }
        }

        public async Task RestoreMany(IEnumerable<Guid> entityIds)
        {
            var entities = _productContext.Product.Where(i => entityIds.Contains(i.Id));

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    entity.LastSavedDate = DateTime.UtcNow;
                    entity.LastSavedBy = Guid.NewGuid();   //Заглушка
                    entity.IsDeleted = false;
                }
                _productContext.Product.UpdateRange(entities);
                await _productContext.SaveChangesAsync();
            }
        }
    }
}