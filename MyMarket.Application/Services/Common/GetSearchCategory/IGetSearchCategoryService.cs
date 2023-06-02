using MyMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMarket.Application.Services.Common.GetSearchCategory
{
   public interface IGetSearchCategoryService
    {
        List<GetSearchCategoryDto> Execute();
    }

    public class GetSearchCategoryService : IGetSearchCategoryService
    {
        private readonly IMyDBContetx _context;
        public GetSearchCategoryService(IMyDBContetx context)
        {
            _context = context;
        }
        public List<GetSearchCategoryDto> Execute()
        {


            //var categories = _context.Categories
            //    .Where(p => p.ParentCategory == null).ToList();

            //return categories.Select(p => new GetSearchCategoryDto
            //{
            //    Id=p.Id,
            //    Name=p.Name

            //}).ToList();


            //var categories = _context.Categories
            //    .Where(p => p.ParentCategory == null).Select(p=> new GetSearchCategoryDto
            //    {
            //        Id=p.Id,
            //        Name=p.Name
            //    }
            //    ).ToList();
            //return categories;

            return _context.Categories
                .Where(p => p.ParentCategory == null).Select(p => new GetSearchCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();



        }
    }

    public class GetSearchCategoryDto
    {
        public long Id { set; get; }
        public string Name { get; set; }

    }
}
