using System;
using System.Collections.Generic;

namespace Core.Game
{
    public class Game
    {
        public IEnumerable<Card> Cards { get; set; }

        public eFamily Trump { get; set; }

        public IEnumerable<Position> Positions { get; set; }

        public bool IsActive { get; set; }

        public int? IndexPlayerMove { get; set; }

        public int? IndexPlayerBeat { get; set; }

        public bool IsLastPlayerBeat { get; set; }

        public int IndexPlayerLooser { get; set; }
    }
}
