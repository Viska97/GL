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
    public partial class Account : Form
    {
        
        public Account()
        {
            InitializeComponent();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            options options = new options(this);
            options.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Account_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            login login = new login(this);
            login.ShowDialog();
            this.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Labyrinth labyrinth = new Labyrinth(this, 15, 0, true, 0, 20 , "Иванов", "Иван", "Иванович", true);
            labyrinth.ShowDialog();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            login login = new login(this);
            login.ShowDialog();
        }
    }
}
