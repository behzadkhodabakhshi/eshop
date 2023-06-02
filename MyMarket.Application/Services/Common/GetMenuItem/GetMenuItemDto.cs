using System.Collections.Generic;

namespace MyMarket.Application.Services.Common.GetMenuItem
{
    public class GetMenuItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        //public long? ParentId { get; set; }
        //public string? ParentName { get; set; }
        public List<GetMenuItemDto> Child { set; get; }

        //public List<GetMenuItemDto> ChildChild { set; get; }
    }
}
