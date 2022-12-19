using DB.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserRequestService
    {
        Task<bool> CreateProfileAsync(UserDto profile);

        Task<bool> EditProfileAsync(UserDto profile, int id);

        Task<UserDto> GetGuestProfileAsync();
    }
}
