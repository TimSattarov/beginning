using Repository;

namespace ProductService.Entity
{
    public class ProductDbModel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
