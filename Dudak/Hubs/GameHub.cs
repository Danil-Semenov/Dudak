using Application;
using Application.BaseEntity;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Durak.Hubs
{
    public class GameHub : Hub
    {

        public async Task Move(string entityString)
        {
            var entity = JsonConvert.DeserializeObject<Entity>(entityString);
            var result = false;
            var room = GemeCore.Rooms.First(r => r.Id == entity.IdGame);
            if (room == null)
            {
                await Clients.Caller.SendAsync("Result", result);
                return;
            }
            switch (entity.Action)
            {
                case "move":
                    var castomFieldMove = (CastomField)entity.CastomField;
                    result = room.SetMove(castomFieldMove.card, entity.PlayerName);
                    break;
                case "beat":
                    var castomFieldBeat = (CastomField)entity.CastomField;
                    result = room.SetBeat(castomFieldBeat.position.Value, castomFieldBeat.card, entity.PlayerName);
                    break;
                case "take":
                    result = room.TakeCard();
                    break;
                case "beatAllCard":
                    result = room.BeatCard();
                    break;
            }

            await Clients.Caller.SendAsync("Result", result);

            if (result)
            {
                var resultTable = new TableEntity() 
                {
                    IdGame= entity.IdGame,
                    Trump = room.Game.Trump,
                    IndexPlayerBeat = room.Game.IndexPlayerBeat.Value,
                    IndexPlayerMove = room.Game.IndexPlayerMove.Value,
                    CardsOnDeck = room.Game.Cards.Count(),
                    CardOnTable = room.Game.Positions,
                    CardOnHand = room.Players.Select( pl => new CardOnHand() { PlayerName = pl.Name, Cards = pl.CardsInHand.ToList()}).ToList()
                };
                await Clients.All.SendAsync("Table", resultTable);
            }
        }
    }
}
