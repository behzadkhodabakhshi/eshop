using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Finances;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Finances.Commands.AddPayRequest
{
    public interface IAddPayRequest
    {
        ResultDto<PayRequestResult> Execute(long userid,long cartid, long amount);
    }

    public class AddPayRequest : IAddPayRequest
    {
        private readonly IMyDBContetx _context;
        public AddPayRequest(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<PayRequestResult> Execute(long userid, long cartid, long amount)
        {
            var user = _context.Users.Find(userid);
            var cart = _context.Carts.Find(cartid);
            PayRequest payrequest = new PayRequest()
            {
                Guid = Guid.NewGuid(),
                Amount = amount,
                User = user,
                Cart = cart,
                IsPay = false,
                
            };

            _context.PayRequests.Add(payrequest);
            _context.SaveChanges();

            return new ResultDto<PayRequestResult>
            {
                Data = new PayRequestResult
                {
                    Guid = payrequest.Guid,
                    Id=payrequest.Id,
                   
                },
                IsSuccess = true,

        };


    }
}

public class PayRequestResult
{
    public Guid Guid { get; set; }
    public long Id { get; set; }
    }
}
