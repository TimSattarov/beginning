using AutoMapper;
using PriceService.Models;
using PriceService.Repositories;

namespace PriceService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Price, PriceModel>().ReverseMap();
        }  
    }
}