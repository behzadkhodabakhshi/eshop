using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMarket.Application.Interfaces.FacadPattens;
using MyMarket.Application.Services.Products.Commands.CreateNewProduct;
using MyMarket.Application.Services.Products.Commands.EditProduct;
using MyMarket.Domain.Entity.Product;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductFacad _productfacad;
        public ProductController(IProductFacad productfacad)
        {
            _productfacad = productfacad;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_productfacad.GetFullCategory.Execute().Data, "Id","Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto request,List<ProductFeaturesDto> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0;i<Request.Form.Files.Count; i++)
            {
                //var file = Request.Form.Files[i];
                images.Add(Request.Form.Files[i]);
            }

            request.Images = images;
            request.productFeatures = features;

            return Json(_productfacad.CreateNewProductService.Execute(request));


        }

       

        [HttpGet]

        public IActionResult Edit(long id)
        {
            ViewBag.Categories = new SelectList(_productfacad.GetFullCategory.Execute().Data, "Id", "Name");
            return View(_productfacad.EditProductService.Execute(id).Data);
        }
        [HttpPost]
        public IActionResult Edit(EditProductDto request, List<ProductFeaturesDto> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                //var file = Request.Form.Files[i];
                images.Add(Request.Form.Files[i]);
            }

            request.Images = images;
            request.productFeatures = features;
            return Json(_productfacad.PEditProductService.Execute(request));
        }


        [HttpGet]
        public IActionResult RemoveFeature(long id)
        {
            return Json(_productfacad.RemoveProductFeatureService.Execute(id));
           
        }

        [HttpGet]
        public IActionResult RemoveImage(long id)
        {
            return Json(_productfacad.RemoveProductImageService.Execute(id));

        }
    }
}
