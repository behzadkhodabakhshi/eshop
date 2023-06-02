using MyMarket.Domain.Entity.Commons;
using MyMarket.Domain.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Domain.Entity.Finances
{
  public  class PayRequest:BaseEntity
    {
        public Guid Guid { get; set; }

        public long Amount { get; set; }

        public virtual User.User User { get; set; }

        public long UserId { get; set; }

        public virtual Carts.Cart Cart { get; set; }

        public long CartId { get; set; }
        public bool IsPay { get; set; }

        public DateTime? PayDate { get; set; }

        public string Authority { get; set; }

        public long RefId { get; set; } = 0;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
