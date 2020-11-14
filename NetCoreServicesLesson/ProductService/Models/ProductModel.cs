using System;
using System.Collections.Generic;

namespace ProductService.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }        
        public string Category { get; set; }


        public IEnumerable<ImageModel> Images { get; set; }
        public IEnumerable<PriceModel> Prices { get; set; }
    }
}