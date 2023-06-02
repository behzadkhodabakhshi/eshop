using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Interfaces.FacadPattens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.ViewComponents
{
    public class GetSearchCategories:ViewComponent
    {
        private readonly IProductFacad _productFacad;
        public GetSearchCategories(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        public IViewComponentResult Invoke()
        {
            var searchcategories = _productFacad.GetSearchCategoryService.Execute();
            return View(viewName: "GetSearchCategories", searchcategories);
        }

    }
}
