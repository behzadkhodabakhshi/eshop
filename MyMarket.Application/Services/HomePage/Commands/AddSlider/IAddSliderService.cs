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

namespace MyMarket.Application.Services.HomePage.Commands.AddSlider
{
    public interface IAddSliderService
    {
        ResultDto Execute(AddSliderImageDto slider);
    }

    public class AddSliderService : IAddSliderService
    {
        private readonly IMyDBContetx _context;
        private readonly IHostingEnvironment _environment;
        public AddSliderService(IHostingEnvironment environment,IMyDBContetx context)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(AddSliderImageDto slider)
        {
            var uplfile = UploadFile(slider.Image);

            Slider newslider = new Slider()
            {

                Src = uplfile.FileNameAddress,
                Link = slider.Link
            };
            _context.Sliders.Add(newslider);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess=true,
                Message="اسلایدر با موفقیت افزوده شد"
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



    public class AddSliderImageDto
    {
        

        public string Link { get; set; }

        public IFormFile Image { set; get; }

        public ImageLocation ImageLocation { get; set; }
    }
}
