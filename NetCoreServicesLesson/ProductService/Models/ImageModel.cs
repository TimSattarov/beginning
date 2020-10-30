namespace ProductService.Models
{
    public class ImageModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Path {get; set;}



        public int ProductId { get; set; }
        //public ProductModel Product { get; set; }
    }
}