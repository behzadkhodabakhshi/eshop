using MyMarket.Common.Dto;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Users.Commands
{
    public interface IRegisterUser
    {
        public ResultDto<ResultRegisteUserDto> Execute(RequestRegisterUserDto request);
    }

}
