using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using Лабиринт;
using System.Text.RegularExpressions;
using System.Text;

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
            if (SQLHelper.CheckTeacherRegistration())
            {
                radioButton1.Enabled = false;
            }
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Visible = true;
        }

        private void zaregistrirovtsy_Click(object sender, EventArgs e)
        {
            string login = "";
            bool check=false;
            bool exit = false;
            int isteacher=0;
            string familiya = Convert.ToString(textBox1.Text);
            string imya = Convert.ToString(textBox2.Text);
            string otchestvo = Convert.ToString(textBox3.Text);
            string password = Convert.ToString(textBox4.Text);
            if (string.IsNullOrEmpty(familiya) || Regex.IsMatch(familiya, @"[^а-яА-Я]") || familiya.Length < 3 || familiya.Length > 30)
            {
                label7.Visible = true;
                exit = true;
            }
            else
            {
                label7.Visible = false;
            }
            if (string.IsNullOrEmpty(imya) || Regex.IsMatch(imya, @"[^а-яА-Я]") || imya.Length < 3 || imya.Length > 30)
            {
                label8.Visible = true;
                exit = true;
            }
            else
            {
                label8.Visible = false;
            }
            if (string.IsNullOrEmpty(otchestvo) || Regex.IsMatch(otchestvo, @"[^а-яА-Я]") || otchestvo.Length < 3 || otchestvo.Length > 30)
            {
                label9.Visible = true;
                exit = true;
            }
            else
            {
                label9.Visible = false;
            }
            if (string.IsNullOrEmpty(password) || Regex.IsMatch(password, @"[^a-zA-Z\d]") || password.Length < 6 || password.Length > 30)
            {
                label10.Visible = true;
                exit = true;
            }
            else
            {
                label10.Visible = false;
            }
            if (exit)
            {
                return;
            }
            familiya = familiya.Substring(0, 1).ToUpper() + familiya.Remove(0, 1);
            imya = imya.Substring(0, 1).ToUpper() + imya.Remove(0, 1);
            otchestvo = otchestvo.Substring(0, 1).ToUpper() + otchestvo.Remove(0, 1);

            Translit translit = new Translit();
            string loginfamiliya = translit.GetTranslit(familiya.Substring(0, 1).ToLower() + familiya.Remove(0, 1));
            if (radioButton1.Checked)
                { isteacher = 1; }
            try
            {
                login = SQLHelper.AddAccount(loginfamiliya, password, familiya, imya, otchestvo, isteacher);
                check = true;
            }
            catch (Exception)
            {
                check = false;
            }
            if (check)
            {
                MessageBox.Show(string.Format("Регистрация успешна! Ваш логин: {0}", login), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
            else
            {
                MessageBox.Show("Неизвестная ошибка при добавлении нового пользователя!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.UseSystemPasswordChar = true;
            }
            else
            {
                textBox4.UseSystemPasswordChar = false;
            }
        }
    }
}
