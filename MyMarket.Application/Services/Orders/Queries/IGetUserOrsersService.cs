using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Orders.Queries
{
  public  interface IGetUserOrsersService
    {
        ResultDto<List<GetUserOrdersServiceResultDto>> Execute(long userid);

    }

    public class GetUserOrsersService : IGetUserOrsersService
    {
        private readonly IMyDBContetx _context;
        public GetUserOrsersService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<List<GetUserOrdersServiceResultDto>> Execute(long userid)
        {
            var order = _context.Orders
                .Include(p => p.PayRequests)
                 .Include(p => p.OrderDetails)
                 .ThenInclude(p => p.Products)
                 .Where(p => p.UserId == userid).ToList().Select(p => new GetUserOrdersServiceResultDto
                 {
                     OrderId = p.Id,
                     OrderState = p.OrderState,
                     PayRequestId=p.PayRequestId,
                     OrderPriceSum=p.PayRequests.Amount,
                     
                     OrderDetails = p.OrderDetails.ToList().Select(p => new OrderDetailsDto()
                     {
                         ProductName = p.Products.Name,
                         Price = p.Price,
                         Count = p.Count,
                         PriceSum=p.Price*p.Count
                         
                     }).ToList(),
                     
                 }).ToList();

            return new ResultDto<List<GetUserOrdersServiceResultDto>>()
            {
                Data = order,
                IsSuccess = true,
                Message = "لیست سفارشات برگشت داده شد"

            };
        }
    }



    public class GetUserOrdersServiceResultDto
    {
        public long OrderId { get; set; }
        public long OrderPriceSum { get; set; }

        public long PayRequestId { get; set; }
        public OrderState OrderState { get; set; }



        public List<OrderDetailsDto> OrderDetails { get; set; }

    }

    public class OrderDetailsDto
    {
        public string ProductName { get; set; }

        public long Price { get; set; }

        public long Count { get; set; }

        public long PriceSum { get; set; }
    }
}
