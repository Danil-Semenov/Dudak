using Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public abstract class BaseRules
    {
        public bool IsCanBeat(Card whoIsBeaten, Card whoBeats, eFamily trump)
        {
            if (whoIsBeaten.Family != trump && whoBeats.Family == trump)
                return true;
            if (whoIsBeaten.Family == whoBeats.Family && whoIsBeaten.Value < whoBeats.Value)
                return true;
            return false;
        }

        public abstract IEnumerable<Card> GetDeck();

        public IEnumerable<Card> GetRandomCards(int count, Card[] deck)
        {
            var uniqueIndices = Randomizer.RandomNumbers(deck.Length).Distinct().Take(count);
            return uniqueIndices.Select(i => deck[i]).ToArray();
        }

        // TO-DO проверить
        public IEnumerable<Card> GetCards(int count, Card[] deck)
        {
            return deck.Take(count);
        }

        public eFamily GetTrump()
        {
            return (eFamily)Randomizer.RandomInt(1, 4);
        }

        public IEnumerable<Card> RandomSort(Card[] data)
        {
            for (int i = data.Length - 1; i >= 1; i--)
            {
                int j = Randomizer.RandomNumber(i + 1);
                // обменять значения data[j] и data[i]
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }
            return data;
        }
    }
}
