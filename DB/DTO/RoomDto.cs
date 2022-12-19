using System;
using System.Collections.Generic;
using System.Text;

namespace DB.DTO
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public int Players { get; set; }
        public string Password { get; set; }
        public bool IsOpen { get; set; }
        public int Admissions { get; set; }
        public RulesDto Rules { get; set; }

    }
}
