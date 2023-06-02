using System.Collections.Generic;

namespace MyMarket.Application.Services.Users.Queries.GetUsers
{
    public class GetListUserDto
    {
        public List<GetUserDto> Users { get; set; }
        public int RowCount { get; set; }

        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
