using System;

namespace ProductService.Models
{
    public class PriceModel
    {
        public Guid Id { get; set; }
        public float CurrentPrice { get; set; }
        public float SalePrice { get; set; }
        public float RRPrice { get; set; }
        
        public Guid ProductId { get; set; }
    }
}