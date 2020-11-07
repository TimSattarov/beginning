using ImageService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService
{
    public interface IImageServiceTest
    {
        Task<IEnumerable<ImageEntity>> GetAll();
    }
}
