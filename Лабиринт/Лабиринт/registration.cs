using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Visible = true;
        }
    }
}
