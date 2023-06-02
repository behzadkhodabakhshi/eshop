using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.Finances.Queries.GetPayForAdmin;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PayRequestsController : Controller
    {

        private readonly IGetPayRequestForAdminService _getPayRequestForAdminService;
        public PayRequestsController(IGetPayRequestForAdminService GetPayRequestForAdminService)
        {
            _getPayRequestForAdminService = GetPayRequestForAdminService;
        }
        public IActionResult Index(bool paystate)
        {
            return View(_getPayRequestForAdminService.Execute(paystate).Data);
        }
    }
}
