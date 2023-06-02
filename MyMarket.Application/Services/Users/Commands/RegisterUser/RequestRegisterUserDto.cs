namespace MyMarket.Application.Services.Users.Commands
{
    public class RequestRegisterUserDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long PhoneNumber { get; set; }
        public string RePasword { get; set; }
        public long RoleId { get; set; }
        public bool AdminCheked { get; set; }
        public bool CustomerCheked { get; set; }
        public bool UserCheked { get; set; }



    }

}
