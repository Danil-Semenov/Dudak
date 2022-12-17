using Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class VanilaRules : BaseRules
    {
        public override IEnumerable<Card> GetDeck()
        {
            var result = new List<Card>(36);
            for (var i = 1; i < 5; i++)
            {
                for (var j = 6; i < 15; i++)
                {
                    result.Add(new Card() 
                    { 
                        Value = (eValue)j,
                        Family = (eFamily)i 
                    });
                }
            }

            return RandomSort(result.ToArray());
        }
    }
}
