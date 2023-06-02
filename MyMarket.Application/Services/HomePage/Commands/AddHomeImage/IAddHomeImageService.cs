using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyMarket.Application.Interfaces;
using MyMarket.Application.Services.Products.Commands.CreateNewProduct;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.HomePage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyMarket.Application.Services.HomePage.Commands.AddHomeImage
{
    public interface IAddHomeImageService
    {
        ResultDto Execute(AddHomeImageDto home);
    }

    public class AddHomeImageService : IAddHomeImageService
    {
        private readonly IMyDBContetx _context;
        private readonly IHostingEnvironment _environment;
        public AddHomeImageService(IMyDBContetx context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(AddHomeImageDto home)
        {
            var uplfile = UploadFile(home.Image);

            HomeImage homeimage = new HomeImage()
            {
                Src = uplfile.FileNameAddress,
                Link = home.Link,
                ImageLocation = home.ImageLocation
            };
            _context.HomeImages.Add(homeimage);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "لیست با موفقیت برگشت داده شد"

            };
        }

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\SlideImages\";
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

    public class AddHomeImageDto
    {
        public string Link { get; set; }

        public IFormFile Image { set; get; }

        public ImageLocation ImageLocation { get; set; }

    }
}
