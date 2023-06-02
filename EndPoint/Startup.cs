using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMarket.Application.Interfaces;
using MyMarket.Application.Interfaces.FacadPattens;
using MyMarket.Application.Services.carts;
using MyMarket.Application.Services.Finances.Commands.AddPayRequest;
using MyMarket.Application.Services.Finances.Queries.GetPayForAdmin;
using MyMarket.Application.Services.Finances.Queries.GetPayRequest;
using MyMarket.Application.Services.HomePage.Commands.AddHomeImage;
using MyMarket.Application.Services.HomePage.Commands.AddSlider;
using MyMarket.Application.Services.HomePage.Queries.GetHomeImage;
using MyMarket.Application.Services.HomePage.Queries.GetSlider;
using MyMarket.Application.Services.Orders.Commands;
using MyMarket.Application.Services.Orders.Queries;
using MyMarket.Application.Services.Products.Commands.EditProduct;
using MyMarket.Application.Services.Products.FacadPatterns;
using MyMarket.Application.Services.Products.Queries.GetDiscountProduct;
using MyMarket.Application.Services.Products.Queries.GetInstantOffer;
using MyMarket.Application.Services.Users.Commands;
using MyMarket.Application.Services.Users.Commands.Changetatus;
using MyMarket.Application.Services.Users.Commands.EditUser;
using MyMarket.Application.Services.Users.Commands.RemoveUser;
using MyMarket.Application.Services.Users.Commands.UserLogin;
using MyMarket.Application.Services.Users.Queries.GetRole;
using MyMarket.Application.Services.Users.Queries.GetUsers;
using MyMarket.Common.Role;
using MyMarket.Persistence.Context;


namespace EndPoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRole.Admin, policy => policy.RequireRole(UserRole.Admin));
                options.AddPolicy(UserRole.Customer, policy => policy.RequireRole(UserRole.Customer));
                options.AddPolicy(UserRole.Operator, policy => policy.RequireRole(UserRole.Operator));

            });


            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Authentication/Signin");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });

            services.AddScoped<IMyDBContetx, MyDBContext>();
            services.AddScoped<IGetUserService, GetUserService>();
            services.AddScoped<IGetRoles, GetRoles>();
            services.AddScoped<IRegisterUser, RegisterUser>();
            services.AddScoped<IRemoveUser, RemoveUser>();
            services.AddScoped<IChangeStatusUser, ChangeStatusUser>();
            services.AddScoped<IEditUserService, EditUserService>();
            services.AddScoped<IUserLoginService, UserLogInService>();
            services.AddScoped<IAddSliderService, AddSliderService>();
            services.AddScoped<IGetSlidersService, GetSlidersService>();
            services.AddScoped<IAddHomeImageService, AddHomeImageService>();
            services.AddScoped<IGetHomeImageService, GetHomeImageService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IAddPayRequest, AddPayRequest>();
            services.AddScoped<IGetRequestPay, GetRequestPay>();
            services.AddScoped<IAddOrderService, AddOrderService>();
            services.AddScoped<IGetUserOrsersService, GetUserOrsersService>();
            services.AddScoped<IGetOrdersForAdminService, GetOrdersForAdminService>();
            services.AddScoped<IGetPayRequestForAdminService, GetPayRequestForAdminService>();
            services.AddScoped<IEditProductService, EditProductService>();
            services.AddScoped<IGetDiscountProduct, GetDiscountProduct>();
            services.AddScoped<IGetInstantOffer, GetInstantOffer>();
            

            services.AddScoped<IProductFacad, ProductFacad>();


            services.AddDbContext<MyDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapControllerRoute(
                     name: "areas",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                   );
            });
        }
    }
}
