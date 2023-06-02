using Microsoft.EntityFrameworkCore;
using MyMarket.Application.Interfaces;
using MyMarket.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MyMarket.Application.Services.Common.GetMenuItem
{
    public class GetMenuItem : IGetMenuItem
    {
        private readonly IMyDBContetx _context;
        public GetMenuItem(IMyDBContetx contetx)
        {
            _context = contetx;
        }
        public ResultDto<List<GetMenuItemDto>> Execute()
        {
            var category = _context.Categories
                .Include(p => p.ParentCategory)
                .Include(p => p.SubCategories)
                .ThenInclude(p => p.SubCategories)
                .Where(p => p.ParentId == null)
                .ToList()

                .Select(p => new GetMenuItemDto
                {
                    Id = p.Id,
                    Name = p.Name,

                    //ParentId = p.ParentCategory.Id,
                    //ParentName = p.ParentCategory.Name,
                    Child = p.SubCategories.ToList().Select(c => new GetMenuItemDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Child = c.SubCategories.ToList().Select(d => new GetMenuItemDto
                        {
                            Id=d.Id,
                            Name=d.Name

                        }).ToList(),

                    }).ToList(),

                }).ToList();
            return new ResultDto<List<GetMenuItemDto>>
            {
                Data = category,
                IsSuccess = true
            };


        }
    }
}
