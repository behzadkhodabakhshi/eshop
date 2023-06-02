using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Cache;
using System.Text;

namespace MyMarket.Application.Services.Products.Commands.CreateNewProduct
{
    public interface ICreateNewProductService
    {
        ResultDto Execute(CreateProductDto request);
    }

    public class CreateNewProductService : ICreateNewProductService
    {
        private readonly IMyDBContetx _context;
        private readonly IHostingEnvironment _environment;
        public CreateNewProductService(IMyDBContetx contetx, IHostingEnvironment hostingEnvironment)
        {
            _context = contetx;
            _environment = hostingEnvironment;
            
        }
        public ResultDto Execute(CreateProductDto requset)
        {
            try
            {
                var category = _context.Categories.Find(requset.CategoryId);
                Product product = new Product()
                {
                    Brand = requset.Brand,
                    Description = requset.Description,
                    Name = requset.Name,
                    Price = requset.Price,
                    Discount=requset.Discount,
                    Inventory = requset.Inventory,
                    Category = category,
                    Displayed = requset.Displayed,
                    InstantOffer=requset.InstantOffer,
                };
                _context.products.Add(product);

                List<ProductImage> productimage = new List<ProductImage>();

                foreach (var item in requset.Images)
                {
                    var uploadresult = UploadFile(item);
                    productimage.Add(new ProductImage
                    {
                        Src = uploadresult.FileNameAddress,
                        Product = product,
                    });
                }

                _context.GetProductImages.AddRange(productimage);

                List<ProductFeature> productFeatures = new List<ProductFeature>();
                foreach (var item in requset.productFeatures)
                {
                    productFeatures.Add(new ProductFeature
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product=product,
                    });
                }

                _context.ProductFeatures.AddRange(productFeatures);
                _context.SaveChanges();
                return new ResultDto
                {
                    
                    IsSuccess = true,
                    Message = "لیست با موفقیت اضافه شد"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {

                    IsSuccess = false,
                    Message = "خطا در انجام عملیات"
                };

            }
           
        }
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }

    }

   
}
