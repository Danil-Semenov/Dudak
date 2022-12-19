using DB.DTO;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Mappers
{
    public static class UserMappingExtentions
    {
        public static UserDto ToDTO(this User user)
        {
            if(user == null)
                return null;

            UserDto userDto = new UserDto();

            userDto.Id = user.Id;
            userDto.Login = user.Login;
            userDto.Password = user.Password;

            return userDto;
        }
    }
}
