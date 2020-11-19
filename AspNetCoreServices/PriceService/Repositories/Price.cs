using System;
using Repository;

namespace PriceService.Repositories
{
    public class Price : BaseEntity 
    {
        public Price() : base()
        {
        }
        
        public float CurrentPrice { get; set; }
        public float SalePrice { get; set; }
        public float RRPrice { get; set; }
        public bool IsLast { get; set; }

        public Guid ProductId { get; set; }
    }
}