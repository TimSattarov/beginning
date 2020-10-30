using System.Collections.Generic;
using System.Linq;
using ImageService.Models;

namespace ImageService.Services
{
    public class ImageService : IImageService
    {
        public IEnumerable<ImageModel> _images = new List<ImageModel>
        {
            new ImageModel() {Id = 0, Name = "0_1", Path = "", ProductId = 0},
            new ImageModel() {Id = 1, Name = "0_2", Path = "", ProductId = 0},
            new ImageModel() {Id = 2, Name = "0_3", Path = "", ProductId = 0},
            new ImageModel() {Id = 3, Name = "1_1", Path = "", ProductId = 1},
            new ImageModel() {Id = 4, Name = "1_2", Path = "", ProductId = 1},
            new ImageModel() {Id = 5, Name = "2_1", Path = "", ProductId = 2},
            new ImageModel() {Id = 6, Name = "3_1", Path = "", ProductId = 3}
        };

        public IEnumerable<ImageModel> GetAll() => _images;

        public ImageModel Get(int id) => _images.FirstOrDefault(i => i.Id == id);
    }
}