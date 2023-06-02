using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Role;
using MyMarket.Domain.Entity.Carts;
using MyMarket.Domain.Entity.Finances;
using MyMarket.Domain.Entity.HomePage;
using MyMarket.Domain.Entity.Orders;
using MyMarket.Domain.Entity.Product;
using MyMarket.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Persistence.Context
{
    public class MyDBContext: DbContext, IMyDBContetx
    {
        public MyDBContext(DbContextOptions options):base (options)
        {

        }

        public DbSet<User> Users { set; get; }

        public DbSet<Role> Roles { set; get; }

        public DbSet<UserInRole> UserInRoles { set; get; }

        public DbSet<Category> Categories { set; get; }

        public DbSet<Product> products { set; get; }

        public DbSet<ProductFeature> ProductFeatures { set; get; }

        public DbSet<ProductImage> GetProductImages { set; get; }

        public DbSet<Slider> Sliders { set; get; }

        public DbSet<HomeImage> HomeImages { set; get; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<PayRequest> PayRequests { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
      
        {
            //bareye error cycle zamane update-database
            modelBuilder.Entity<Order>()
                .HasOne(p => p.User)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                    .HasOne(p => p.PayRequests)
                    .WithMany(p => p.Orders)
                    .OnDelete(DeleteBehavior.NoAction);




            //ااضافه کردن دیتای پیش فرض به جدول رل

            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = UserRole.Admin });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = UserRole.Operator });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = UserRole.Customer });

            //ایندکس گذاری روی ایمیل و عدم قبول تکراری
            modelBuilder.Entity<User>().HasIndex(p=>p.Email).IsUnique();

            //فیلتر عدم نمایش حذف شده های جدول یوزر

            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomeImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<PayRequest>().HasQueryFilter(p => !p.IsRemoved);

            
        }
    }

    
}
