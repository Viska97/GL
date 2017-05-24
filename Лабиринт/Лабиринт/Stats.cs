using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Лабиринт;

namespace Лабиринт
{
    public partial class Stats : Form
    {
        Account account;
        List<ListViewItem> results;
        List<Student> students;
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

        private void Stats_Load(object sender, EventArgs e)
        {
            students = SQLHelper.GetAccounts();
            foreach (Student student in students)
            {
                comboBox1.Items.Add(student.fio);
            }
            label2.Visible = false;
            label3.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            label2.Visible = true;
            label3.Visible = true;
            label2.Text = "Логин: " + students[comboBox1.SelectedIndex].login;
            label3.Text = "Пароль: " + students[comboBox1.SelectedIndex].password;
            listView1.Items.Clear();
            results = SQLHelper.GetResults(students[comboBox1.SelectedIndex].id);
            foreach (ListViewItem lvi in results)
            {
                listView1.Items.Add(lvi);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                DialogResult result = MessageBox.Show(
                "Вы действительно хотите удалить результаты ученика " + students[comboBox1.SelectedIndex].fio + "? Это действие нельзя будет отменить!",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SQLHelper.DeleteResults(students[comboBox1.SelectedIndex].id);
                    listView1.Items.Clear();
                    MessageBox.Show(
                    "Результаты успешно удалены.",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show(
                    "Нет результатов для удаления!",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            
        }
    }
}
