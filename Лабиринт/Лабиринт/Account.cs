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
            bool firstrun = CheckTeacherRegistration();
            login login = new login(this, !firstrun);
            login.ShowDialog();
            this.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] parameters = GetProfileParameters(comboBox1.SelectedIndex);
            this.Visible = false;
            Labyrinth labyrinth = new Labyrinth(this, Convert.ToInt32(parameters[3]), Convert.ToInt32(parameters[1]), Convert.ToInt32(parameters[2]), parameters[0], Convert.ToInt32(parameters[4]), Convert.ToInt32(parameters[5]), familiya, imya, otchestvo, isteacher, id);
            labyrinth.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Account_Enter(object sender, EventArgs e)
        {
            
        }

        private void Account_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            Stats stats = new Stats(this);
            stats.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            CheckDatabase();
            List<string> presetsnames = GetProfilesNames();
            foreach (string presetname in presetsnames)
            {
                comboBox1.Items.Add(presetname);
            }
            bool firstrun = CheckTeacherRegistration();
            this.Visible = false;
            login login = new login(this, !firstrun);
            login.ShowDialog();
        }

        public void UpdateUserInfo(int id)
        {
            this.id = id;
            string[] credentials = GetCredentials(id);
            familiya = credentials[0];
            imya = credentials[1];
            otchestvo = credentials[2];
            if (credentials[3] == "True")
            {
                isteacher = true;
            }
            else
            {
                isteacher = false;
            }
            label1.Text = familiya + " " + imya.Substring(0, 1) + "." + otchestvo.Substring(0, 1) + ".";
            if (isteacher)
            {
                izmenit_parametry.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                izmenit_parametry.Enabled = false;
                button1.Enabled = false;
            }
            comboBox1.SelectedIndex = 0;
        }
        
    }
}
