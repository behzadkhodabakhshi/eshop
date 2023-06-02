using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Products.Queries.GetProductDetailsForSite
{
    public interface IGetProductDetailForSiteService
    {
        ResultDto<GetProductDetailForSiteDto> Execute(long Id);
    }

    public class GetProductDetailForSiteService : IGetProductDetailForSiteService
    {
        //تزریق وابستگی
        private readonly IMyDBContetx _context;
        //ctor
        public GetProductDetailForSiteService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<GetProductDetailForSiteDto> Execute(long Id)
        {
            var product = _context.products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .ThenInclude(p=>p.ParentCategory)
                .Include(p => p.productFeatures)
                .Include(p => p.ProductImages)
                .Where(p => p.Id == Id).FirstOrDefault();
                 product.ViewCount++;
                 _context.SaveChanges();


            if (product == null)
            {
                throw new Exception("Product Not Found...");


            }
            return new ResultDto<GetProductDetailForSiteDto>
            {
                Data = new GetProductDetailForSiteDto
                {
                    Id=product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Description = product.Description,
                    Price = product.Price,
                    Discount=product.Discount,
                    Inventory = product.Inventory,
                    //Category = $"{product.Category.ParentCategory.Name} - { product.Category.Name }",
                    Category = product.Category.ParentCategory == null ? $"{product.Category.Name}" : product.Category.ParentCategory.ParentCategory == null ? $"{product.Category.ParentCategory.Name}/{product.Category.Name}" : $"{product.Category.ParentCategory.ParentCategory.Name}/{product.Category.ParentCategory.Name}/{product.Category.Name}",

                    Images = product.ProductImages.Select(p => p.Src).ToList(),
                    Features = product.productFeatures.Select(p => new ProductFeature
                    {
                        DisplayName = p.DisplayName,
                        Value = p.Value,
                    }).ToList()

                },
                IsSuccess = true,

            };
        }
    }

    public class GetProductDetailForSiteDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int Discount { get; set; }
        public int Inventory { get; set; }

        public String Category { set; get; }
        public List<string> Images { set; get; }

        public List<ProductFeature> Features { set; get; }
    }
}
