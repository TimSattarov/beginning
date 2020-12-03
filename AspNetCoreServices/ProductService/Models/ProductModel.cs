using System;
using System.Collections.Generic;

namespace ProductService.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        

        public IEnumerable<ImageModel> Images { get; set; }
        public IEnumerable<PriceModel> Prices { get; set; }
    }
}