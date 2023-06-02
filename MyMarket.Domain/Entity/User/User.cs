using MyMarket.Domain.Entity.Commons;
using MyMarket.Domain.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Domain.Entity.User
{


    public class User : BaseEntity
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public bool IsActive { get; set; }

        public long PhoneNumber { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }



}
