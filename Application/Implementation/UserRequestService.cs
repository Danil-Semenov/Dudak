using Application.Interface;
using DB;
using DB.DTO;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class UserRequestService : IUserRequestService
    {
        private readonly DurakDbContext _context;

        public UserRequestService(DurakDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateProfileAsync(UserDto profile)
        {
            var NewProfile = new User()
            {
                Login = profile.Login,
                Password = profile.Password
            };
            _context.Users.Add(NewProfile);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditProfileAsync(UserDto profile, int id)
        {
            var editProfile = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
            _context.Users.Update(editProfile);
            editProfile.Login = profile.Login;
            editProfile.Password = profile.Password;
            //_context.Users.Update(editProfile);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto> GetGuestProfileAsync()
        {
            var index = 1;
            var list = GemeCore.Guests.ToList();
            while (true) //Решить проблему что гости всегда одинаковые
            {
                if (!list.Contains(index))
                {
                    list.Add(index);
                    break;
                }
                index++;
            }
            GemeCore.Guests = list;
            return new UserDto() { Login = "guest_" + index, Password = "guest_pas" };
        }

        public async Task<bool> IsThereSuchGuyAsync(string login, string password)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Login == login && u.Password == password) != null;
        }
    }
}
