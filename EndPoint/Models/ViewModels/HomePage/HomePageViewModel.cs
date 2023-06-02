using MyMarket.Application.Services.Products.Queries.GetAllProductForSite;
using MyMarket.Application.Services.Products.Queries.GetDiscountProduct;
using MyMarket.Application.Services.Products.Queries.GetInstantOffer;
using MyMarket.Domain.Entity.HomePage;
using MyMarket.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Models.ViewModels.HomePage
{
    public class HomePageViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<HomeImage> HomeImages { get; set; }

        public GetAllProductForSiteServiceDto CarProducts { get; set; }

        public List<GetDiscountProductDto> DiscountProduct { get; set; }

        public List<GetInstantOfferDto> InstantOffer { get; set; }
    }
}
