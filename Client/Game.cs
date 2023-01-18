using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Game : Form
    {
        public int Id { get; set; }

        public Game()
        {
            InitializeComponent();
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
    }
}
