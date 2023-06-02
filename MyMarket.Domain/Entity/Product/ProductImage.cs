using MyMarket.Domain.Entity.Commons;

namespace MyMarket.Domain.Entity.Product
{
    public class ProductImage:BaseEntity
    {
        public string Src { get; set; }
        public virtual Product Product { set; get; }
        public long ProductId { set; get; }
    }
}
