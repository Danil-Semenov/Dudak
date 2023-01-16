using Core.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BaseEntity
{
    public class TableEntity
    {
        public int IdGame { get; set; }
        public IEnumerable<Position> CardOnTable { get; set; }
        public List<CardOnHand> CardOnHand { get; set; }
        public Card Trump { get; set; }
        public int IndexPlayerMove { get; set; }
        public int IndexPlayerBeat { get; set; }
        public int CardsOnDeck { get; set; }
    }
}
