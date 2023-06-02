using MyMarket.Common.Dto;

namespace MyMarket.Application.Services.Users.Commands.Changetatus
{
    public interface IChangeStatusUser
    {
        public ResultDto Execute(long UserId);
    }
}
