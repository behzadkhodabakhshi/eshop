using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyMarket.Application.Services.Products.Queries.GetAllCategories
{
    public interface IGetAllCategoriesService
    {

        ResultDto<List<CategoryDto>> Execute(long? parentid);
        
    }
}
