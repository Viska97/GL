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
            students = GetAccounts();
            foreach (Student student in students)
            {
                comboBox1.Items.Add(student.fio);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            listView1.Items.Clear();
            results = GetResults(students[comboBox1.SelectedIndex].id);
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
                    DeleteResults(students[comboBox1.SelectedIndex].id);
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
