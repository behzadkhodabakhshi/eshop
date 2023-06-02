using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Products.Commands.CreateNewCategory
{
    public interface ICreateNewCategoryService
    {
        ResultDto Execute(long? ParentId, string Name);
    }
}
