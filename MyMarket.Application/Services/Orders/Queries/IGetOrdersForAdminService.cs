using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Orders.Queries
{
    public interface IGetOrdersForAdminService
    {
        ResultDto<List<GetOrdersForAdminServiceDto>> Execute(OrderState orderstate);
     }

    public class GetOrdersForAdminService : IGetOrdersForAdminService
    {
        private readonly IMyDBContetx _context;
        public GetOrdersForAdminService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<List<GetOrdersForAdminServiceDto>> Execute(OrderState orderstate)
        {
            var orders = _context.Orders.Where(p => p.OrderState == orderstate).ToList().Select(p => new GetOrdersForAdminServiceDto
            {
                Id = p.Id,
                PayRequestId = p.PayRequestId,
                UserId = p.UserId,
                OrderState = p.OrderState,
                InsertTime=p.InsertTime,

            }).ToList();

            return new ResultDto<List<GetOrdersForAdminServiceDto>>()
            {
                Data=orders,
                IsSuccess=true,
            };
        }
    }

    public class GetOrdersForAdminServiceDto
    {
        public long Id { get; set; }
        public long PayRequestId { get; set; }
        public long UserId { get; set; }
        public OrderState OrderState { get; set; }

        public DateTime InsertTime { get; set; }
    }
}
