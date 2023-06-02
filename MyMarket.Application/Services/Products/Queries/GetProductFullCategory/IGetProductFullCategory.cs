using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Products.Queries.GetProductFullCategory
{
  public  interface IGetProductFullCategory
    {
        ResultDto<GetProductFullCategoryDto> Execute(long id);
    }

    public class GetProductFullCategory : IGetProductFullCategory
    {
        public ResultDto<GetProductFullCategoryDto> Execute(long id)
        {
            throw new NotImplementedException();
        }
    }

    public class GetProductFullCategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
