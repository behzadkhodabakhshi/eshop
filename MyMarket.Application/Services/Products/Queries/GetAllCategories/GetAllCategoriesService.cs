using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MyMarket.Application.Services.Products.Queries.GetAllCategories
{
    public class GetAllCategoriesService : IGetAllCategoriesService
    {
        private readonly IMyDBContetx _context;
        public GetAllCategoriesService(IMyDBContetx contetx)
        {
            _context = contetx;
        }
        public ResultDto<List<CategoryDto>> Execute(long? parentid)
        {
            var category = _context.Categories
                .Include(p => p.ParentCategory)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.SubCategories)
                .Where(p => p.ParentId == parentid)
                .ToList()
                .Select(p => new CategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Parent = p.ParentCategory != null ? new
                    ParentCategoryDto
                    {
                        Id = p.ParentCategory.Id,
                        Name = p.ParentCategory.Name,
                        Parent_parent=p.ParentCategory.ParentCategory != null ? new
                        ParentparentCategoryDto
                        {
                            Id = p.ParentCategory.ParentCategory.Id,
                             Name = p.ParentCategory.ParentCategory.Name,
                        }
                        : null,
                    }
                    : null,
                    //parent_parentid=p.ParentCategory.ParentId,
                    ///////////////
                    //Parent_parent = p.ParentCategory.ParentCategory != null ? new
                    //ParentCategoryDto
                    //{
                    //    Id = p.ParentCategory.ParentCategory.Id,
                    //    Name = p.ParentCategory.ParentCategory.Name,
                    //}
                    //: null,
                    ////////////////////



                    HasChild = p.SubCategories.Count() > 0 ? true : false,

                }).ToList();
            return new ResultDto<List<CategoryDto>>
            {
                IsSuccess = true,
                Message = "لیست با موفقیت برگشت داده شد",
                Data = category,

            };

        }


    }
}
