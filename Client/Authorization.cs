using DB.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;

namespace Client
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                Error.Text = @"Не заполнено обязательное поле.
  Проверьте данные.";
                return;
            }
            var uriService = "http://danilsemenov-001-site1.itempurl.com/api/v1/user/сheck";
            var resultService = "";
            var request = $"?login={LoginTextBox.Text}&password={PasswordTextBox.Text}";
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(uriService + request).GetAwaiter().GetResult();
                resultService = result.Content.ReadAsStringAsync().Result;
            }
            var resultString = JObject.Parse(resultService);
            if ((bool) resultString["result"])
            {
                User.Login = LoginTextBox.Text;
                User.Password = PasswordTextBox.Text;
                Hide();
                var home = new Home();
                home.Show();
            }
            else
            {
                Error.Text = @"       Ошибка данных.
       Проверьте данные.";
            }
        }

        private void Guest_Click(object sender, EventArgs e)
        {
            var uriService = "http://danilsemenov-001-site1.itempurl.com/api/v1/user/guest";
            var resultService = "";
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(uriService).GetAwaiter().GetResult();
                resultService = result.Content.ReadAsStringAsync().Result;
            }
            var resultString = JObject.Parse(resultService);
            var user = JsonConvert.DeserializeObject<UserDto>(resultString["result"].ToString());
            User.Login = user.Login;
            User.Password = user.Password;
            Hide();
            var home = new Home();
            home.Show();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var registration = new Registration();
            registration.Show();
        }
    }
}
