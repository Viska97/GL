using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Лабиринт.SQLHelper;

namespace Лабиринт
{
    public partial class Account : Form
    {

        string imya, familiya, otchestvo;
        int id;
        bool isteacher;

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
            Labyrinth labyrinth = new Labyrinth(this, 15, 0, 0, "Лабиринт 1", 0, 20 , familiya, imya, otchestvo, isteacher);
            labyrinth.ShowDialog();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            CheckDatabase();
            this.Visible = false;
            login login = new login(this);
            login.ShowDialog();
        }

        public void UpdateUserInfo(int id, bool isteacher)
        {
            this.id = id;
            this.isteacher = isteacher;
            string[] credentials = GetCredentials(id);
            familiya = credentials[0];
            imya = credentials[1];
            otchestvo = credentials[2];
            label1.Text = familiya + " " + imya.Substring(0, 1) + "." + otchestvo.Substring(0, 1) + ".";
            if (isteacher)
            {
                izmenit_parametry.Enabled = true;
            }
            else
            {
                izmenit_parametry.Enabled = false;
            }
        }
        
    }
}
