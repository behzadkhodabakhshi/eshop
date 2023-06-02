using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMarket.Application.Services.Users.Commands;
using MyMarket.Application.Services.Users.Commands.Changetatus;
using MyMarket.Application.Services.Users.Commands.EditUser;
using MyMarket.Application.Services.Users.Commands.RemoveUser;
using MyMarket.Application.Services.Users.Queries.GetRole;
using MyMarket.Application.Services.Users.Queries.GetUsers;
using MyMarket.Common.Dto;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUserService _getuserservice;
        private readonly IGetRoles _getrole;
        private readonly IRegisterUser _registerUser;
        private readonly IRemoveUser _RemoveUser;
        private readonly IChangeStatusUser _changeStatusUser;
        private readonly IEditUserService _edituserservice;
        public UsersController(IGetUserService getUserService,IGetRoles GetRole,IRegisterUser registerUser, IRemoveUser removeUser, IChangeStatusUser changeStatusUser,IEditUserService edituserservice)
        {
            _getuserservice = getUserService;
            _getrole = GetRole;
            _registerUser = registerUser;
            _RemoveUser = removeUser;
            _changeStatusUser = changeStatusUser;
            _edituserservice = edituserservice;
        }
        [HttpGet]
        public IActionResult Index(string searchkey, int page = 1,int pagesize=3)
        {
            return View(_getuserservice.Execute(new RequestGetUserDto
            {
                Page = page,
                SerachKey = searchkey,
                PageSize=pagesize,

            }
                ));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles= new SelectList(_getrole.Execute().Data, "ID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(bool AdminCheked,bool CustomerCheked,bool UserCheked, string Email, string Name, long RoleId,long PhoneNumber, string Password, string RePassword)
        {
            var result = _registerUser.Execute(new RequestRegisterUserDto
            {
                Email = Email,
                Name = Name,
                PhoneNumber=PhoneNumber,
                RoleId=RoleId,
                Password = Password,
                RePasword = RePassword,
                AdminCheked=AdminCheked,
                CustomerCheked=CustomerCheked,
                UserCheked=UserCheked,
            });
            return Json(result);
        }

        public IActionResult Remove(long userId)
        {
           
                var result = _RemoveUser.Execute(new RequestRemoveUser
                {
                    Id = userId,

                });
                return Json(result);       
          
        }

        public IActionResult UserSatusChange(long userid)
        {
            //var result = _changeStatusUser.Execute(userid);
            //return Json(result);

            return Json(_changeStatusUser.Execute(userid));

        }

        public IActionResult Edit(bool adminchecked,bool customerchecked, bool userchecked, long id,string name,string email)
        {

            return Json (_edituserservice.Execute(new EditUserRequestDto
            {
            Id=id,
            Name=name,
            Email=email,
            adminchecked=adminchecked,
            customerchecked=customerchecked,
            userchecked=userchecked,
            }));

        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

    }
}
