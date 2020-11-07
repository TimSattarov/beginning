using repository;

namespace PriceService.Repositories
{
    public class PriceDbModel : BaseEntity
    {
        public float CurrentPrice { get; set; }
        public float SalePrice { get; set; }
        public float RRPrice { get; set; }

         public int ProductId { get; set; }
    }
}