using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Products.Queries.GetFullCategory
{
   public interface IGetFullCategory
    {
        ResultDto<List<GetFullCategoryDto>> Execute();
    }
}
