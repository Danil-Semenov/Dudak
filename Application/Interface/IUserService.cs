using DB.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);

        Task<UserDto> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}
