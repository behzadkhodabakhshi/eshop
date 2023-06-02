namespace MyMarket.Application.Services.Users.Commands.EditUser
{
    public class EditUserRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public  bool adminchecked { get; set; }
        public bool customerchecked { get; set; }
        public bool userchecked { get; set; }
        



    }
}
