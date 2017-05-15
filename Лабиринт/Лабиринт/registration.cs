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

        private void zaregistrirovtsy_Click(object sender, EventArgs e)
        {
            string lastname = Convert.ToString(textBox1.Text);
            string firstname = Convert.ToString(textBox2.Text);
            string otchestvo = Convert.ToString(textBox3.Text);
            string password = Convert.ToString(textBox4.Text);
            string logins = lastname + firstname;
            if (radioButton1.Checked)
                { int type = 1; }
            else { int type = 0; }
            const string databaseName = @"C:\Users\Ariy\Documents\Visual Studio 2015\Projects\labirinth\GL\labirinth.db";
            SQLiteConnection connection =
                new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO 'Accounts' ('lastname', 'firstname', 'otchestvo', 'password', 'login', 'type') VALUES (lastname, firstname, otchestvo, password, logins, type);", connection);
            command.ExecuteNonQuery();
            connection.Close();                 
        }
    }
}
