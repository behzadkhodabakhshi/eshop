using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;

namespace MyMarket.Application.Services.Users.Commands.RemoveUser
{
    public class RemoveUser : IRemoveUser
    {
        private readonly IMyDBContetx _context;
        public RemoveUser(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestRemoveUser request)
        {
            try
            {
                var user = _context.Users.Find(request.Id);
                if (user != null)
                {
                    user.IsRemoved = true;
                    user.RemoveTime = DateTime.Now;
                    _context.SaveChanges();
                    return new ResultDto
                    {
                        IsSucsess = true,
                        Message = "عملیات حذف کاربر با موفقیت انجام شد"
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSucsess = false,
                        Message = "کاربر یافت نشد"
                    };
                }


            }
            catch (Exception)
            {


                return new ResultDto
                {
                    IsSucsess = false,
                    Message = "عملیات حذف کاربر با خطا مواجه شد"
                };
            }
        }
    }
}
