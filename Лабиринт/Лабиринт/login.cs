using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SQLite;
using static Лабиринт.SQLHelper;

namespace Лабиринт
{
    public partial class login : Form
    {

        Account account;
        bool access = false;
        public login()
        {
            InitializeComponent();
        }

        public login(Account account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            RegistrationForm registration = new RegistrationForm(this);
            registration.ShowDialog();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = Convert.ToString(textBox1.Text);
            string password = Convert.ToString(textBox2.Text);
            int resultcode = Authorize(login, password);
            switch (resultcode)
            {
                case 1:
                    MessageBox.Show("К сожалению, такого логина не существует! Пожалуйста, проверьте поле логина.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 2:
                    MessageBox.Show("К сожалению, пароль неверен! Пожалуйста, проверьте поле пароля.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 10:
                    access = true;
                    account.UpdateUserInfo(resultcode, true);
                    account.Visible = true;
                    this.Close();
                    break;
                default:
                    access = true;
                    account.UpdateUserInfo(resultcode,false);
                    account.Visible = true;
                    this.Close();
                    break;
            }
            
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!access)
            {
                Application.Exit();
            }
        }
    }
}
