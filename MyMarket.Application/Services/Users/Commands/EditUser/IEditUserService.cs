using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Users.Commands.EditUser
{
    public interface IEditUserService
    {
        public ResultDto Execute(EditUserRequestDto request);
    }
}
