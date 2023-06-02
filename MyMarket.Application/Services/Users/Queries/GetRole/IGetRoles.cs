using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MyMarket.Application.Services.Users.Queries.GetRole
{
    public interface IGetRoles
    {
       public ResultDto<List<RolesDto>> Execute();
    }
}
