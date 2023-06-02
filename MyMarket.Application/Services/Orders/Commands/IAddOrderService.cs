using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Orders.Commands
{
    public interface IAddOrderService
    {
        ResultDto Execute(RequestIAddOrderServiceDto request);

    }

    public class AddOrderService : IAddOrderService
    {
        private readonly IMyDBContetx _context;

        public AddOrderService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestIAddOrderServiceDto request)
        {
            //var payrequest = _context.PayRequests.Where(p => p.Guid == guid).FirstOrDefault();
            //var userid = payrequest.UserId;


            // chon include nashode intori nemishe agar intori bekhahim baya mesle zir nevesht yani include user ra ezafe konim
            var payrequest = _context.PayRequests.Find(request.PayRequestId);

    
            var user = _context.Users.Find(request.UserId);

            var cart = _context.Carts
                .Include(p => p.CartItems)
                .ThenInclude(p => p.Product)
                .Where(p=>p.Id==request.CartId).FirstOrDefault();

            payrequest.IsPay = true;
            payrequest.PayDate = DateTime.Now;

            cart.Finished = true;

            Order order = new Order()
            {
                User = user,
                PayRequests = payrequest,
                OrderState = OrderState.Processing,
                Adress = "tehran niro havaie"
            };
            _context.Orders.Add(order);

            //////////////////////////////////////////////////////////////////////////////
            //foreach (var item in cart.CartItems)
            //{
            //    var product = _context.products.Find(item.Product.Id);

            //    OrderDetail orderdetail = new OrderDetail()
            //    {
            //        Orders = order,
            //        Products = product,
            //        //Products =item.Product,
            //        Price = product.Price,
            //        Count = item.Count,

            //    };

            //    _context.OrderDetails.Add(orderdetail);

            //}
            //_context.SaveChanges();

            //////////////////////////////////////////////////////////////////////////////
            List<OrderDetail> orderdetails = new List<OrderDetail>();
            foreach (var item in cart.CartItems)
            {

                OrderDetail orderdetail = new OrderDetail()
                {
                    Orders = order,
                    Products = item.Product,
                    Price = item.Product.Price,
                    Count = item.Count,
                    Discount=item.Discount,
                };

                item.Product.Inventory -= item.Count;

                orderdetails.Add(orderdetail);
            }

            _context.OrderDetails.AddRange(orderdetails);
            _context.SaveChanges();
            //////////////////////////////////////////////////////////////////////////////////


            return new ResultDto()
            {
                IsSuccess = true
            };

        }
    }


    public class RequestIAddOrderServiceDto
    {
        public long UserId { get; set; }

        public long PayRequestId { get; set; }

        public long CartId { get; set; }
    }


}
