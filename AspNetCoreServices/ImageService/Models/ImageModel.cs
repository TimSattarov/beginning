using ImageService.Entities;
using System;

namespace ImageService.Models
{
    public class ImageModel : Image
    {
        public ImageModel() : base()
        {
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Url { get; set; }
    }
}