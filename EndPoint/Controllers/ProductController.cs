using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Interfaces.FacadPattens;
using MyMarket.Application.Services.Products.Queries.GetAllProductForSite;

namespace EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductFacad _productfacad;
        public ProductController(IProductFacad productfacad)
        {
            _productfacad = productfacad;
        }
        public IActionResult Index(Ordering ordering, long? SearchCatId, string? SearchKey, long? categoryid, int Page=1, int PageSize=6)

        {


            ViewBag.CategoryId = categoryid;
            return View(_productfacad.GetAllProductForSiteService.Execute(SearchCatId,SearchKey,categoryid,Page,PageSize,ordering));
        }

        public IActionResult Detail(long Id)
        {
            return View(_productfacad.GetProductDetailForSiteService.Execute(Id).Data);
        }
    }
}
