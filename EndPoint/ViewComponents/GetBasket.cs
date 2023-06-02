using EndPoint.Utilities;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.ViewComponents
{
    public class GetBasket : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly CookiesManeger cookiesManeger;
        public GetBasket(ICartService cartService)
        {
            _cartService = cartService;
            cookiesManeger = new CookiesManeger();
        }

        public IViewComponentResult Invoke()
        {
            var userid = ClaimUtilities.GetUserId(HttpContext.User);

            var Cart = _cartService.GetCartItem(cookiesManeger.GetBrowserId(HttpContext), userid);

            return View(viewName: "GetBasket", Cart.Data);


            //if (Cart != null)
            //{
            //    return View(viewName: "GetBasket", Cart.Data);
            //}
            //else
            //{
            //    return View(viewName: "NullBasket");
               
            //}

        }
    }
}
     

 
