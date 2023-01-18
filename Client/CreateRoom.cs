using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class CreateRoom : Form
    {
        public CreateRoom()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                Ups("Обязательное поле не заполнено!");
                return;
            }
            var players =Convert.ToInt32(LoginTextBox.Text);
            if (players>6 || players<2)
            {
                Ups("Кол-во игроков может быть от 2 до 6!");
                return;
            }
            DialogResult = DialogResult.OK;
            this.Close();
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
