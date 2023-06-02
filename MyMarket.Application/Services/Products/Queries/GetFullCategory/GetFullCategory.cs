using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MyMarket.Application.Services.Products.Queries.GetFullCategory
{
    public class GetFullCategory : IGetFullCategory
    {
        private readonly IMyDBContetx _contetx;
        public GetFullCategory(IMyDBContetx contetx)
        {
            _contetx = contetx;
        }
        public ResultDto<List<GetFullCategoryDto>> Execute()
        {
            //agar bekhahim faghat rishehara namayesh dahad
            var Category = _contetx.Categories
                .Include(p => p.ParentCategory)
                .ThenInclude(p => p.ParentCategory)
                .ThenInclude(p => p.ParentCategory)
                .Where(p => p.SubCategories.Count() == 0)


                .ToList()
                .Select(p => new GetFullCategoryDto
                {
                    Id = p.Id,
                    Name = p.ParentCategory == null ? $"{p.Name}" : p.ParentCategory.ParentCategory == null ? $"{p.ParentCategory.Name}-{p.Name}" : $"{p.ParentCategory.ParentCategory.Name}-{p.ParentCategory.Name}-{p.Name}"

                }).ToList();


            //agar bekhahim hameye gereha ra namayesh dahad
            //var Category = _contetx.Categories
            //    .Include(p => p.SubCategories)
            //    .Include(p => p.ParentCategory)
            //    .ThenInclude(p => p.ParentCategory)
            //    .Where(p => p.ParentCategory != null || (p.ParentCategory == null & p.SubCategories.Count() == 0) || (p.ParentCategory == null & p.SubCategories.Count() > 0))
            //    .ToList()
            //    .Select(p => new GetFullCategoryDto
            //    {
            //        Id = p.Id,
            //        Name = p.ParentCategory == null ? $"{p.Name}" : p.ParentCategory.ParentCategory == null ? $"{p.ParentCategory.Name}-{p.Name}" : $"{p.ParentCategory.ParentCategory.Name}-{p.ParentCategory.Name}-{p.Name}"

            //    }).ToList();



            return new ResultDto<List<GetFullCategoryDto>>
            {
                Data = Category,
                IsSuccess = true,
                Message = "لیست با موفقیت برگشت داده شد",
            };
        }
    }
}
