using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Payment;
using EndPoint.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMarket.Application.Services.carts;
using MyMarket.Application.Services.Finances.Commands.AddPayRequest;
using MyMarket.Application.Services.Finances.Queries.GetPayRequest;
using MyMarket.Application.Services.Orders.Commands;
using ZarinPal.Class;

namespace EndPoint.Controllers
{
    [Authorize("Operator")]
    public class PayController : Controller
    {
        private readonly IAddPayRequest _addpayrequest;
        private readonly ICartService _cartservice;
        private readonly IGetRequestPay _getrequestpay;
        private readonly IAddOrderService _addOrderService;
        private readonly CookiesManeger cookiesManeger;
        //zarin pal
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;


        public PayController(IAddPayRequest addPayRequest, IGetRequestPay getRequestPay, ICartService cartService, IAddOrderService AddOrderService)
        {
            _addpayrequest = addPayRequest;
            _cartservice = cartService;
            _getrequestpay = getRequestPay;
            _addOrderService = AddOrderService;
            cookiesManeger = new CookiesManeger();
            //zarin pal
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }
        public async Task<IActionResult> Index(long userid, long cartid, long PriceSum, string email, long mobile)
        {

            //var userid = ClaimUtilities.GetUserId(User);
            //var result = _cartservice.GetCartItem(cookiesManeger.GetBrowserId(HttpContext), userid);
            var requestpay = _addpayrequest.Execute(userid, cartid, PriceSum);

            //ارسال به درگاه پرداخت

            try
            {
                var result = await _payment.Request(new DtoRequest()
                {
                    //Mobile =Convert.ToString( mobile),
                    Mobile = Convert.ToString(mobile),
                    CallbackUrl = $"https://localhost:44362/Pay/Verify?guid={requestpay.Data.Guid}",
                    Description = "پرداخت فاکتور شماره:" + requestpay.Data.Id,
                    //Description = "پرداخت فاکتور شماره:",
                    Email = email,
                    //Email = "jghjhj@a.com",
                    Amount = Convert.ToInt32(PriceSum),
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");

            }
            catch (Exception e)
            {

                throw;
            }
   
            //return RedirectToAction("Index","Cart");
        }

        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            var requestpay = _getrequestpay.Execute(guid);
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = Convert.ToInt32(requestpay.Data.Pricesum),
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);
            if (verification.Status == 100)
            {

                long? userid = ClaimUtilities.GetUserId(User);
                _addOrderService.Execute(new RequestIAddOrderServiceDto()
                {
                    PayRequestId = requestpay.Data.Id,
                    UserId = userid.Value,
                    CartId = _cartservice.GetCartItem(cookiesManeger.GetBrowserId(HttpContext), userid).Data.CartId,
                });
            };


            return RedirectToAction("index", "home");
        }
    }
}




