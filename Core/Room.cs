using Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Room
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public int MaxPlayers { get; set; }

        public Game.Game Game { get; set; }

        public BaseRules Rules { get; set; }

        public IList<Player.Player> Players { get; set; }

        public Room(BaseRules rules, IList<string> players)
        {
            Rules = rules;
            Players = new List<Player.Player>(players.Count);
            foreach (var player in players)
            {
                Players.Add(new Player.Player()
                {
                    Name = player,
                    IsActive = true,
                    PositionOnTable = players.IndexOf(player)
                });
            }
            Game = new Game.Game();
        }

        private int GetFreePlayerPos()
        {
            for(var i = 0; i < MaxPlayers; i++)
            {
                var player = Players.FirstOrDefault(p=> p.PositionOnTable == i);
                if (player != null)
                {
                    return i;
                }
            }
            new ArgumentOutOfRangeException();
            return default;
        }

        public bool AddPlayer(string player)
        {
            if(Players.Count >= MaxPlayers)
                return false;
            try
            {
                Players.Add(new Player.Player()
                {
                    Name = player,
                    IsActive = true,
                    PositionOnTable = GetFreePlayerPos()
                });
                return true;
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return false;
            }
        }

        public bool EscapeFromRoom(string player)
        {
            try
            {
                if (Game.IsActive)
                {
                    var excapePlayer = Players.First(p => p.Name == player);
                    Players.Remove(Players.First(p => p.Name == player));
                    return Surrender(excapePlayer.PositionOnTable);
                }
                else
                {
                    Players.Remove(Players.First(p => p.Name == player));
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool StartGame()
        {

            Game.Cards = Rules.GetDeck();
            Game.Trump = Rules.GetTrump(Game.Cards);
            Game.Positions = new List<Position>(6);
            foreach(var player in Players)
            {
                player.CardsInHand = Rules.GetCards(6, Game.Cards.ToArray());
                Game.Cards.ToList().RemoveAll(x => player.CardsInHand.Contains(x));
            }
            Game.IndexPlayerMove = WhoMove();
            Game.IndexPlayerBeat = WhoBeat();
            Game.IsActive = true;
            Notify?.Invoke();
            return true;
        }

        public bool SetMove(Card card, string name) //Добавить проверку на то может ли игрок добавлять карты
        {
            var player = Players.FirstOrDefault(pl => pl.IsActive && pl.Name == name);
            if (Rules.IsCanMove(Players, Game.IndexPlayerBeat.Value, player.PositionOnTable))
            {
                var pos = Game.Positions.FirstOrDefault(p => p.WhoIsBeaten == null);
                if (pos != null)
                {
                    pos.WhoIsBeaten = card;
                    player.CardsInHand.ToList().Remove(card);
                    Notify?.Invoke();
                    return true;
                }
            }
            return false;
        }

        public bool SetBeat(int position, Card card, string name)
        {
            var player = Players.FirstOrDefault(pl => pl.IsActive && pl.Name == name);
            var pos = Game.Positions.ToArray()[position];
            if(pos.WhoBeats == null && Rules.IsCanBeat(pos.WhoIsBeaten, card, Game.Trump.Family))
            {
                pos.WhoBeats = card;
                player.CardsInHand.ToList().Remove(card);
                Notify?.Invoke();
                return true;
            }
            return false;
        }

        // Выдать всем игрокам карты
        private void GiveEveryoneCards()
        {
            foreach (var player in Players.Where(p=>p.IsActive))
            {
                var needCard = player.CardsInHand.ToList().Count > 5 ? 0 : 6 - player.CardsInHand.ToList().Count;
                player.CardsInHand.ToList().AddRange(Rules.GetCards(needCard, Game.Cards.ToArray()));
                Game.Cards.ToList().RemoveAll(x => player.CardsInHand.Contains(x));
            }
        }

        // Метод определения чей ход 
        private int WhoMove()
        {
            if (Game.IndexPlayerMove == null && Game.IndexPlayerBeat == null)// начало игры
            {
                return Players.Where(pl => pl.IsActive).Min().PositionOnTable; //Мб случайным образом?
            }
            if ( Game.IndexPlayerBeat != null && Game.IsLastPlayerBeat)// игрок побился
            {
                return Game.IndexPlayerBeat.Value;
            }
            if (Game.IndexPlayerBeat != null && !Game.IsLastPlayerBeat)// игрок взял
            {
                return Game.IndexPlayerBeat.Value < Players.Where(pl => pl.IsActive).Max().PositionOnTable ? Game.IndexPlayerBeat.Value + 1: Players.Where(pl => pl.IsActive).Min().PositionOnTable;
            }
            return default;
        }

        // Метод определения кто бьется 
        private int WhoBeat() => Game.IndexPlayerMove.Value < Players.Where(pl => pl.IsActive).Max().PositionOnTable ? Game.IndexPlayerMove.Value + 1 : Players.Where(pl => pl.IsActive).Min().PositionOnTable;


        // Событие что что-то поменялось + на уровне выше быдет подписчик на это событие который будет уведомлять всех что что-то поменялось.
        // Событие будет возвращать Game, а обработчик будет выдавать каждому игроку то что надо ему
        // Надо решить кому что надо
        delegate void AccountHandler();
        event AccountHandler Notify;

        // Игрок берет карты + дерганье события
        public bool TakeCard()
        {
            var player = Players[Game.IndexPlayerBeat.Value];
            foreach(var pos in Game.Positions)
            {
                if (pos.WhoIsBeaten != null)
                    player.CardsInHand.ToList().Add(pos.WhoIsBeaten);
                if (pos.WhoBeats != null)
                    player.CardsInHand.ToList().Add(pos.WhoBeats);
            }
            GiveEveryoneCards();
            Game.Positions = new List<Position>(6);
            Game.IndexPlayerMove = WhoMove();
            Game.IndexPlayerBeat = WhoBeat();
            Notify?.Invoke();
            return true;
        }

        // БИТО + дерганье события
        public bool BeatCard()
        {
            GiveEveryoneCards();
            Game.Positions = new List<Position>(6);
            Game.IndexPlayerMove = WhoMove();
            Game.IndexPlayerBeat = WhoBeat();
            Notify?.Invoke();
            return true;
        }

        // Конец игры + дерганье события
        public bool EndGame(int looser = -1)
        {
            Game.IndexPlayerLooser = looser == -1 ? Game.IndexPlayerBeat.Value : looser;
            Game.IsActive = false;
            Notify?.Invoke();
            return true;
        }

        // Сдаться
        public bool Surrender(int indexPlayer)
        {
            return EndGame(indexPlayer);
        }
    }
}
