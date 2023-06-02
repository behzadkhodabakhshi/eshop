using MyMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMarket.Application.Services.Common.GetMenuItem
{
    public interface IGetMenuItem
    {
        ResultDto<List<GetMenuItemDto>> Execute();
    }
}
