using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MyMarket.Application.Services.Users.Queries.GetRole
{
    public class GetRoles : IGetRoles
    {
        private readonly IMyDBContetx _context;
        public GetRoles(IMyDBContetx contetx)
        {
            _context = contetx;
        }
       

        public ResultDto<List<RolesDto>> Execute()
        {
            var roles = _context.Roles.ToList().Select(p => new RolesDto
            {
                ID = p.Id,
                Name = p.Name,
            }).ToList();

            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = "",
            };
        }
    }
}
