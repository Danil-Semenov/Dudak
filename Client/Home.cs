using DB.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            NameLabel.Text = $"Добро пожаловать, {User.Login}!";
            FillDataGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FillDataGrid()
        {
            string resultService = "";
            var uriService = "http://danilsemenov-001-site1.itempurl.com/api/v1/room/all";
            var isOk = true;
            var inParams = new { cards = 0};
            var authentication = GetToken();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", authentication);
                using (var content = new StringContent(JsonConvert.SerializeObject(inParams), Encoding.UTF8, "application/json"))
                {
                    var result = httpClient.PostAsync(uriService, content).GetAwaiter().GetResult();
                    resultService = result.Content.ReadAsStringAsync().Result;
                    isOk = result.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            if (isOk)
            {
                dataGridView1.Rows.Clear();
                var resultString = JObject.Parse(resultService);
                var rooms = JsonConvert.DeserializeObject<List<RoomDto>>(resultString["result"].ToString());
                foreach (var room in rooms)
                {
                    var row = new DataGridViewRow();
                    var id = new DataGridViewTextBoxCell() { Value = room.Id };
                    var name = new DataGridViewTextBoxCell() { Value = room.Name };
                    var host = new DataGridViewTextBoxCell() { Value = room.Host };
                    var max = new DataGridViewTextBoxCell() { Value = room.MaxPlayers };
                    var player = new DataGridViewTextBoxCell() { Value = room.PlayersCount };
                    var isclose = new DataGridViewCheckBoxCell() { Value = !room.IsOpen };
                    var rules = new DataGridViewTextBoxCell() { Value = "Стандартные правила" };
                    row.Cells.AddRange(name, host, max,player,isclose,rules, id);
                    dataGridView1.Rows.Add(row);
                }
            }
        }

        private string GetToken()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{User.Login}:{User.Password}");
            string val = Convert.ToBase64String(plainTextBytes);
            return "Basic " + val;
        }

        //Обновить
        private void button1_Click(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        //Создать комнату
        private void button2_Click(object sender, EventArgs e)
        {
            var room = new CreateRoom();
            if (room.ShowDialog(this) == DialogResult.OK)
            {
                var maxplayers = room.LoginTextBox.Text;
                var password = room.PasswordTextBox.Text;

                var uriService = $"http://danilsemenov-001-site1.itempurl.com/api/v1/room/create";
                var resultService = "";
                //var request = $"&password={PasswordTextBox.Text}";
                var isOk = true;
                var authentication = GetToken();
                var inParams = new RoomDto() { Host = User.Login, MaxPlayers =Convert.ToInt32(maxplayers),Password = string.IsNullOrEmpty(password)? null : password };
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authentication);
                    using (var content = new StringContent(JsonConvert.SerializeObject(inParams), Encoding.UTF8, "application/json"))
                    {
                        var result = httpClient.PostAsync(uriService, content).GetAwaiter().GetResult();
                        resultService = result.Content.ReadAsStringAsync().Result;
                        isOk = result.StatusCode == System.Net.HttpStatusCode.OK;
                    }
                }
                if (isOk)
                {
                    var resultString = JObject.Parse(resultService);
                    var id = Convert.ToInt32(resultString["result"]);
                    Hide();
                    var game = new Game();
                    game.Show();
                }
                else
                {
                    Ups("Попробуй в другой раз!");
                }
            }
            
            room.Dispose();
        }

        //Найти
        private void button3_Click(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //очистить
        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;

        }

        //Войти
        private void button5_Click(object sender, EventArgs e)
        {
            ComeRoom();
        }

        private void ComeRoom(int id = -1)
        {
            DataGridViewRow selectedRows = null;
            var localid = id;
            if (id == -1)
            {
                selectedRows = dataGridView1.SelectedRows[0];
                if (selectedRows == null )
                {
                    Ups("Выбери строку!");
                    return;
                }
                localid = Convert.ToInt32(dataGridView1[6, selectedRows.Index].Value);
            }
            else
            {
                localid = id;
            }

            var uriService = $"http://danilsemenov-001-site1.itempurl.com/api/v1/room/{localid}/come?player={User.Login}";
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

                    Hide();
                    var game = new Game();
                    game.Show();
                }
                else
                {
                    Ups("Не получается зайти в комнату(");
                }
            }
            else
            {
                Ups("Попробуй в другой раз!");
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
    }
}
