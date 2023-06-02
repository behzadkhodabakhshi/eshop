using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Finances.Queries.GetPayRequest
{
    public interface IGetRequestPay
    {
        ResultDto<GetRequestPayeDto> Execute(Guid guid);
    }
    public class GetRequestPay : IGetRequestPay
    {
        private readonly IMyDBContetx _context;
        public GetRequestPay(IMyDBContetx context)
        {
            _context = context;
        }

        public ResultDto<GetRequestPayeDto> Execute(Guid guid)
        {
            var payrequest = _context.PayRequests.Where(p => p.Guid == guid).FirstOrDefault();
            return new ResultDto<GetRequestPayeDto>
            {
                Data = new GetRequestPayeDto
                {
                    Pricesum = payrequest.Amount,
                    Id=payrequest.Id,
                },
                IsSuccess = true,
            };
        }
    }
}
    


    public class GetRequestPayeDto
{
    public long Pricesum { get; set; }
    public long Id { get; set; }

}

