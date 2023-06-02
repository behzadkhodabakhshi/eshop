using MyMarket.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Domain.Entity.Product
{
    public class Product:BaseEntity
    {
        
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public bool InstantOffer { get; set; }
        public long ViewCount { get; set; }

        public virtual Category Category { set; get; }

        public long CategoryId { set; get; }
        public virtual ICollection<ProductFeature> productFeatures { set; get; }

        public virtual ICollection<ProductImage> ProductImages { set; get; }
    }
}
