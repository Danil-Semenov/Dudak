using Core.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Player
{
    public class Player
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<Card> CardsInHand { get; set; }

        public int PositionOnTable { get; set; }
    }
}
