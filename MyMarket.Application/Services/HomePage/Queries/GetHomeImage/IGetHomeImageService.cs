using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using MyMarket.Domain.Entity.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.HomePage.Queries.GetHomeImage
{
    public interface IGetHomeImageService
    {
        ResultDto<List<HomeImage>> Execute();
    }

    public class GetHomeImageService : IGetHomeImageService
    {
        private readonly IMyDBContetx _context;

        public GetHomeImageService(IMyDBContetx context)
        {
            _context = context;
        }
        public ResultDto<List<HomeImage>> Execute()
        {
            var images = _context.HomeImages.Select(p => new HomeImage
            {
                Src=p.Src,
                Link=p.Link,
                ImageLocation=p.ImageLocation
            }).ToList();



            return new ResultDto<List<HomeImage>>
            {
                Data = images,
                IsSuccess = true
            };
        }
    }



}
