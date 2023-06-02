namespace MyMarket.Application.Services.Users.Queries.GetUsers
{
    public class RequestGetUserDto
    {
        public string SerachKey { set; get; }

        public int PageSize { get; set; }
        public int Page { set; get; }

    }
}
