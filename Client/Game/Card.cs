using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Card
{
    [Serializable]
    public class Card
    {
        private eFamily _suit;
        private eValue _rank;
        private bool _visible;

        public override string ToString()
        {
            return Enum.GetName(typeof(eFamily), _suit) + "_" + Enum.GetName(typeof(eValue), _rank);
        }

        public eFamily Suit
        {
            get { return _suit; }
            set { _suit = value; }
        } //масть

        public eValue Rank
        {
            get { return _rank; }
            set { _rank = value; }
        } //Величина

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        } //Рубашкой или лицом вверх

        public Card(eFamily suit, eValue rank, bool visible = false)
        {
            _suit = suit;
            _rank = rank;
            _visible = visible;
        }
    }
}
