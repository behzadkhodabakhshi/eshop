using MyMarket.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Domain.Entity.HomePage
{
   public class HomeImage:BaseEntity
    {
        public string Src { get; set; }

        public string Link { get; set; }

        public ImageLocation ImageLocation { get; set; }

        public long ViewCount { set; get; }
    }

    public enum ImageLocation
    {
        L1 = 0,
        L2 = 1,
        R1 = 3,
        CenterFullScreen = 4,
        G1 = 5,
        G2 = 6,
    }
}
