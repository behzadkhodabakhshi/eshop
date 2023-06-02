using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MyMarket.Application.Interfaces.FacadPattens;
using MyMarket.Application.Services.Common.GetMenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly IProductFacad _productFacad;
        public GetMenu(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        public IViewComponentResult Invoke()
        {
            var MenuItem = _productFacad.GetGetMenuItem.Execute();
            return View(viewName: "GetMenu", MenuItem.Data);
        }
    }
}
