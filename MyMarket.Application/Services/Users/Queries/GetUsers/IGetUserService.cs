using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Text;


namespace MyMarket.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUserService
    {
        GetListUserDto Execute(RequestGetUserDto request);
    }
}
