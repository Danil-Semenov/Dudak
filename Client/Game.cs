using Card;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters;
using static Client.Game;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Newtonsoft.Json;
using Client.DTO;

namespace Client
{
    public partial class Game : Form
    {
        protected GameCore game = null;//Сама игра (либо создаётся, либо подключается)

        protected Gamer gamer = null;//Игрок клиента

        private const int _cardWidth = 80;//Ширина карты

        private const int _cardHeight = 100;//Высота карты

        private List<Bitmap> _backsImages = new List<Bitmap>();//Рубашки

        private Dictionary<string, Bitmap> _cardsImages = new Dictionary<string, Bitmap>();//Лица карт
                                                                                           //Изображения мастей для отображения козыря
        public Dictionary<string, bool> Cards { get; set; }//Колода карт изначальная

        public GameState CurrGameState { get; set; }//Текущее состояние игры

        public GameConfig CurrGameConfig { get; set; }//Параметры игры, настраиваются при создании игры

        public event RenewGameHandler RenewGame;//Событие обновления состояния игры

        public static int NumberOfInstance;

        private Bitmap[] _suitsImages =
        {
            Pictures.Diamonds,
            Pictures.Clubs,
            Pictures.Hearts,
            Pictures.Spides
        };

        public int Id { get; set; }

        public Game()
        {
            InitializeComponent();
            //Считываем рубашки (пока одна)
            _backsImages.Add(Pictures.back);
            DeckBack.Image = _backsImages[0];

            DeckBack.BringToFront();
            DeckBack.BringToFront();

            //Считываем лица карт
            Array suitValues = Enum.GetValues(typeof(Card.eFamily));
            Array rankValues = Enum.GetValues(typeof(Card.eValue));

            for (int s = 0; s < suitValues.Length; s++)
            {
                for (int r = 0; r < rankValues.Length; r++)
                {
                    eFamily currSuit = (eFamily)suitValues.GetValue(s);
                    eValue currRank = (eValue)rankValues.GetValue(r);

                    string cardName = Enum.GetName(typeof(eFamily), currSuit) + "_" + Enum.GetName(typeof(eValue), currRank);

                    _cardsImages.Add(cardName, (Bitmap)Pictures.ResourceManager.GetObject(cardName));
                }
            }

            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void startServerToolStripMenuItem_Click_1()
        {
            //Ищем свободный канал
            int channelPort = 8001;
            bool IsChannelRegistered = true;
            while (IsChannelRegistered)
            {
                try
                {
                    // Создаем канал, который будет слушать порт
                    ChannelServices.RegisterChannel(CreateChannel(channelPort, Dns.GetHostName()), false);
                    IsChannelRegistered = false;
                }
                catch
                {
                    channelPort++;
                }
            }

            var uriService = $"http://danilsemenov-001-site1.itempurl.com/api/v1/game/{User.RoomId}/add";
            var resultService = "";
            var request = $"?channel={Dns.GetHostName()}&port={channelPort}";
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(uriService + request).GetAwaiter().GetResult();
                resultService = result.Content.ReadAsStringAsync().Result;
            }

            // Создаем объект-игру
            game = new GameCore();

            // Предоставляем объект-игру для вызова с других компьютеров
            RemotingServices.Marshal(game, "GameObject");

            // Входим в игру
            game.RenewGame += OnRenewGame;
            gamer = new Gamer(game, User.Login);

            game.Connect(gamer);

            newGameToolStripMenuItem.Enabled = true;//Создать игру может только сервер
        }

        TcpChannel CreateChannel(int port, string name)
        {
            BinaryServerFormatterSinkProvider sp = new
                BinaryServerFormatterSinkProvider();
            sp.TypeFilterLevel = TypeFilterLevel.Full; // Разрешаем передачу делегатов

            BinaryClientFormatterSinkProvider cp = new
                BinaryClientFormatterSinkProvider();

            IDictionary props = new Hashtable();
            props["port"] = port;
            props["name"] = name;

            return new TcpChannel(props, cp, sp);
        }

        public void ConnectToServer(int room)
        {
            string serverName = "";
            int port = 0;
            
            var uriService = $"http://danilsemenov-001-site1.itempurl.com/api/v1/game/{room}";
            var resultService = "";
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(uriService ).GetAwaiter().GetResult();
                resultService = result.Content.ReadAsStringAsync().Result;
            }
            var resultString = JObject.Parse(resultService);
            if (resultString!=null)
            {
                var server = JsonConvert.DeserializeObject<Server>(resultString["result"].ToString());
                serverName = server.Host;
                port = server.Port;
            }
            else
            {
                MessageBox.Show(@"Извините, ошибка подключения к игре!");
                return;
            }
            // Создаем канал, который будет подключен к серверу
            TcpChannel tcpChannel = CreateChannel(0, "tcpDurak");
            ChannelServices.RegisterChannel(tcpChannel, false);

            // Получаем ссылку на объект-игру. расположенную на
            // другом компьютере (или в другом процессе)
            game = (GameCore)Activator.GetObject(typeof(GameCore),
                  String.Format("tcp://{0}:{1}/GameObject", serverName, port));

            if (game.PlayerCount == 6)
            {
                MessageBox.Show(@"Извините, свободных мест нет!");
                return;
            }
            // Входим в игру
            game.RenewGame += OnRenewGame;
            gamer = new Gamer(game, User.Login);
            game.Connect(gamer);
        }

        public class Gamer : MarshalByRefObject
        {
            protected GameCore Game { get; set; }
            public string Name { get; set; }
            public SortedDictionary<string, Card.Card> Alignment { get; set; }

            public Gamer(GameCore game, string name = "Gamer")
            {
                Game = game;
                Name = name;
                Alignment = new SortedDictionary<string, Card.Card>();
            }

          

            public void AddCard(Card.Card card)
            {
                Alignment.Add(card.ToString(), card);
            }

            public void RemoveCard(Card.Card card)
            {
                Alignment.Remove(card.ToString());
            }

            public void AlignmentClear()
            {
                Alignment.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var uriService = $"http://danilsemenov-001-site1.itempurl.com/api/v1/room/{Id}/escape?player={User.Login}";
            var resultService = "";
            //var request = $"&password={PasswordTextBox.Text}";
            var isOk = true;
            var authentication = GetToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", authentication);
                var result = httpClient.GetAsync(uriService).GetAwaiter().GetResult();
                resultService = result.Content.ReadAsStringAsync().Result;

                isOk = result.StatusCode == System.Net.HttpStatusCode.OK;

            }
            if (isOk)
            {
                var resultString = JObject.Parse(resultService);
                if ((bool)resultString["result"])
                {
                    Close();
                    Hide();
                    var home = new Home();
                    home.Show();
                }
                else
                {
                    Ups("Не получается выйти из комнаты(");
                }
            }
            else
            {
                Ups("Попробуй в другой раз!");
            }
        }

        public void Distribute(int gameState)
        {
            if (PlayerCount < 2) return;

            if (gameState == 0) //Начало игры
            {
                foreach (Gamer gamer in CurrGameState.Gamers)
                {
                    gamer.AlignmentClear();
                }

                //Козырь
                int trumpCardIndex = PlayerCount * 6 - 1;
                CurrGameState.TrumpSuit = CurrGameState.Deck[trumpCardIndex].Suit;
                Card.Card trumpCard = CurrGameState.Deck[trumpCardIndex];
                CurrGameState.Deck.RemoveAt(trumpCardIndex);
                CurrGameState.Deck.Add(trumpCard); //Последняя карта колоды - козырь, выложить её под колоду лицом вверх

                for (int i = 0; i < 6; i++)
                {
                    for (int k = 0; k < PlayerCount; k++)
                    {
                        CurrGameState.Gamers[k].AddCard(CurrGameState.Deck[0]);
                        CurrGameState.Deck.RemoveAt(0);
                    }
                }

                //Назначаем заходящего в начале игры
                if (CurrGameState.Attacker == null)
                {
                    eValue min = eValue.Ace;
                    foreach (Gamer gamer in CurrGameState.Gamers)
                    {
                        foreach (var card in gamer.Alignment)
                        {
                            if (card.Value.Suit == CurrGameState.TrumpSuit
                                && card.Value.Rank < min)
                            {
                                CurrGameState.Attacker = gamer;
                                int attackerIndex = CurrGameState.Gamers.IndexOf(gamer);
                                //Назначаем отбивающего
                                int defenderIndex = (attackerIndex + 1) % PlayerCount;

                                CurrGameState.Defender = CurrGameState.Gamers[defenderIndex];
                                min = card.Value.Rank;
                            }
                        }
                        if (min == eValue.Six) break; //Чуть ускорим
                    }
                    //Если козырей ни у кого нет
                    if (CurrGameState.Attacker == null)
                    {
                        CurrGameState.Attacker = CurrGameState.Gamers[0];
                        CurrGameState.Defender = CurrGameState.Gamers[1];
                    }
                }
            }
            else
            {
                for (int k = 0; k < PlayerCount; k++)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (CurrGameState.Deck.Count == 0)
                            break;
                        if (CurrGameState.Gamers[k].Alignment.Count >= 6)
                        {
                            continue;
                        }
                        CurrGameState.Gamers[k].AddCard(CurrGameState.Deck[0]);
                        CurrGameState.Deck.RemoveAt(0);
                    }
                    if (CurrGameState.Deck.Count == 0)
                        break;
                }
            }
        }

        public void Check()
        {
            int nGamersWithCards = 0;
            string loserName = String.Empty;
            foreach (Gamer gamer1 in CurrGameState.Gamers)
            {
                if (gamer1.Alignment.Count > 0)
                {
                    loserName = gamer1.Name;
                    nGamersWithCards++;
                }
            }
            if (nGamersWithCards <= 1)
            {
                if (nGamersWithCards == 1)
                {
                    CurrGameState.GameStateMessage =
                      loserName + @" - дурень. Новая игра начнётся через 30 секунд. Если сервер не начнёт её раньше.";
                }
                else if (nGamersWithCards == 0)
                {
                    CurrGameState.GameStateMessage =
                      @"Ничья! Новая игра начнётся через 30 секунд. Если сервер не начнёт её раньше.";
                }
                CurrGameState.GameRun = 2;
            }
        }

        public int PlayerCount
        {
            get
            {
                return CurrGameState.Gamers.Count;
            }
        }

        public void Deal()
        {
            if (CurrGameState.Deck.Count > 0)
            {
                CurrGameState.Deck.Clear();
            }

            string[] keyStrings = Cards.Keys.ToArray();
            foreach (string key in keyStrings)
            {
                Cards[key] = false;
            }

            Array suitValues = Enum.GetValues(typeof(eFamily));
            Array rankValues = Enum.GetValues(typeof(eValue));

            string cardName;
            eFamily currSuit;
            eValue currRank;

            for (int i = 0; i < Cards.Count; i++)
            {
                do
                {
                    currSuit =
                      (eFamily)suitValues.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, suitValues.Length));
                    currRank =
                      (eValue)rankValues.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, rankValues.Length));
                    cardName = Enum.GetName(typeof(eFamily), currSuit) + "_" + Enum.GetName(typeof(eValue), currRank);
                } while (Cards[cardName]);

                CurrGameState.Deck.Add(new Card.Card(
                currSuit,
                currRank
                ));

                Cards[cardName] = true;
            }
        }

        public void Connect(Gamer p)
        {
            CurrGameState.Gamers.Add(p);
            ShowAllGamers(CurrGameState);
        }

        public void Disconnect(Gamer p)
        {
            CurrGameState.Gamers.Remove(p);
            ShowAllGamers(CurrGameState);
        }

        public void ShowAllGamers(GameState gameState)
        {
            if (RenewGame != null)
                RenewGame(gameState);
        }

        void imageCard_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox imageCard = (sender as PictureBox);

            string suitNRank = imageCard.Tag.ToString();

            eFamily cardSuit;
            Enum.TryParse(suitNRank.Substring(0, suitNRank.IndexOf('_')), false, out cardSuit);

            eValue rankCard;
            Enum.TryParse(suitNRank.Substring(suitNRank.IndexOf('_') + 1), false, out rankCard);

            if (gamer.Name == game.CurrGameState.Defender.Name)
            {
                for (int i = 0; i < game.CurrGameState.BoutCardsAttack.Count; i++)
                {
                    eFamily curSuits = game.CurrGameState.BoutCardsAttack[i].Suit;
                    eValue curRanks = game.CurrGameState.BoutCardsAttack[i].Rank;

                    if (((cardSuit == curSuits && rankCard > curRanks)
                      || cardSuit == game.CurrGameState.TrumpSuit && curSuits != game.CurrGameState.TrumpSuit))
                    {
                        Card.Card stepCard = gamer.Alignment[suitNRank];
                        gamer.RemoveCard(stepCard);
                        game.CurrGameState.AddDefendCardToGameField(stepCard);

                        Card.Card cardAttack = game.CurrGameState.BoutCardsAttack[i];
                        game.CurrGameState.RemoveAttackCardToGameField(cardAttack);
                        //game.CurrGameState.AddAttackDefendedCardToGameField(cardAttack);
                        break;
                    }
                }

                if (game.CurrGameState.Defender.Alignment.Count == 0)
                {
                    game.Check();//Получаем результат игры
                    NewRound();
                    return;
                }
            }
            else if (gamer.Name != game.CurrGameState.Defender.Name)
            {
                if (game.CurrGameState.Defender.Alignment.Count == game.CurrGameState.GetCountAttackCardsOnGameField())
                    return;
                bool rightAddCard = false;

                if (game.CurrGameState.GetCountCardsOnGameField() == 0)
                {
                    rightAddCard = true;
                }
                else
                {
                    foreach (Card.Card card in game.CurrGameState.GetAllCardsOnGameField())
                    {
                        if (card.Rank == rankCard)
                        {
                            rightAddCard = true;
                            break;
                        }
                    }
                }

                if (rightAddCard)
                {
                    Card.Card stepCard = gamer.Alignment[suitNRank];
                    gamer.RemoveCard(stepCard);
                    game.CurrGameState.AddAttackCardToGameField(stepCard);
                }
            }

            game.Check();//Получаем результат игры

            game.ShowAllGamers(game.CurrGameState);
        }

        public void OnRenewGame(GameState gameState)
        {
            for (int i = 1; i < gameState.Gamers.Count; i++)
            {
                BeginInvoke(new Action<int>(num =>
                {
                    (Controls["Gamer" + num + "Zone"] as GroupBox).Controls.Clear();
                }), i);
            }
            BeginInvoke(new Action(() =>
            {
                IamZone.Controls.Clear();
                GameField.Controls.Clear();
            }));

            if (gameState.Gamers.Count > 1)
            {
                int i = 1;
                foreach (Gamer currGamer in gameState.Gamers)
                {
                    if (currGamer.Name == gamer.Name)//Оформляем зону пользователя (внизу окна)
                    {
                        //Надпись имени и статуса игрока
                        if (gameState.Attacker != null && currGamer.Name == gameState.Attacker.Name)
                        {
                            BeginInvoke(new Action<string>(n =>
                            {
                                IamZone.Text = n + @" - Ходите";
                                gameStateTb.Text = @"Ходите";
                            }), gamer.Name);

                        }
                        else if (gameState.Defender != null && currGamer.Name == gameState.Defender.Name)
                        {
                            BeginInvoke(new Action<string>(n =>
                            {
                                IamZone.Text = n + @" - Отбивайтесь";
                                gameStateTb.Text = @"Отбивайтесь";
                            }), gamer.Name);
                        }
                        else
                        {
                            BeginInvoke(new Action<string>(n =>
                            {
                                IamZone.Text = n;
                                gameStateTb.Text = @"Можете подбрасывать";
                            }), gamer.Name);
                        }

                        //Раскладываем карты в зоне игрока на форме
                        if (currGamer.Alignment.Count > 0)
                        {
                            IAsyncResult iar = BeginInvoke(new Func<int>(() => IamZone.Width));

                            int iamZoneWidth;

                            if (iar.IsCompleted)
                            {
                                iamZoneWidth = (int)EndInvoke(iar);
                            }
                            else
                            {
                                iamZoneWidth = IamZone.Width;
                            }

                            int cardLeft = 10;
                            int step = (iamZoneWidth - _cardWidth) / currGamer.Alignment.Count;
                            foreach (var card in currGamer.Alignment) //Раскладываем карты в зоне игрока на форме
                            {
                                PictureBox imageCard = new PictureBox();
                                imageCard.Image = _cardsImages[card.Key];
                                imageCard.Height = _cardHeight;
                                imageCard.Width = _cardWidth;
                                imageCard.SizeMode = PictureBoxSizeMode.StretchImage;
                                imageCard.Location = new Point(cardLeft, 20);
                                imageCard.Tag = card.Value.Suit + "_" + card.Value.Rank;
                                imageCard.MouseDown += imageCard_MouseDown;
                                cardLeft += step;
                                BeginInvoke(new Action<PictureBox>(img => IamZone.Controls.Add(img)), imageCard);
                            }
                        }
                    }
                    else
                    {
                        if (i != 3)//Оформляем зоны всех игроков, кроме третьего (они по бокам)
                        {
                            if (gameState.Attacker != null && currGamer.Name == gameState.Attacker.Name)
                            {
                                BeginInvoke(new Action<int, string>((num, name) =>
                                {
                                    (Controls["Gamer" + num + "Zone"] as GroupBox).Text = name + @" - Ходит";
                                }), i, currGamer.Name);
                            }
                            else if (gameState.Defender != null && currGamer.Name == gameState.Defender.Name)
                            {
                                BeginInvoke(new Action<int, string>((num, name) =>
                                {
                                    (Controls["Gamer" + num + "Zone"] as GroupBox).Text = name + @" - Отбивается";
                                }), i, currGamer.Name);
                            }
                            else
                            {
                                BeginInvoke(new Action<int, string>((num, name) =>
                                {
                                    (Controls["Gamer" + num + "Zone"] as GroupBox).Text = name;
                                }), i, currGamer.Name);
                            }

                            if (currGamer.Alignment.Count > 0)
                            {
                                IAsyncResult iar = BeginInvoke(new Func<int, int>(
                                    num => (Controls["Gamer" + num + "Zone"] as GroupBox).Height),
                                  i);

                                int gbHeight;
                                if (iar.IsCompleted)
                                {
                                    gbHeight = (int)EndInvoke(iar);
                                }
                                else
                                {
                                    gbHeight = (Controls["Gamer" + i + "Zone"] as GroupBox).Height;
                                }

                                int cardTop = 20;
                                int step = (gbHeight - _cardHeight) / currGamer.Alignment.Count;
                                foreach (var card in currGamer.Alignment)
                                {
                                    PictureBox imageCard = new PictureBox();
                                    imageCard.Image = _backsImages[0];
                                    imageCard.Height = _cardHeight;
                                    imageCard.Width = _cardWidth;
                                    imageCard.Location = new Point(20, cardTop);
                                    imageCard.SizeMode = PictureBoxSizeMode.StretchImage;
                                    imageCard.Tag = card.ToString();
                                    cardTop += step;
                                    BeginInvoke(
                                      new Action<int, PictureBox>((num, picb) =>
                                      {
                                          (Controls["Gamer" + num + "Zone"] as GroupBox).Controls.Add(picb);
                                      }), i, imageCard);
                                }
                            }
                        }
                        else
                        {//Оформляем зону третьего игрока (она вверху)
                            string gbName = "Gamer" + i + "Zone";

                            if (gameState.Attacker != null && currGamer.Name == gameState.Attacker.Name)
                            {
                                BeginInvoke(new Action<string, string>((name, gbname) =>
                                {
                                    (Controls[gbname] as GroupBox).Text = name + @" - Ходит";
                                }), currGamer.Name, gbName);
                            }
                            else if (gameState.Defender != null && currGamer.Name == gameState.Defender.Name)
                            {
                                BeginInvoke(new Action<string, string>((name, gbname) =>
                                {
                                    (Controls[gbname] as GroupBox).Text = name + @" - Отбивается";
                                }), currGamer.Name, gbName);
                            }
                            else
                            {
                                BeginInvoke(new Action<string, string>((name, gbname) =>
                                {
                                    (Controls[gbname] as GroupBox).Text = name;
                                }), currGamer.Name, gbName);
                            }

                            if (currGamer.Alignment.Count > 0)
                            {
                                IAsyncResult iarWidth =
                                  BeginInvoke(new Func<string, int>(gbname => (Controls[gbname] as GroupBox).Width), gbName);

                                int gbWidth;
                                if (iarWidth.IsCompleted)
                                {
                                    gbWidth = (int)EndInvoke(iarWidth);
                                }
                                else
                                {
                                    gbWidth = (Controls[gbName] as GroupBox).Width;
                                }

                                int cardLeft = 10;
                                int step = (gbWidth - _cardWidth) / currGamer.Alignment.Count;
                                foreach (var card in currGamer.Alignment) //Раскладываем карты в зоне игрока на форме
                                {
                                    PictureBox imageCard = new PictureBox();
                                    imageCard.Image = _backsImages[0];
                                    imageCard.Height = _cardHeight;
                                    imageCard.Width = _cardWidth;
                                    imageCard.Location = new Point(cardLeft, 20);
                                    imageCard.SizeMode = PictureBoxSizeMode.StretchImage;
                                    imageCard.Tag = card.Value.Suit + "_" + card.Value.Rank;
                                    cardLeft += step;
                                    BeginInvoke(
                                      new Action<int, PictureBox>((num, picb) =>
                                      {
                                          (Controls["Gamer" + num + "Zone"] as GroupBox).Controls.Add(picb);
                                      }), i, imageCard);
                                }
                            }
                        }
                        i++;
                    }
                }

                //Показываем колоду и козырь
                if (gameState.GameRun == 0)
                {
                    if (gameState.Deck.Count != 0)
                    {
                        BeginInvoke(new Action<Bitmap, Bitmap>((pic, trump) =>
                        {
                            TrumpCard.Visible = true;
                            DeckBack.Visible = true;
                            TrumpCard.Image = pic;
                            TrumpImage.Image = trump;
                            TrumpImage.SendToBack();
                        }),
                          _cardsImages[gameState.Deck[gameState.Deck.Count - 1].ToString()],
                          _suitsImages[(int)gameState.TrumpSuit]);
                    }
                    else
                    {
                        BeginInvoke(new Action<Bitmap>(trump =>
                        {
                            TrumpCard.Visible = false;
                            DeckBack.Visible = false;
                            TrumpImage.Image = trump;
                            TrumpImage.BringToFront();
                        }),
                        _suitsImages[(int)gameState.TrumpSuit]);
                    }
                }

                //Скрываем колоду, когда остаётся только одна карта - козырная
                if (gameState.Deck.Count == 1)
                {
                    if (DeckZone.Controls.Count > 1)
                    {
                        BeginInvoke(new Action(() => DeckBack.Visible = false));
                    }
                }

                //Скрываем колоду и козырь, когда в колоде не осталось карт
                if (gameState.GameRun == 1 && gameState.Deck.Count == 0)
                {
                    if (DeckZone.Controls.Count > 1)
                    {
                        BeginInvoke(new Action(() => TrumpCard.Visible = false));
                        BeginInvoke(new Action(() => DeckBack.Visible = false));
                    }
                }

                //Раскладываем карты атаки на игровом столе
                //Битые карты
                int offset = 0;
                foreach (Card.Card card in gameState.BoutCardsAttackDefended)
                {
                    PictureBox imageStepCard = new PictureBox();
                    imageStepCard.Image = _cardsImages[card.ToString()];
                    imageStepCard.Height = _cardHeight;
                    imageStepCard.Width = _cardWidth;

                    if (offset <= 4)
                    {
                        imageStepCard.Location = new Point((offset * (_cardWidth + 10) + 10), 20);
                    }
                    else
                    {
                        imageStepCard.Location = new Point(((offset - 5) * (_cardWidth + 10) + 10), _cardHeight + 40);
                    }

                    imageStepCard.SizeMode = PictureBoxSizeMode.StretchImage;
                    BeginInvoke(new Action<PictureBox>(pic => GameField.Controls.Add(pic)), imageStepCard);
                    offset++;
                }

                //Небитые карты
                foreach (Card.Card card in gameState.BoutCardsAttack)
                {
                    PictureBox imageStepCard = new PictureBox();
                    imageStepCard.Image = _cardsImages[card.ToString()];
                    imageStepCard.Height = _cardHeight;
                    imageStepCard.Width = _cardWidth;

                    if (offset <= 4)
                    {
                        imageStepCard.Location = new Point((offset * (_cardWidth + 10) + 10), 20);
                    }
                    else
                    {
                        imageStepCard.Location = new Point(((offset - 5) * (_cardWidth + 10) + 10), _cardHeight + 40);
                    }

                    imageStepCard.SizeMode = PictureBoxSizeMode.StretchImage;
                    BeginInvoke(new Action<PictureBox>(pic => GameField.Controls.Add(pic)), imageStepCard);
                    offset++;
                }

                //Раскладываем карты защиты на игровом столе
                offset = 0;
                foreach (Card.Card card in gameState.BoutCardsDefend)
                {
                    PictureBox imageStepCard = new PictureBox();
                    imageStepCard.Image = _cardsImages[card.ToString()];
                    imageStepCard.Height = _cardHeight;
                    imageStepCard.Width = _cardWidth;

                    if (offset <= 4)
                    {
                        imageStepCard.Location = new Point(offset * (_cardWidth + 10) + 20, 40);
                    }
                    else
                    {
                        imageStepCard.Location = new Point((offset - 5) * (_cardWidth + 10) + 20, _cardHeight + 60);
                    }


                    imageStepCard.SizeMode = PictureBoxSizeMode.StretchImage;
                    BeginInvoke(new Action<PictureBox>(pic =>
                    {
                        GameField.Controls.Add(pic);
                        GameField.Controls[GameField.Controls.Count - 1].BringToFront();
                    }), imageStepCard);
                    offset++;
                }
            }

            //Регулируем кнопки "Беру" и "Отбой"
            if (gameState.Attacker != null)
            {
                if (gameState.GetCountCardsOnGameField() != 0
                  && gameState.GetCountAttackCardsOnGameField() == 0)
                {
                    if (gamer.Name == gameState.Attacker.Name)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            takeCardsBtn.Enabled = false;
                            endRoundBtn.Enabled = true;
                        }));
                    }
                    if (gamer.Name == gameState.Defender.Name)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            takeCardsBtn.Enabled = true;
                            endRoundBtn.Enabled = false;
                        }));
                    }
                }
                else if (gameState.GetCountCardsOnGameField() != 0
                  && gameState.GetCountAttackCardsOnGameField() != 0)
                {
                    if (gamer.Name == gameState.Attacker.Name)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            takeCardsBtn.Enabled = false;
                            endRoundBtn.Enabled = true;
                        }));
                    }
                    if (gamer.Name == gameState.Defender.Name)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            takeCardsBtn.Enabled = true;
                            endRoundBtn.Enabled = false;
                        }));
                    }
                }
                else
                {
                    BeginInvoke(new Action(() =>
                    {
                        takeCardsBtn.Enabled = false;
                        endRoundBtn.Enabled = false;
                    }));
                }
            }

            if (game.CurrGameState.GameRun == 2)
            {
                BeginInvoke(new Action<string>(n => gameStateTb.Text = n), game.CurrGameState.GameStateMessage);

                if (!RemotingServices.IsTransparentProxy(game)) // Мы на сервере
                {
                    new Thread(() =>
                    {
                        Thread.Sleep(30000);
                        if (game.CurrGameState.GameRun == 2)
                        {
                            NewGame();
                        }
                    }).Start();
                }
            }
        }

        private void Ups(string message, string caption = "Ошибка.", MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private string GetToken()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{User.Login}:{User.Password}");
            string val = Convert.ToBase64String(plainTextBytes);
            return "Basic " + val;
        }

        [Serializable]
        public class GameState : MarshalByRefObject
        {
            public List<Game.Gamer> Gamers = new List<Game.Gamer>();//Игроки

            public Game.Gamer Attacker { get; set; }//Чей ход

            public Game.Gamer Defender { get; set; }//Кто отбивается

            public List<Card.Card> BoutCardsAttack = new List<Card.Card>();//Карты захода
            public List<Card.Card> BoutCardsDefend = new List<Card.Card>();//Карты защиты
            public List<Card.Card> BoutCardsAttackDefended = new List<Card.Card>();//Карты отбитые

            public eFamily TrumpSuit { get; set; }//Козырь

            public List<Card.Card> Deck = new List<Card.Card>();//Колода игры

            public int GameRun { get; set; }//Новая игра = 0, расдача = 1, игра идёт = 2 

            public string GameStateMessage { get; set; }



            public Game.Gamer GetDefender()
            {
                return Defender;
            }

            public void AddAttackCardToGameField(Card.Card card)
            {
                BoutCardsAttack.Add(card);
            }

            public void RemoveAttackCardToGameField(Card.Card card)
            {
                BoutCardsAttack.Remove(card);
            }

            public void InsertAttackCardToGameField(int i, Card.Card card)
            {
                BoutCardsAttack.Insert(i, card);
            }

            public void AddDefendCardToGameField(Card.Card card)
            {
                BoutCardsDefend.Add(card);
            }

            public void AddAttackDefendedCardToGameField(Card.Card card)
            {
                BoutCardsAttackDefended.Add(card);
            }

            public int GetCountCardsOnGameField()
            {
                return BoutCardsAttack.Count + BoutCardsDefend.Count + BoutCardsAttackDefended.Count;
            }

            public int GetCountAttackCardsOnGameField()
            {
                return BoutCardsAttack.Count;
            }

            public int GetCountAttackDefendedCardsOnGameField()
            {
                return BoutCardsAttackDefended.Count;
            }

            public int GetCountDefendCardsOnGameField()
            {
                return BoutCardsDefend.Count;
            }

            public List<Card.Card> GetAllCardsOnGameField()
            {
                List<Card.Card> allCards = new List<Card.Card>();
                allCards.AddRange(BoutCardsAttack);
                allCards.AddRange(BoutCardsDefend);
                allCards.AddRange(BoutCardsAttackDefended);
                return allCards;
            }

            public void BoutCardsClear()
            {
                BoutCardsAttack.Clear();
                BoutCardsDefend.Clear();
                BoutCardsAttackDefended.Clear();
            }
        }

        private void takeCardsBtn_Click(object sender, EventArgs e)
        {
            foreach (Card.Card card in game.CurrGameState.GetAllCardsOnGameField())
            {
                gamer.AddCard(card);
            }

            game.CurrGameState.BoutCardsClear();

            game.Distribute(1);

            //Назначаем заходящего
            int index = game.CurrGameState.Gamers.IndexOf(game.CurrGameState.Defender);
            do
            {
                index = (index + 1) % game.PlayerCount;
            } while (game.CurrGameState.Gamers[index].Alignment.Count == 0);
            game.CurrGameState.Attacker = game.CurrGameState.Gamers[index];

            //Назначаем отбивающего
            do
            {
                index = (index + 1) % game.PlayerCount;
            } while (game.CurrGameState.Gamers[index].Alignment.Count == 0);
            game.CurrGameState.Defender = game.CurrGameState.Gamers[index];

            game.Check();//Получаем результат игры

            game.ShowAllGamers(game.CurrGameState);
        }

        private void endRoundBtn_Click(object sender, EventArgs e)
        {
            NewRound();
        }

        void NewRound()
        {
            game.CurrGameState.BoutCardsClear();
            game.Distribute(1);

            //Назначаем заходящего
            int index = (game.CurrGameState.Gamers.IndexOf(game.CurrGameState.Defender) - 1) % game.PlayerCount;
            do
            {
                index = (index + 1) % game.PlayerCount;
            } while (game.CurrGameState.Gamers[index].Alignment.Count == 0);
            game.CurrGameState.Attacker = game.CurrGameState.Gamers[index];

            //Назначаем отбивающего
            index = game.CurrGameState.Gamers.IndexOf(game.CurrGameState.Attacker);
            do
            {
                index = (index + 1) % game.PlayerCount;
            } while (game.CurrGameState.Gamers[index].Alignment.Count == 0);
            game.CurrGameState.Defender = game.CurrGameState.Gamers[index];

            game.Check();//Получаем результат игры

            game.ShowAllGamers(game.CurrGameState);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        void NewGame()
        {
            game.CurrGameState.Attacker = null;
            game.CurrGameState.Defender = null;
            game.CurrGameState.BoutCardsClear();
            game.Deal();
            game.Distribute(0);

            game.CurrGameState.GameRun = 0;
            game.ShowAllGamers(game.CurrGameState);
            game.CurrGameState.GameRun = 1;
        }

      
    }

    public delegate void RenewGameHandler(GameState gameState);

    public struct GameConfig
    {
        public int CardsNumberIn1BoutRestriction { get; set; }//Ограничение на количество карт в круге


        internal void SetCardsNumberIn1BoutRestriction(int p)
        {
            CardsNumberIn1BoutRestriction = p;
        }
    }

    public class GameCore : MarshalByRefObject
    {
        public Dictionary<string, bool> Cards { get; set; }//Колода карт изначальная

        public GameState CurrGameState { get; set; }//Текущее состояние игры

        public GameConfig CurrGameConfig { get; set; }//Параметры игры, настраиваются при создании игры

        public event RenewGameHandler RenewGame;//Событие обновления состояния игры

        public static int NumberOfInstance;

        public GameCore()
        {
            NumberOfInstance++;

            Cards = new Dictionary<string, bool>();
            CurrGameState = new GameState();
            CurrGameConfig = new GameConfig();

            //Конфигурирование игры (д.б. в отдельном окне)
            CurrGameConfig.SetCardsNumberIn1BoutRestriction(6);

            //Формирование колоды
            Array suitValues = Enum.GetValues(typeof(eFamily));
            Array rankValues = Enum.GetValues(typeof(eValue));

            for (int s = 0; s < suitValues.Length; s++)
            {
                for (int r = 0; r < rankValues.Length; r++)
                {
                    eFamily currSuit = (eFamily)suitValues.GetValue(s);
                    eValue currRank = (eValue)rankValues.GetValue(r);

                    string cardName = Enum.GetName(typeof(eFamily), currSuit) + "_" + Enum.GetName(typeof(eValue), currRank);

                    Cards.Add(cardName, false);
                }
            }
        }

        public override object InitializeLifetimeService()
        {
            ILease il = (ILease)base.InitializeLifetimeService();
            il.InitialLeaseTime = TimeSpan.FromDays(1);
            il.RenewOnCallTime = TimeSpan.FromSeconds(10);
            return il;
        }

        public void Connect(Gamer p)
        {
            CurrGameState.Gamers.Add(p);
            ShowAllGamers(CurrGameState);
        }

        public void Disconnect(Gamer p)
        {
            CurrGameState.Gamers.Remove(p);
            ShowAllGamers(CurrGameState);
        }

        public void ShowAllGamers(GameState gameState)
        {
            if (RenewGame != null)
                RenewGame(gameState);
        }

        public void Deal()
        {
            if (CurrGameState.Deck.Count > 0)
            {
                CurrGameState.Deck.Clear();
            }

            string[] keyStrings = Cards.Keys.ToArray();
            foreach (string key in keyStrings)
            {
                Cards[key] = false;
            }

            Array suitValues = Enum.GetValues(typeof(eFamily));
            Array rankValues = Enum.GetValues(typeof(eValue));

            string cardName;
            eFamily currSuit;
            eValue currRank;

            for (int i = 0; i < Cards.Count; i++)
            {
                do
                {
                    currSuit =
                      (eFamily)suitValues.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, suitValues.Length));
                    currRank =
                      (eValue)rankValues.GetValue(new Random((int)DateTime.Now.Ticks).Next(0, rankValues.Length));
                    cardName = Enum.GetName(typeof(eFamily), currSuit) + "_" + Enum.GetName(typeof(eValue), currRank);
                } while (Cards[cardName]);

                CurrGameState.Deck.Add(new Card.Card(
                currSuit,
                currRank
                ));

                Cards[cardName] = true;
            }
        }

        public void Distribute(int gameState)
        {
            if (PlayerCount < 2) return;

            if (gameState == 0) //Начало игры
            {
                foreach (Gamer gamer in CurrGameState.Gamers)
                {
                    gamer.AlignmentClear();
                }

                //Козырь
                int trumpCardIndex = PlayerCount * 6 - 1;
                CurrGameState.TrumpSuit = CurrGameState.Deck[trumpCardIndex].Suit;
                Card.Card trumpCard = CurrGameState.Deck[trumpCardIndex];
                CurrGameState.Deck.RemoveAt(trumpCardIndex);
                CurrGameState.Deck.Add(trumpCard); //Последняя карта колоды - козырь, выложить её под колоду лицом вверх

                for (int i = 0; i < 6; i++)
                {
                    for (int k = 0; k < PlayerCount; k++)
                    {
                        CurrGameState.Gamers[k].AddCard(CurrGameState.Deck[0]);
                        CurrGameState.Deck.RemoveAt(0);
                    }
                }

                //Назначаем заходящего в начале игры
                if (CurrGameState.Attacker == null)
                {
                    var min = eValue.Ace;
                    foreach (Gamer gamer in CurrGameState.Gamers)
                    {
                        foreach (var card in gamer.Alignment)
                        {
                            if (card.Value.Suit == CurrGameState.TrumpSuit
                                && card.Value.Rank < min)
                            {
                                CurrGameState.Attacker = gamer;
                                int attackerIndex = CurrGameState.Gamers.IndexOf(gamer);
                                //Назначаем отбивающего
                                int defenderIndex = (attackerIndex + 1) % PlayerCount;

                                CurrGameState.Defender = CurrGameState.Gamers[defenderIndex];
                                min = card.Value.Rank;
                            }
                        }
                        if (min == eValue.Six) break; //Чуть ускорим
                    }
                    //Если козырей ни у кого нет
                    if (CurrGameState.Attacker == null)
                    {
                        CurrGameState.Attacker = CurrGameState.Gamers[0];
                        CurrGameState.Defender = CurrGameState.Gamers[1];
                    }
                }
            }
            else
            {
                for (int k = 0; k < PlayerCount; k++)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (CurrGameState.Deck.Count == 0)
                            break;
                        if (CurrGameState.Gamers[k].Alignment.Count >= 6)
                        {
                            continue;
                        }
                        CurrGameState.Gamers[k].AddCard(CurrGameState.Deck[0]);
                        CurrGameState.Deck.RemoveAt(0);
                    }
                    if (CurrGameState.Deck.Count == 0)
                        break;
                }
            }
        }

        public void Check()
        {
            int nGamersWithCards = 0;
            string loserName = String.Empty;
            foreach (Gamer gamer1 in CurrGameState.Gamers)
            {
                if (gamer1.Alignment.Count > 0)
                {
                    loserName = gamer1.Name;
                    nGamersWithCards++;
                }
            }
            if (nGamersWithCards <= 1)
            {
                if (nGamersWithCards == 1)
                {
                    CurrGameState.GameStateMessage =
                      loserName + @" - дурень. Новая игра начнётся через 30 секунд. Если сервер не начнёт её раньше.";
                }
                else if (nGamersWithCards == 0)
                {
                    CurrGameState.GameStateMessage =
                      @"Ничья! Новая игра начнётся через 30 секунд. Если сервер не начнёт её раньше.";
                }
                CurrGameState.GameRun = 2;
            }
        }

        public int PlayerCount
        {
            get
            {
                return CurrGameState.Gamers.Count;
            }
        }
    }


}




