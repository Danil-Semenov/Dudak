using Application.Interface;
using DB;
using DB.DTO;
using DB.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly DurakDbContext _context;

        public UserService(DurakDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return (await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id))?.ToDTO();
        }

        public async Task<UserDto> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            return (await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Login == login && u.Password == password))?.ToDTO();
        }
    }
}
