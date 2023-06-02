using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MyMarket.Application.Services.Users.Commands.UserLogin
{
    public class UserLogInService : IUserLoginService
    {
        private readonly IMyDBContetx _context;
        public UserLogInService(IMyDBContetx context)
        {
            _context = context;
        }


        public ResultDto<ResultUserloginDto> Execute(RequestUserLoginDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto
                    {

                    },
                    IsSuccess = false,
                    Message = "پست الکترونیک را وارد کنید",
                };
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto
                    {

                    },
                    IsSuccess = false,
                    Message = "رمز عبور را وارد کنید",
                };
            }

            var user = _context.Users
               .Include(p=>p.UserInRoles)
               .ThenInclude(p=>p.Role)
                .Where(p => p.Email == request.Email && p.Password == request.Password).FirstOrDefault();
            //var userin = _context.UserInRoles.Where(p => p.UserId ==43);

            if (user != null)
            {
                if (user.IsActive!=true)
                {
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto
                        {

                        },
                        IsSuccess = false,
                        Message = "کاربر غیر فعال می باشد",
                    };
                }
                //var roles = "";
                //foreach (var item in user.UserInRoles)
                //{
                //    roles += $"{item.Role.Name}";

                //}

                List<string> roles = new List<string>();
                foreach (var item in user.UserInRoles)
                {
                    roles.Add(item.Role.Name);
                }

                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto
                    {
                        Name = user.Name,
                        UserId = user.Id,
                        //Role = "Customer",
                        Role = roles,


                    },
                    IsSuccess = true,
                    Message = "ورود با موفقیت انجام شد",
                };
            }
            else
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto
                    {

                    },
                    IsSuccess = false,
                    Message = "ایمیل یا رمز عبور اشتباه میباشد",
                };
            }

        }
    }
}

