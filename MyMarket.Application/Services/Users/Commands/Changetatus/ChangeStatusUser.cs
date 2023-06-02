using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Users.Commands.Changetatus
{

    public class ChangeStatusUser : IChangeStatusUser
    {
        private readonly IMyDBContetx _context;
        public ChangeStatusUser(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto Execute(long UserId)
        {
            try
            {
                var user = _context.Users.Find(UserId);
                if (user!=null)
                {
                    user.IsActive= !user.IsActive;
                    _context.SaveChanges();
                    string auserstate = user.IsActive == true ? "فعالسازی" : "غیر فعالسازی";
                    return (new ResultDto
                    {
                        IsSucsess = true,
                        Message = $"عملیات {auserstate} کاربر با موفقیت انجام شد" ,
                    });

                }
                else
                {
                    return (new ResultDto
                    {
                        IsSucsess = false,
                        Message = "کاربر یافت نشد"
                    });
                }

            }
            catch (Exception)
            {

                return new ResultDto
                {
                    IsSucsess=false,
                    Message="عملیات تغییر وضعیت کاربر با خطا مواجه شد"
                };
            }
        }
    }
}
