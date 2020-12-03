using System;
using PriceService.Repositories;

namespace PriceService.Models
{
    public class PriceModel
    {
        public Guid Id { get; set; }
        public float CurrentPrice { get; set; }
        public float SalePrice { get; set; }
        public float RRPrice { get; set; }
        public bool IsLast { get; set; }

         public Guid ProductId { get; set; } 
    }
}