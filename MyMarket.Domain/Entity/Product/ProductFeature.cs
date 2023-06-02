using MyMarket.Domain.Entity.Commons;

namespace MyMarket.Domain.Entity.Product
{
    public class ProductFeature:BaseEntity
    {

        public string DisplayName { get; set; }
        public string Value { get; set; }

        public virtual Product Product { set; get; }

        public long ProductId { set; get; }
    }
}
