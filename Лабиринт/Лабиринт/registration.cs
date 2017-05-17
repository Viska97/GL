using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using static Лабиринт.SQLHelper;

namespace Лабиринт
{
    public partial class RegistrationForm : Form
    {
               login login;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        public RegistrationForm(login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            login.Visible = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (CheckTeacherRegistration())
            {
                radioButton1.Enabled = false;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Visible = true;
        }

        private void zaregistrirovtsy_Click(object sender, EventArgs e)
        {
            int isteacher=0;
            string familiya = Convert.ToString(textBox1.Text);
            string imya = Convert.ToString(textBox2.Text);
            string otchestvo = Convert.ToString(textBox3.Text);
            string password = Convert.ToString(textBox4.Text);
            string login = Convert.ToString(textBox1.Text) + Convert.ToString(textBox2.Text);
            if (radioButton1.Checked)
                { isteacher = 1; }
            bool check = AddAccount(login, password, familiya, imya, otchestvo, isteacher);
            if (check)
            {
                MessageBox.Show("Регистрация успешна!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
            else
            {
                MessageBox.Show("Неизвестная ошибка при добавлении нового пользователя!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
