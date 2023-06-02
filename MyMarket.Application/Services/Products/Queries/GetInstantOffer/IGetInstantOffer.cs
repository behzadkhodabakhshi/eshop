using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Products.Queries.GetInstantOffer
{
    public interface IGetInstantOffer
    {
        List<GetInstantOfferDto> Execute();
    }

    public class GetInstantOffer : IGetInstantOffer
    {
        private readonly IMyDBContetx _context;
        public GetInstantOffer(IMyDBContetx context)
        {
            _context = context;
        }
        public List<GetInstantOfferDto> Execute()
        {
            var product = _context.products
                .Where(p => p.InstantOffer == true)
                .Include(p=>p.ProductImages)
                .ToList();
            return product.Select(p => new GetInstantOfferDto
            {
                Id = p.Id,
                Name = p.Name,
                ImgSrc = p.ProductImages.FirstOrDefault().Src,
                Brand = p.Brand,
                Discount = p.Discount,
                Price = p.Price,
            }).ToList();
        }
    }


    public class GetInstantOfferDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
        public int Discount { get; set; }

        public string Brand { get; set; }

        public string ImgSrc { get; set; }
    }
}
