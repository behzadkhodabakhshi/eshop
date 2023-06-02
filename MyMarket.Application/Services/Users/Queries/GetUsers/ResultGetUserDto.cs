using System.Collections.Generic;

namespace MyMarket.Application.Services.Users.Queries.GetUsers
{
  
        public class ResultGetUserDto 
        {
            public IEnumerable<GetUserDto> Users { get; set; }
            public long RowsCount { get; set; }



        }
    }

