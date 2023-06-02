using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Utilities;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.carts;

namespace EndPoint.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly CookiesManeger cookiesManeger;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            cookiesManeger = new CookiesManeger();
        }
        public IActionResult Index()
        {
           var userid= ClaimUtilities.GetUserId(User);
           var result = _cartService.GetCartItem(cookiesManeger.GetBrowserId(HttpContext), userid);
            if (result!=null)
            {
                return View(result.Data);
            }
            else
            {
                return View(result.Data);
            }
       
        }

        public IActionResult AddToCart(long productId)
        {
            var resultAdd = _cartService.AddItemToCart(productId, cookiesManeger.GetBrowserId(HttpContext));

            return RedirectToAction("Index");
        }

        public IActionResult Remove(long CartItemId)
        {
            _cartService.RemoveItemOfCart(CartItemId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult SubCount(long CartItemId)
        {
            _cartService.Subcount(CartItemId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
            //return View();
        }

        public IActionResult AddCount(long CartItemId)
        {
            _cartService.AddCount(CartItemId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
            //return View();
        }
        
    }
}
