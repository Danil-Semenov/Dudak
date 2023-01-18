using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                Ups("Обязательное поле не заполнено!");
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
