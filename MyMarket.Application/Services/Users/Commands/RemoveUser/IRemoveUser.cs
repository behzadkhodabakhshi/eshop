using Microsoft.EntityFrameworkCore.Storage;
using MyMarket.Common.Dto;
using System.Net.Sockets;
using System.Text;

namespace MyMarket.Application.Services.Users.Commands.RemoveUser
{
    public interface IRemoveUser
    {
        public ResultDto Execute(RequestRemoveUser request);
    }
}
