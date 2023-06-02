namespace MyMarket.Application.Services.Users.Queries.GetUsers
{


    public class GetUserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
      

        public bool AdminCheked { get; set; }
        public bool CustomerCheked { get; set; }
        public bool UserCheked { get; set; }
    }
}
