using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.HomePage.Commands.AddHomeImage;
using MyMarket.Domain.Entity.HomePage;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Operator")]
    public class HomeImagesController : Controller
    {
        private readonly IAddHomeImageService _addHomeImageService;
        public HomeImagesController(IAddHomeImageService addHomeImageService)
        {
            _addHomeImageService = addHomeImageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(AddHomeImageDto home)
        {
           

            _addHomeImageService.Execute(home);

            return View();
        }
    }
}
