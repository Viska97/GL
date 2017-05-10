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
    public partial class options : Form
    {
        Account account;
        public options()
        {
            InitializeComponent();
        }

        public options(Account account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void options_FormClosed(object sender, FormClosedEventArgs e)
        {
            account.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
