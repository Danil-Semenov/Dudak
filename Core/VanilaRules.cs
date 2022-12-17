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

        public override bool IsCanMove(IList<Player.Player> players, int whoBeatPlayerPos, int whoMovePlayerPos)
        {
            var playerMove = players.FirstOrDefault(p => p.PositionOnTable == whoMovePlayerPos && p.IsActive);
            var playerBeat = players.FirstOrDefault(p => p.PositionOnTable == whoMovePlayerPos && p.IsActive);
            if (playerMove != null && playerBeat != null)
            {
                //if (whoMovePlayerPos - whoBeatPlayerPos == 1 || whoBeatPlayerPos - whoMovePlayerPos == 1)
                //    return true;
                //if (players.Where(pl => pl.IsActive).Max().PositionOnTable == whoBeatPlayerPos && players.Where(pl => pl.IsActive).Min().PositionOnTable == whoMovePlayerPos)
                //    return true;
                //if (players.Where(pl => pl.IsActive).Max().PositionOnTable == whoBeatPlayerPos && players.Where(pl => pl.IsActive).ToArray()[^2].PositionOnTable == whoMovePlayerPos)
                //    return true;
                //if (players.Where(pl => pl.IsActive).Min().PositionOnTable == whoBeatPlayerPos && players.Where(pl => pl.IsActive).Max().PositionOnTable == whoMovePlayerPos)
                //    return true;
                //if (players.Where(pl => pl.IsActive).Min().PositionOnTable == whoBeatPlayerPos && players.Where(pl => pl.IsActive).ToArray()[1].PositionOnTable == whoMovePlayerPos)
                //    return true;
                players.ToList().RemoveAll(p => !p.IsActive);
                var playerMoveIndex = players.IndexOf(playerMove);
                var playerBeatIndex = players.IndexOf(playerBeat);
                if (playerMoveIndex - playerBeatIndex == 1 || playerBeatIndex - playerMoveIndex == 1)
                    return true;
                if (playerBeatIndex == 0 && playerMoveIndex == players.Count - 1)
                    return true;
                if (playerMoveIndex == 0 && playerBeatIndex == players.Count - 1)
                    return true;
            }
            return false;
        }
    }
}
