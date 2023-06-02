using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.Orders.Queries;
using MyMarket.Domain.Entity.Orders;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IGetOrdersForAdminService _getorderforadminservice;
        public OrderController(IGetOrdersForAdminService GetOrdersForAdminService)
        {
            _getorderforadminservice = GetOrdersForAdminService;
        }
        public IActionResult Index(OrderState orderstaet)
        {
            return View(_getorderforadminservice.Execute(orderstaet).Data);
        }
    }
}
