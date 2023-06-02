using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.User;
using System.Linq;

namespace MyMarket.Application.Services.Users.Commands.EditUser
{
    public class EditUserService : IEditUserService
    {
        private readonly IMyDBContetx _context;
        public EditUserService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto Execute(EditUserRequestDto request)
        {
            var user = _context.Users.Find(request.Id);

            if (user != null)
            {
                user.Name = request.Name;
                user.Email = request.Email;
                //admin change//////////////////////////
                if (request.adminchecked == true)
                {
                    if (_context.UserInRoles.Any(p => p.UserId == request.Id & p.RoleId == 1) == false)
                    {
                        UserInRole u_r = new UserInRole()
                        {
                            UserId = request.Id,
                            RoleId = 1
                        };
                        _context.UserInRoles.Add(u_r);
                    }
                }
                else
                {
                    if (_context.UserInRoles.Any(p => p.UserId == request.Id & p.RoleId == 1) == true)
                    {
                        UserInRole u_rr = _context.UserInRoles.Where(p => p.UserId == request.Id & p.RoleId == 1).FirstOrDefault(); ;
                        _context.UserInRoles.Remove(u_rr);
                    }
                }
                //customer change////////////////////////
                if (request.customerchecked == true)
                {
                    if (_context.UserInRoles.Any(p => p.UserId == request.Id & p.RoleId == 3) == false)
                    {
                        UserInRole u_r = new UserInRole()
                        {
                            UserId = request.Id,
                            RoleId = 3
                        };
                        _context.UserInRoles.Add(u_r);
                    }
                }
                else
                {
                    if (_context.UserInRoles.Any(p => p.UserId == request.Id & p.RoleId == 3) == true)
                    {
                        UserInRole u_rr = _context.UserInRoles.Where(p => p.UserId == request.Id & p.RoleId == 3).FirstOrDefault(); ;
                        _context.UserInRoles.Remove(u_rr);
                    }
                }
                //user change////////////////////////
                if (request.userchecked == true)
                {
                    if (_context.UserInRoles.Any(p => p.UserId == request.Id & p.RoleId == 2) == false)
                    {
                        UserInRole u_r = new UserInRole()
                        {
                            UserId = request.Id,
                            RoleId = 2
                        };
                        _context.UserInRoles.Add(u_r);
                    }
                }
                else
                {
                    if (_context.UserInRoles.Any(p => p.UserId == request.Id & p.RoleId == 2) == true)
                    {
                        UserInRole u_rr = _context.UserInRoles.Where(p => p.UserId == request.Id & p.RoleId == 2).FirstOrDefault(); ;
                        _context.UserInRoles.Remove(u_rr);
                    }
                }
                //////////////////////////
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSucsess = true,
                    Message = "ویرایش کاربر با موفقیت انجام شد",
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSucsess = false,
                    Message = "کاربر یافت نشد",
                };
            }
        }
    }
}
