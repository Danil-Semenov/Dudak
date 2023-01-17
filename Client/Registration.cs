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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Client
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                Ups(@"Не заполнено обязательное поле. Проверьте данные.");
                return;
                
            }
            string resultService = "";
            var uriService = "http://danilsemenov-001-site1.itempurl.com/api/v1/user/create";
            var isOk = true;
            var inParams = new { login = LoginTextBox.Text, password = PasswordTextBox.Text };
            using (var httpClient = new HttpClient())
            {
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
                if ((bool)resultString["result"])
                {
                    User.Login = LoginTextBox.Text;
                    User.Password = PasswordTextBox.Text;
                    Ups(message:"Поздравляю с регистрацией!", caption:"Ура!");
                    Hide();
                    var home = new Home();
                    home.Show();
                }
                else
                {
                    Ups("Попробуй еще раз, но с другими данными!");
                }
            }
            else
            {
                Ups("Попробуй в другой раз!");
            }

        }
        private void Ups(string message,string caption = "Ошибка.", MessageBoxButtons buttons = MessageBoxButtons.OK)
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
