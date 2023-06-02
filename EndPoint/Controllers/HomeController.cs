using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EndPoint.Models;
using EndPoint.Models.ViewModels.HomePage;
using MyMarket.Application.Services.HomePage.Queries.GetSlider;
using MyMarket.Application.Services.HomePage.Queries.GetHomeImage;
using MyMarket.Application.Interfaces.FacadPattens;
using MyMarket.Application.Services.Products.Queries.GetAllProductForSite;
using MyMarket.Application.Services.Products.Queries.GetDiscountProduct;
using MyMarket.Application.Services.Products.Queries.GetInstantOffer;

namespace EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetSlidersService _getSlidersService;
        private readonly IGetHomeImageService _getHomeImageService;
        private readonly IProductFacad _productFacad;
        private readonly IGetDiscountProduct _getDiscountProduct;
        private readonly IGetInstantOffer _getinstantoffer;


        public HomeController(ILogger<HomeController> logger, IGetSlidersService getSlidersService,IGetHomeImageService getHomeImageService, IProductFacad productfacad, IGetDiscountProduct GetDiscountProduct, IGetInstantOffer GetInstantOffer)
        {
            _logger = logger;
            _getSlidersService = getSlidersService;
            _getHomeImageService = getHomeImageService;
            _productFacad = productfacad;
            _getDiscountProduct = GetDiscountProduct;
            _getinstantoffer = GetInstantOffer;
        }

        public IActionResult Index()
        {
            long categoryid = 2;
            int Page = 1;
            int PageSize = 6;

            HomePageViewModel homepage = new HomePageViewModel()
            {

                Sliders = _getSlidersService.Execute().Data,
                HomeImages = _getHomeImageService.Execute().Data,
                CarProducts = _productFacad.GetAllProductForSiteService.Execute(null, null, categoryid, Page, PageSize, Ordering.theNewest),
                DiscountProduct = _getDiscountProduct.Execute(),
                InstantOffer = _getinstantoffer.Execute(),
            };
            return View(homepage);
        }

        [HttpGet]
        public IActionResult AddSlider()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
