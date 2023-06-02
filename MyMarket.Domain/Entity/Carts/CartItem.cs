using MyMarket.Domain.Entity.Commons;

namespace MyMarket.Domain.Entity.Carts
{
    public class CartItem : BaseEntity
    {


        public int Price { get; set; }

        public int Discount { get; set; }
        public int Count { get; set; }

        public virtual Cart Cart { get; set; }

        public long CartId { get; set; }

        public virtual Product.Product Product { get; set; }

        public long ProductId { get; set; }





    }
}
