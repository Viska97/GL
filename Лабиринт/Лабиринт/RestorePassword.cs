using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Лабиринт;

namespace Лабиринт
{
    public partial class RestorePassword : Form
    {
        login login;
        public RestorePassword()
        {
            InitializeComponent();
        }

        public RestorePassword(login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            Teacher teacher = SQLHelper.GetTecherData();
            if (string.IsNullOrEmpty(email) || Regex.IsMatch(email, @"[а-яА-Я]")  || email.Length < 3 || email.Length > 30 || SQLHelper.CheckTeacherRegistration() == false)
            {
                MessageBox.Show("Email введен в неверном формате", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show(
                "Вы дейтвительно хотите отправить запрос для восстановления пароля учителя?",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {

                    try
                    {
                        GoogleForm.PostToFormTest(teacher.familiya, teacher.imya, teacher.otchestvo, teacher.login, teacher.password, email);
                        MessageBox.Show("Запрос успешно отправлен!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Enabled = false;
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Неизвестная ошибка при отправке запроса!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            login.Visible = true;
            this.Close();
        }
    }
}
