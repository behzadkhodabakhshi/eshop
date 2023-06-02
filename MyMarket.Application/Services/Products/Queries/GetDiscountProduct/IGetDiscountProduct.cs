using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Products.Queries.GetDiscountProduct
{
  public  interface IGetDiscountProduct
    {
        List<GetDiscountProductDto> Execute();
    }

    public class GetDiscountProduct : IGetDiscountProduct
    {
        private readonly IMyDBContetx _context;
        public GetDiscountProduct(IMyDBContetx contex)
        {
            _context = contex;
        }
        public List<GetDiscountProductDto> Execute()
        {
            var product = _context.products
            .Where(p => p.Discount > 0)
            .Include(p => p.productFeatures)
            .Include(p=>p.ProductImages)
            .OrderByDescending(p => p.Discount).Take(5).AsQueryable();
            return product.Select(p => new GetDiscountProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Brand = p.Brand,
                ImgSrc = p.ProductImages.FirstOrDefault().Src,
                Price = p.Price,
                Discount = p.Discount,
                Feature = p.productFeatures.Select(p => new ProductFeature
                {
                    DisplayName = p.DisplayName,
                    Value = p.Value
                }).ToList()

            }).ToList();
                    

           
        }
    }
    public class GetDiscountProductDto
    {
        public List<ProductFeature> Feature { get; set; }
        public long Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        public int Discount { get; set; }

        public string Brand { get; set; }

        public string ImgSrc { get; set; }
 
    }
    
}
