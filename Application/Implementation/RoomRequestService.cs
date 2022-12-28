using Application.Interface;
using Core;
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
    public class RoomRequestService : IRoomRequestService
    {
        public async Task<int> CreateRoomAsync(RoomDto room)
        {
            var list = GemeCore.Rooms.ToList();
            var lastRoom = list.LastOrDefault();
            var id = lastRoom == null ? 1 : lastRoom.Id + 1;
            var rules = room.Rules == null ? new VanilaRules() : new VanilaRules();
            var players = new List<string>() { room.Host };
            list.Add(new Core.Room(rules, players) 
            {
                Id = id,
                MaxPlayers = room.MaxPlayers,
                Password = room.Password
            });
            GemeCore.Rooms = list;
            return id;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync(RulesDto rules)
        {
            var result = new List<RoomDto>();
            foreach(var room in GemeCore.Rooms)
            {
                result.Add(new RoomDto()
                { 
                    Id = room.Id,
                    Name = room.Players.First().Name,
                    Host = room.Players.First().Name,
                    MaxPlayers = room.MaxPlayers,
                    PlayersCount = room.Players.Count,
                    IsOpen = string.IsNullOrEmpty(room.Password)
                });
            }
            return result;
        }

        public async Task<RoomDto> GetRoomByIDAsync(int id)
        {
            var room = GemeCore.Rooms.First(r=>r.Id == id);
            return new RoomDto()
            {
                Id = room.Id,
                Name = room.Players.First().Name,
                Host = room.Players.First().Name,
                MaxPlayers = room.MaxPlayers,
                PlayersCount = room.Players.Count,
                IsOpen = string.IsNullOrEmpty(room.Password)
            };
        }

        public async Task<bool> ComeRoomByIDAsync(int id, string password, string player)
        {
            var room = GemeCore.Rooms.FirstOrDefault(r => r.Id == id);
            if(room != null)
            {
                if (string.IsNullOrEmpty(room.Password) || password == room.Password)
                {
                    return room.AddPlayer(player);
                }
            }
            return false;
        }

        public async Task<bool> EscapeFromRoomByIDAsync(int id, string player)
        {
            var room = GemeCore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room != null)
            {
                var result = room.EscapeFromRoom(player);
                if (room.Players.Count == 0)
                    GemeCore.Rooms.ToList().Remove(room);

                return result;
            }
            return false;
        }
    }
}
