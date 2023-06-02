using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Users.Commands.UserLogin
{
    public interface IUserLoginService
    {
        public ResultDto<ResultUserloginDto> Execute(RequestUserLoginDto request);
    }
}

