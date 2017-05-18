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
    public partial class Stats : Form
    {
        Account account;
        public Stats()
        {
            InitializeComponent();
        }

        public Stats(Account account)
        {
            this.account = account;
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            account.Visible = true;
            this.Close();
        }
    }
}
