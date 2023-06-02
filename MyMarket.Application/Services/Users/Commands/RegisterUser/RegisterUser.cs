using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.User;
using System;

namespace MyMarket.Application.Services.Users.Commands
{
    public class RegisterUser : IRegisterUser
    {

        private readonly IMyDBContetx _context;
        public RegisterUser(IMyDBContetx context)
        {
            _context = context;
        }

        public ResultDto<ResultRegisteUserDto> Execute(RequestRegisterUserDto request)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new ResultDto<ResultRegisteUserDto>()
                    {
                        Data = new ResultRegisteUserDto()
                        {
                            Id = 0,
                        },
                        IsSuccess = false,
                        Message = "نام را وارد کنید",
                    };
                }

                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto<ResultRegisteUserDto>()
                    {
                        Data = new ResultRegisteUserDto()
                        {
                            Id = 0,
                        },
                        IsSuccess = false,
                        Message = "پست الکترونیک را وارد کنید",
                    };
                }


                //if (request.PhoneNumber<1)
                //{
                //    return new ResultDto()
                //    {
                //        IsSucsess = false,
                //        Message = "شماره تلفن را وارد کنید",
                //    };
                //}

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultRegisteUserDto>()
                    {
                        Data = new ResultRegisteUserDto()
                        {
                            Id = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد کنید",
                    };
                }

                if (string.IsNullOrWhiteSpace(request.RePasword))
                {
                    return new ResultDto<ResultRegisteUserDto>()
                    {
                        Data = new ResultRegisteUserDto()
                        {
                            Id = 0,
                        },
                        IsSuccess = false,
                        Message = "تکرار رمز عبور را وارد کنید",
                    };
                }

                if (request.Password != request.RePasword)
                {
                    return new ResultDto<ResultRegisteUserDto>()
                    {
                        Data = new ResultRegisteUserDto()
                        {
                            Id = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور و تکرار رمز عبور یکسان نیست",
                    };
                }


                User user = new User()
                {

                    Email = request.Email,
                    Name = request.Name,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    IsActive = true,

                };

                //UserInRole userinrole = new UserInRole()
                //{

                //    UserId = user.Id,
                //    RoleId = request.RoleId,
                //    user = user,
                //    //Role=_context.Roles.Find(request.RoleId),
                //};
                //_context.UserInRoles.Add(userinrole);

                if (request.AdminCheked == true)
                {
                    UserInRole userinrole = new UserInRole()
                    {

                        UserId = user.Id,
                        RoleId = 1,
                        user = user,
                        //Role=_context.Roles.Find(request.RoleId),
                    };
                    _context.UserInRoles.Add(userinrole);
                }
                if (request.UserCheked == true)
                {
                    UserInRole userinrole = new UserInRole()
                    {

                        UserId = user.Id,
                        RoleId = 2,
                        user = user,
                        //Role=_context.Roles.Find(request.RoleId),
                    };
                    _context.UserInRoles.Add(userinrole);
                }
                if (request.CustomerCheked == true)
                {
                    UserInRole userinrole = new UserInRole()
                    {

                        UserId = user.Id,
                        RoleId = 3,
                        user = user,
                        //Role=_context.Roles.Find(request.RoleId),
                    };
                    _context.UserInRoles.Add(userinrole);
                }

                _context.Users.Add(user);





                _context.SaveChanges();

                return new ResultDto<ResultRegisteUserDto>()
                {
                    Data = new ResultRegisteUserDto()
                    {
                        Id = user.Id,
                    },
                    IsSuccess = true,
                    Message = "عملیات ثبت با موفقیت انجام شد",
                };
            }
            catch (Exception)
            {

                return new ResultDto<ResultRegisteUserDto>()
                {
                    Data = new ResultRegisteUserDto()
                    {
                        Id = 0,
                    },
                    IsSuccess = false,
                    Message = "عملیات ثبت انجام نشد",
                };
            }
        }
    }

      
    
   

}
