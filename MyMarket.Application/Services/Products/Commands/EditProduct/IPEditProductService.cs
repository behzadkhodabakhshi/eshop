using Microsoft.AspNetCore.Hosting;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.Product;
using MyMarket.Domain.Entity.Carts;

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using MyMarket.Application.Services.Products.Commands.CreateNewProduct;
using System.Linq;

namespace MyMarket.Application.Services.Products.Commands.EditProduct
{
    public interface IPEditProductService
    {
        ResultDto Execute(EditProductDto request);

    }

    public class PEditProductService : IPEditProductService
    {
        private readonly IMyDBContetx _context;
        private readonly IHostingEnvironment _environment;
        public PEditProductService(IMyDBContetx contetx, IHostingEnvironment hostingEnvironment)
        {
            _context = contetx;
            _environment = hostingEnvironment;

        }
        public ResultDto Execute(EditProductDto request )
        {
            try
            {
                var category = _context.Categories.Find(request.CategoryId);
                var product = _context.products.Find(request.Id);

               

                product.Name = request.Name;
                product.Price = request.Price;
                product.Discount = request.Discount;
                product.Inventory = request.Inventory;
                product.Brand = request.Brand;
                product.Description = request.Description;
                product.Displayed = request.Displayed;
                product.InstantOffer = request.InstantOffer;
                product.Category = category;

              
                List<ProductImage> productimage = new List<ProductImage>();
                ///edite mablaghe sabade kharid
                List<CartItem> cartitem = new List<CartItem>();
                cartitem = _context.CartItems.Where(p => p.ProductId == request.Id).ToList();
                foreach(var cart in cartitem)
                {
                    cart.Price = request.Price;
                    cart.Discount = request.Discount;   
                }
                ///
                foreach (var item in request.Images)
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
                foreach (var item in request.productFeatures)
                {
                    productFeatures.Add(new ProductFeature
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product,
                    });
                }

                _context.ProductFeatures.AddRange(productFeatures);
                _context.SaveChanges();
                return new ResultDto
                {

                    IsSuccess = true,
                    Message = "محصول با موفقیت ویرایش شد"
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

