using Repository;

namespace ProductService.Entity
{
    public class ProductDbModel : BaseEntity
    {
        public ProductDbModel() : base()
        {
        }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
