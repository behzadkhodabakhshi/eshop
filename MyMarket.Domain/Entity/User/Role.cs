using System.Collections.Generic;

namespace MyMarket.Domain.Entity.User
{
    public class Role
        {
            public long Id { get; set; }

            public string Name { get; set; }

        public ICollection<UserInRole> UserInRoles { get; set; }

        }

    

}
