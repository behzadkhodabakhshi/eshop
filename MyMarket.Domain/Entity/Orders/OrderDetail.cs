using MyMarket.Domain.Entity.Commons;

namespace MyMarket.Domain.Entity.Orders
{
    public class OrderDetail:BaseEntity
    {
        public virtual Order Orders { get; set; }

        public long OrderId { get; set; }

        public virtual Product.Product Products { get; set; }

        public long ProductId { get; set; }

        public long Price { get; set; }

        public long Discount { get; set; }

        public long Count { get; set; }



    }
}
