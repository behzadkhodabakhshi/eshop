using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Finances.Queries.GetPayForAdmin
{
    public interface IGetPayRequestForAdminService
    {
        ResultDto<List<IGetPayRequestForAdminServiceDto>> Execute(bool PayState);
    }

    public class GetPayRequestForAdminService : IGetPayRequestForAdminService
    {
        private readonly IMyDBContetx _context;
        public GetPayRequestForAdminService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<List<IGetPayRequestForAdminServiceDto>> Execute(bool PayState)
        {
            var pay = _context.PayRequests
                 .Include(p => p.User)
                 .Where(p => p.IsPay == PayState).ToList().Select(p => new IGetPayRequestForAdminServiceDto
                 {
                     Id = p.Id,
                     Name = p.User.Name,
                     Amount = p.Amount,
                     PayDate = p.PayDate,
                     Authority = p.Authority,
                     RefId = p.RefId
                 }).ToList();

            return new ResultDto<List<IGetPayRequestForAdminServiceDto>>()
            {
                Data = pay,
                IsSuccess = true,
            };


        }
    }
    public class IGetPayRequestForAdminServiceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long Amount { get; set; }

        public DateTime? PayDate { get; set; }

        public string Authority { get; set; }

        public long RefId { get; set; } = 0;



    }


}
