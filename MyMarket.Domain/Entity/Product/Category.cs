using MyMarket.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Domain.Entity.Product
{
  public  class Category:BaseEntity
    {
        public string Name { get; set; }

        public long? ParentId { get; set; }

        public virtual Category ParentCategory { set; get; }

        public virtual ICollection<Category> SubCategories { set; get; }


    }
}
