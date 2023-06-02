using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MyMarket.Application.Services.carts.CartService;

namespace MyMarket.Application.Services.carts
{
    public interface ICartService
    {
        ResultDto AddItemToCart(long ProductId, Guid BrowserId);

        ResultDto RemoveItemOfCart(long ProductId, Guid BrowserId);

        ResultDto<GetCartItemDto> GetCartItem(Guid BrowserId, long? UserId);

        ResultDto AddCount(long ProductId, Guid BrowserId);

        ResultDto Subcount(long ProductId, Guid BrowserId);
    }

    public class CartService : ICartService
    {
        private readonly IMyDBContetx _context;

        public CartService(IMyDBContetx context)
        {
            _context = context;
        }



        public ResultDto AddItemToCart(long ProductId, Guid BrowserId)
        {
            var product = _context.products.Find(ProductId);
            var cart = _context.Carts.Where(p => p.BrowserId == BrowserId && p.Finished == false).FirstOrDefault();
            if (cart != null)
            {
                var item = _context.CartItems.Where(p => p.ProductId == ProductId && p.CartId == cart.Id).FirstOrDefault();
                if (item != null)
                {
                    item.Count++;
                    _context.SaveChanges();
                }
                else
                {
                    CartItem caritem = new CartItem()
                    {
                        Cart = cart,
                        Price = product.Price,
                        Discount = product.Discount,
                        Count = 1,
                        Product = product,
                    };
                    _context.CartItems.Add(caritem);
                    _context.SaveChanges();
                }
            }
            else
            {

                Cart newcart = new Cart()
                {
                    Finished = false,
                    BrowserId = BrowserId,
                };

                CartItem caritem = new CartItem()
                {
                    Cart = newcart,
                    Price = product.Price,
                    Count = 1,
                    Product = product,
                };
                _context.Carts.Add(newcart);
                _context.CartItems.Add(caritem);
                _context.SaveChanges();
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"محصول  {product.Name} با موفقیت به سبد خرید شما اضافه شد "

            };
        }

        public ResultDto<GetCartItemDto> GetCartItem(Guid BrowserId, long? UserId)
        {

            try
            {

                var cart = _context.Carts
                                         .Include(p => p.CartItems)
                                         .ThenInclude(p => p.Product)
                                         .ThenInclude(p => p.ProductImages)
                                         .Include(p => p.User)
                                         .Where(p => p.BrowserId == BrowserId && p.UserId == null).FirstOrDefault();




                if (UserId != null)
                {
                    var user = _context.Users.Find(UserId);
                    if (cart != null)
                    {
                        cart.User = user;
                        _context.SaveChanges();
                    }


                    cart = _context.Carts
                                            .Include(p => p.CartItems)
                                            .ThenInclude(p => p.Product)
                                            .ThenInclude(p => p.ProductImages)
                                            .Include(p => p.User)
                                            .Where(p => p.BrowserId == BrowserId && p.UserId == UserId && p.Finished == false).FirstOrDefault();

                }

                if (cart != null)
                {
                    return new ResultDto<GetCartItemDto>()
                    {
                        Data = new GetCartItemDto()
                        {
                            PriceSum = cart.CartItems.Sum(p => p.Count * p.Price),
                            DiscountSum = cart.CartItems.Sum(p => p.Count * p.Discount),
                            CountSum = cart.CartItems.Count(),
                            UserId = cart.UserId,
                            CartId = cart.Id,
                            Name = cart.User != null ? cart.User.Name : "",
                            PhoneNumber =cart.User !=null ? cart.User.PhoneNumber : 0,
                            Email = cart.User != null ? cart.User.Email : "",
                            CartItem = cart.CartItems.Select(p => new CartItemDto()
                            {
                                Price = p.Price,
                                Discount=p.Discount,
                                Count = p.Count,
                                Image = p.Product?.ProductImages?.FirstOrDefault()?.Src ?? "",
                                Id = p.Id,
                                Name = p.Product.Name,
                                ProductPriceSum = p.Price * p.Count,
                                ProductDiscountSum=p.Discount * p.Count,

                            }).ToList(),
                        },
                        Message = "سبد خرید با موفقیت ارسال شد",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new ResultDto<GetCartItemDto>()
                    {
                        Data = new GetCartItemDto()
                        {
                            CartItem = new List<CartItemDto>(),
                        },
                    };

                }

                //var cart = _context.Carts
                //    .Include(p => p.CartItems)
                //    .ThenInclude(p => p.Product)
                //    .ThenInclude(p => p.ProductImages)
                //    .Where(p => p.BrowserId == BrowserId).FirstOrDefault();
                //var mycart = new GetCartItemDto()
                //{
                //    PriceSum = cart.CartItems.Sum(p => p.Price * p.Count),
                //    CartItem = cart.CartItems.Select(p => new CartItemDto()
                //    {
                //        Price = p.Price,
                //        Count = p.Count,
                //        ProductPriceSum = p.Price * p.Count,
                //        Id = p.ProductId,
                //        Name = p.Product.Name,
                //        Image = p.Product?.ProductImages?.FirstOrDefault()?.Src ?? "",
                //    }).ToList(),
                //};

                //return new ResultDto<GetCartItemDto>
                //{
                //    Data = mycart,
                //    IsSuccess = true,
                //    Message = "ok"
                //};

                //var mycart = _context.Carts
                //    .Include(p => p.CartItems)
                //    .ThenInclude(p => p.Product)
                //    .ThenInclude(p => p.ProductImages)
                //    .Where(p => p.BrowserId == BrowserId).Select(p => new GetCartItemDto()
                //    {
                //        PriceSum = p.CartItems.Sum(p => p.Count * p.Price),
                //        CartItem = p.CartItems.Select(p => new CartItemDto
                //        {
                //            Price = p.Price,
                //            Count = p.Count,
                //            ProductPriceSum = p.Price * p.Count,
                //            Id = p.ProductId,
                //            Name = p.Product.Name,
                //        }).ToList()
                //    });
                //return new ResultDto<GetCartItemDto>()
                //{
                //    Data = mycart,
                //    IsSuccess = true,
                //    Message = "ok",
                //};
            }
            catch (Exception)
            {

                throw;
            }


        }

        public ResultDto RemoveItemOfCart(long CartItemId, Guid BrowserId)
        {
            try
            {

                var item = _context.CartItems
                .Where(p => p.Id == CartItemId).FirstOrDefault();

                if (item != null)
                {
                    item.IsRemoved = true;
                    item.RemoveTime = DateTime.Now;
                    _context.SaveChanges();
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "محصول از سبد حذف گردید"
                    };
                }
                else
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "محصول یافت نشد"
                    };
                }

                //var cart = _context.Carts.Where(p => p.BrowserId == BrowserId).FirstOrDefault();
                //var item = _context.CartItems.Where(p => p.CartId == cart.Id && p.Id == CartItemId).FirstOrDefault();
                //item.IsRemoved = true;
                //_context.SaveChanges();
                //return new ResultDto()
                //{
                //    IsSuccess = true,
                //    Message = "محصول از سبد حذف گردید"
                //};
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ResultDto Subcount(long CartItemId, Guid BrowserId)
        {
            try
            {
                //error dar recorde a be bad
                //var item = _context.Carts
                //    .Include(p => p.CartItems)
                //    .Where(p => p.BrowserId == BrowserId && p.CartItems.FirstOrDefault().Id == CartItemId).FirstOrDefault();
                //if (item.CartItems.FirstOrDefault().Count>0)
                //{
                //    item.CartItems.FirstOrDefault().Count -= 1;
                //}

                var item = _context.CartItems
                .Where(p => p.Id == CartItemId).FirstOrDefault();
                if (item.Count > 1)
                {
                    item.Count -= 1;
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تعداد با موفیت کم شد"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResultDto AddCount(long CartItemId, Guid BrowserId)
        {
            try
            {
                //error dar recorde a be bad
                //var item = _context.Carts
                //              .Include(p => p.CartItems)
                //              .Where(p => p.BrowserId == BrowserId && p.CartItems.FirstOrDefault().Id == CartItemId).FirstOrDefault();
                //item.CartItems.FirstOrDefault().Count += 1;


                var item = _context.CartItems
                .Where(p => p.Id == CartItemId).FirstOrDefault();
                item.Count += 1;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تعداد با موفیت اضافه شد"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class GetCartItemDto
    {
        public List<CartItemDto> CartItem { get; set; }

        public long PriceSum { get; set; }

        public int DiscountSum { get; set; }
        public long CountSum { get; set; }

        //For Add PayRequest
        public long? UserId { get; set; }

        public long CartId { get; set; }

        //for zarin pal
        public string? Name { get; set; }
        public string? Email { get; set; }
        public long? PhoneNumber { get; set; }
    }

    public class CartItemDto
    {

        public int Price { get; set; }

        public int Discount { get; set; }

        public int Count { get; set; }

        public int Inventory { get; set; }


        public string Image { get; set; }

        public long Id { get; set; }

        public string? Name { get; set; }

        public long ProductPriceSum { get; set; }
        public long ProductDiscountSum { get; set; }

        


    }
}
