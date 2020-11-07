using ImageService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ImageService.Services
{
    public class ImageServiceTest : IImageServiceTest
    {
        private readonly ImageContext _imageContext;
        public ImageServiceTest(ImageContext imageContet)
        {
            _imageContext = imageContet;
        }
        
        public async Task<IEnumerable<ImageEntity>> GetAll()
        {
            return await _imageContext.Images.ToListAsync();
        }
    }
}