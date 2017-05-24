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
using Лабиринт;
using System.Text.RegularExpressions;

namespace Лабиринт
{
    public partial class login : Form
    {

        Account account;
        bool access = false;
        bool firstrun = false;
        public login()
        {
            InitializeComponent();
        }

        

        public login(Account account, bool firstrun)
        {
            InitializeComponent();
            this.account = account;
            this.firstrun = firstrun;
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
            bool exit = false;
            string login = Convert.ToString(textBox1.Text);
            string password = Convert.ToString(textBox2.Text);
            if (string.IsNullOrEmpty(login) || Regex.IsMatch(login, @"[^a-zA-Z\d]"))
            {
                label4.Visible = true;
                exit = true;
            }
            else
            {
                label4.Visible = false;
            }
            if (string.IsNullOrEmpty(password) || Regex.IsMatch(password, @"[^a-zA-Z\d]"))
            {
                label5.Visible = true;
                exit = true;
            }
            else
            {
                label5.Visible = false;
            }
            if (exit)
            {
                return;
            }
            int resultcode = SQLHelper.Authorize(login, password);
            switch (resultcode)
            {
                case 1:
                    MessageBox.Show("К сожалению, такого логина не существует! Пожалуйста, проверьте поле логина.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 2:
                    MessageBox.Show("К сожалению, пароль неверен! Пожалуйста, проверьте поле пароля.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                default:
                    access = true;
                    account.UpdateUserInfo(resultcode);
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

        private void login_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            label4.Visible = false;
            if (firstrun)
            {
                DialogResult result = MessageBox.Show("В настоящий момент аккаунт учителя не зарегистрирован. Хотите зарегистрировать его прямо сейчас?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Visible = false;
                    RegistrationForm registration = new RegistrationForm(this);
                    registration.ShowDialog();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            this.Visible = false;
            RestorePassword rp = new RestorePassword(this);
            rp.ShowDialog();
            //MessageBox.Show("Для восстановления пароля отправьте письмо на email: gl-support@yandex.ru с темой 'я забыл пароль'. Мы обязательно свяжемся с вами!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        }
    }
}
