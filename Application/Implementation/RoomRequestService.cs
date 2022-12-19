using Application.Interface;
using DB.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class RoomRequestService : IRoomRequestService
    {
        public async Task<bool> CreateRoomAsync(RoomDto room)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync(RulesDto rules)
        {
            throw new NotImplementedException();
        }

        public async Task<RoomDto> GetRoomByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
