using System.Collections.Generic;
using ImageService.Models;

namespace ImageService.Services
{
    public interface IImageService
    {
        IEnumerable<ImageModel> GetAll();
        ImageModel Get(int id);
    }
}