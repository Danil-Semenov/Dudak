using DB.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Application.Interface
{
    public interface IRoomRequestService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync(RulesDto rules);

        Task<bool> CreateRoomAsync(RoomDto room);

        Task<RoomDto> GetRoomByIDAsync(int id);
    }
}
