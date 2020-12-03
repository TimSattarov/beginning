using System;

namespace ProductService.Models
{
    public class ImageModel
    {
        public Guid Id {get;set;}
        public string Url {get; set;}
        public string Path { get; set; }

        public Guid ProductId { get; set; }
    }
}