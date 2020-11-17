using AutoMapper;
using ProductService.Entity;
using ProductService.Models;

namespace ImageService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ProductDbModel, ProductModel>().ReverseMap();
        }
    }
}
