using System.Collections.Generic;

namespace MyMarket.Application.Services.Users.Commands.UserLogin
{
    public class ResultUserloginDto
    {
        public long UserId { get; set; }

        public List<string> Role { get; set; }

        //public string Role { get; set; }
        public string Name { get; set; }
    }
}

