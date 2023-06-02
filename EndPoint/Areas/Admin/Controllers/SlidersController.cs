using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.HomePage.Commands.AddSlider;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private IAddSliderService _addsliderservice;
        public SlidersController(IAddSliderService AddSliderService)
        {
            _addsliderservice = AddSliderService;
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

        public IActionResult Add(string link, IFormFile file)
        {
            AddSliderImageDto slider = new AddSliderImageDto()
            {
                Link = link,
                Image = file

            };

            _addsliderservice.Execute(slider);
            return View();
        }
    }
}
