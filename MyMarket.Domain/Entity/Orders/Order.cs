using MyMarket.Domain.Entity.Commons;
using MyMarket.Domain.Entity.Finances;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Domain.Entity.Orders
{
    public class Order:BaseEntity
    {
        public virtual User.User User { get; set; }

        public long UserId { get; set; }

        public virtual PayRequest PayRequests { get; set; }

        public long PayRequestId { get; set; }

        public OrderState OrderState { get; set; }

        public string Adress { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
