using Microsoft.EntityFrameworkCore;
using MyMarket.Domain.Entity.Carts;
using MyMarket.Domain.Entity.Finances;
using MyMarket.Domain.Entity.HomePage;
using MyMarket.Domain.Entity.Orders;
using MyMarket.Domain.Entity.Product;
using MyMarket.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyMarket.Application.Interfaces
{
    public interface IMyDBContetx
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Category> Categories { set; get; }

        DbSet<Product> products { set; get; }

        DbSet<ProductFeature> ProductFeatures { set; get; }

        DbSet<ProductImage> GetProductImages { set; get; }

        DbSet<Slider> Sliders { set; get; }

        DbSet<HomeImage> HomeImages { set; get; }

        DbSet<Cart> Carts { get; set; }

        DbSet<CartItem> CartItems { get; set; }

        DbSet<PayRequest> PayRequests { get; set; }

        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }


        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
