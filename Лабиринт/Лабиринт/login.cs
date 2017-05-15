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
            access = true;
            account.Visible = true;
            this.Close();
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
