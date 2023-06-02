using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common;
using System.Collections.Generic;
using System.Linq;


namespace MyMarket.Application.Services.Users.Queries.GetUsers
{
    public class GetUserService : IGetUserService
    {

        private readonly IMyDBContetx _context;
        public GetUserService(IMyDBContetx context)
        {
            _context = context;
        }
        public GetListUserDto Execute(RequestGetUserDto request)
        {
            var users_query = _context.Users
                .Include(p => p.UserInRoles)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SerachKey))
            {
                users_query = users_query.Where(p => p.Name.Contains(request.SerachKey) || p.Email.Contains(request.SerachKey));
            }
            var rowscount = 0;

            var user = users_query.ToPaged(request.Page, request.PageSize, out rowscount);

            return new GetListUserDto
            {
                Page = request.Page,
                RowCount = rowscount,
                PageSize = request.PageSize,
                Users = user.Select(p => new GetUserDto
                {
                    AdminCheked = p.UserInRoles.Any(p => p.RoleId == 1),
                    UserCheked = p.UserInRoles.Any(p => p.RoleId == 2),
                    CustomerCheked = p.UserInRoles.Any(p => p.RoleId == 3),
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    IsActive = p.IsActive,

                }).ToList(),
            };
        }
    }
}




