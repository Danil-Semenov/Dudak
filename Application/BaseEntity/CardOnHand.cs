using Core.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BaseEntity
{
    public class CardOnHand
    {
        public string PlayerName { get; set; }
        public List<Card> Cards { get; set; }
    }
}
