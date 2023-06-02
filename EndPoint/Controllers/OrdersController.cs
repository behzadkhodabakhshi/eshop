using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndPoint.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.Orders.Queries;

namespace EndPoint.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGetUserOrsersService _getUserOrsersService;
        public OrdersController(IGetUserOrsersService getUserOrsersService)
        {
            _getUserOrsersService = getUserOrsersService;
        }
        public IActionResult Index()
        {
         
            long userid = ClaimUtilities.GetUserId(User).Value;
            return View(_getUserOrsersService.Execute(userid).Data);
        }
    }
}
