using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Models.ViewModels.HomePage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMarket.Application.Interfaces.FacadPattens;
using MyMarket.Application.Services.HomePage.Queries.GetHomeImage;
using MyMarket.Application.Services.HomePage.Queries.GetSlider;
using MyMarket.Application.Services.Products.Queries.GetAllProductForSite;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly ILogger<AdminHomeController> _logger;
        private readonly IGetSlidersService _getSlidersService;
        private readonly IGetHomeImageService _getHomeImageService;
        private readonly IProductFacad _productFacad;
        public AdminHomeController(ILogger<AdminHomeController> logger, IGetSlidersService getSlidersService, IGetHomeImageService getHomeImageService, IProductFacad productfacad)
        {
            _logger = logger;
            _getSlidersService = getSlidersService;
            _getHomeImageService = getHomeImageService;
            _productFacad = productfacad;


        }

        public IActionResult Index()
        {
            long categoryid = 2;
            int Page = 1;
            int PageSize = 3;

            HomePageViewModel homepage = new HomePageViewModel()
            {

                Sliders = _getSlidersService.Execute().Data,
                HomeImages = _getHomeImageService.Execute().Data,
                CarProducts = _productFacad.GetAllProductForSiteService.Execute(null, null, categoryid, Page, PageSize, Ordering.theNewest),

            };
            return View(homepage);
        }

        
    }
}
