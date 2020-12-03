using ImageService.Entities;
using System;

namespace ImageService.Models
{
    public class ImageModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
    }
}