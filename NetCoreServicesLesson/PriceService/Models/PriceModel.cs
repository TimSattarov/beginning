namespace PriceService.Models
{
    public class PriceModel
    {
        public int Id { get; set; }
        public float CurrentPrice { get; set; }
        public float SalePrice { get; set; }
        public float RRPrice { get; set; }

         public int ProductId { get; set; }
        // public ProductModel Product { get; set; }
    }
}