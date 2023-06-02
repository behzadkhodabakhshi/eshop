using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Products.Queries.GetAllProductForSite
{

    public interface IGetAllProductForSiteService
    {
        GetAllProductForSiteServiceDto Execute(long? SearchCatId, string? SearchKey, long? categoryid, int Page, int PageSize, Ordering ordering);

    }

    public class GetAllProductForSiteService : IGetAllProductForSiteService
    {
        private readonly IMyDBContetx _context;
        public GetAllProductForSiteService(IMyDBContetx context)
        {
            _context = context;
        }

        //public GetAllProductForSiteServiceDto Execute(int? SearchCatId, string? SearchKey, int? categoryid, int Page, int PageSize)
        public GetAllProductForSiteServiceDto Execute(long? SearchCatId, string? SearchKey, long? categoryid, int Page, int PageSize, Ordering ordering)
        {
            int rowcount = 0;
            var products_query = _context.products
                  .Include(p => p.ProductImages).AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchKey))
            {
                products_query = products_query.Where(p => p.Name.Contains(SearchKey) || p.Brand.Contains(SearchKey)).AsQueryable();
            }
            if (SearchCatId != null)
            {
                products_query = products_query.Where(p => p.CategoryId == SearchCatId || p.Category.ParentCategory.Id == SearchCatId).AsQueryable();
            }

            if (categoryid != null)
            {
                products_query = products_query.Where(p => p.CategoryId == categoryid || p.Category.ParentCategory.Id == categoryid || p.Category.ParentCategory.ParentCategory.Id == categoryid).AsQueryable();
            }

            switch (ordering)
            {
                case Ordering.NotOrder:
                    products_query = products_query.OrderBy(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    products_query = products_query.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.Bestselling:
                    break;
                case Ordering.MostPopular:
                    break;
                case Ordering.theNewest:
                    products_query = products_query.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    products_query = products_query.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.theMostExpensive:
                    products_query = products_query.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                default:
                    break;

            }

            var products = products_query.ToPaged(Page, PageSize, out rowcount);

            Random rnd = new Random();
            return new GetAllProductForSiteServiceDto
            {
                Page = Page,
                PageSize = PageSize,
                RowCount = rowcount,

                Products = products.Select(p => new GetProduct
                {
                    Id = p.Id,
                    Name = p.Name,
                    Star = rnd.Next(1, 5),
                    Brand = p.Brand,
                    ImgSrc = p.ProductImages.FirstOrDefault()?.Src,
                    Price = p.Price,
                    Discount=p.Discount,
                    // color=p.productFeatures.FirstOrDefault().Value,

                }).ToList(),
            };



        }


    }




    public class GetAllProductForSiteServiceDto
    {
        public List<GetProduct> Products { get; set; }
        public int RowCount { get; set; }
        public int PageSize { get; set; }

        public int Page { get; set; }

    }

    public class GetProduct
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        public int Discount { get; set; }


        public int Star { get; set; }

        public string Brand { get; set; }

        public string ImgSrc { get; set; }
        public string color { set; get; }
    }

    public class InGetAllProductForSiteServiceDto
    {
        public long SearchCatId { set; get; }
        public string SearchKey { set; get; }
        public int categoryid { set; get; }
        public int Page { set; get; } = 1;
        public int PageSize { set; get; } = 2;
    }
    public enum Ordering
    {
        NotOrder = 0,
        /// <summary>
        /// پربازدیدترین
        /// </summary>
        MostVisited = 1,
        /// <summary>
        /// پرفروشترین
        /// </summary>
        Bestselling = 2,
        /// <summary>
        /// محبوبترین
        /// </summary>
        MostPopular = 3,
        /// <summary>
        /// جدیدترین
        /// </summary>
        theNewest = 4,
        /// <summary>
        /// ارزانترین
        /// </summary>
        Cheapest = 5,
        /// <summary>
        /// گرانترین
        /// </summary>
        theMostExpensive = 6
    }
}

