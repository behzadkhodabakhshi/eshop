using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto<EditProductServiceDto> Execute(long id);
    }

    public class EditProductService : IEditProductService
    {
        private readonly IMyDBContetx _context;
        public EditProductService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<EditProductServiceDto> Execute(long id)
        {
            var product = _context.products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .ThenInclude(p => p.SubCategories)
                .Where(p => p.Id == id).FirstOrDefault();
            var images = _context.GetProductImages.Where(p => p.ProductId == id).ToList();
            var features = _context.ProductFeatures.Where(p => p.ProductId == id).ToList();
            return new ResultDto<EditProductServiceDto>
            {
                Data = new EditProductServiceDto()
                {
                    id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Description = product.Description,
                    Price = product.Price,
                    Discount=product.Discount,
                    Inventory = product.Inventory,
                    Displayed = product.Displayed,
                    InstantOffer=product.InstantOffer,
                    ViewCount = product.ViewCount,
                    Category = $"{product.Category.ParentCategory.Name}-{product.Category.Name}",
                    ProductImages = images.Select(p => new ProductImageDto
                    {
                        Src = p.Src,
                        Id=p.Id,
                        
                    }).ToList(),
                    productFeatures = features.Select(p => new ProductFeatureDto
                    {
                        DisplayName = p.DisplayName,
                        Value = p.Value,
                        Id=p.Id,
                    }).ToList(),
                },
                IsSuccess = true
            };

        }
    }

    public class EditProductServiceDto
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }

        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public bool InstantOffer { get; set; }
        public long ViewCount { get; set; }

        public string Category { get; set; }

        public List<ProductImageDto> ProductImages { get; set; }

        public List<ProductFeatureDto> productFeatures { get; set; }
    }

    public class ProductImageDto
    {
        public string Src { get; set; }

        public long Id { get; set; }
    }

    public class ProductFeatureDto
    {
        public string DisplayName { get; set; }

        public string Value { get; set; }
        public long Id { get; set; }
    }
}
