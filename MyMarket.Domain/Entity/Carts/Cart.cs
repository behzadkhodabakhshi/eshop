using MyMarket.Domain.Entity.Commons;
using MyMarket.Domain.Entity.Product;
using MyMarket.Domain.Entity.User;

using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Domain.Entity.Carts
{
    public  class Cart:BaseEntity
    {
        public Guid BrowserId { get; set; }

        public bool Finished { get; set; }

        public virtual User.User User { get; set; }
        public long? UserId { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
       

    }
}
