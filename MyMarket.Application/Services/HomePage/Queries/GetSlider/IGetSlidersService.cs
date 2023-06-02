using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.HomePage.Queries.GetSlider
{
   public interface IGetSlidersService
    {
        ResultDto<List<Slider>> Execute();
    }

    public class GetSlidersService : IGetSlidersService

    {

        private readonly IMyDBContetx _context;
        public GetSlidersService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<List<Slider>> Execute()
        {
            var slider = _context.Sliders.Select(p => new Slider
            {
                Src=p.Src,
                Link=p.Link
            }).ToList();

            return new ResultDto<List<Slider>>
            {
                Data = slider,
                IsSuccess =true,
                Message="لیست اسلایدر با موفقیت دریافت شد"
            };
        }
    }

    public class GetSlidersDto
    {
        public string Src { get; set; }

        public string Link { get; set; }
    }
}
