using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Interfaces.FacadPattens;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class CategoryController : Controller
    {
        private readonly IProductFacad _productFacad;
        public CategoryController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(long? parentid)
        {
            return View(_productFacad.GetAllCategoriesService.Execute(parentid).Data);
        }

        [HttpGet]
        public IActionResult Create(long? parentid)
        {
            ViewBag.parentid = parentid;
            return View();
        }

        [HttpPost]
        public IActionResult Create(long? parentid,string name)
        {
            var result = _productFacad.CreateNewCategoryService.Execute(parentid, name);
            return Json(result);
        }
    }
}
