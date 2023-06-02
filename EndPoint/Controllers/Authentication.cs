using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MyMarket.Application.Services.Users.Commands;
using MyMarket.Application.Services.Users.Commands.UserLogin;
using MyMarket.Common.Role;

namespace EndPoint.Controllers
{
    public class Authentication : Controller
    {
        private readonly IUserLoginService _userloginservice;
        private readonly IRegisterUser _registerUser;
        public Authentication(IUserLoginService userLoginService,IRegisterUser registerUser)
        {
            _userloginservice = userLoginService;
            _registerUser = registerUser;
        }
        public IActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SignIn(RequestUserLoginDto request)
        {
            var result = _userloginservice.Execute(new RequestUserLoginDto
            {
                Email = request.Email,
                Password = request.Password,
            });
            if (result.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,result.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, result.Data.Name),
                //new Claim(ClaimTypes.Role, result.Data.Role),      
                    //jaygozin mishe ba foreach paien
            };
                foreach (var item in result.Data.Role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);
            }

            return Json(result);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(RequestRegisterUserDto request)
        {
           var result= _registerUser.Execute(new RequestRegisterUserDto
            {
                Name=request.Name,
                Email=request.Email,
                Password=request.Password,
                RePasword=request.RePasword,
                PhoneNumber=request.PhoneNumber,
                RoleId=3,
            });

            if (result.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,result.Data.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.Name),
                new Claim(ClaimTypes.Role, UserRole.Customer ),
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);
            }

            return Json(result);
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }


    }


}
