using AutoMapper;
using ImageService.Entities;
using ImageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Image, ImageModel>().ReverseMap();
        }
    }
}
