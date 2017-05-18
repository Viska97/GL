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
    public partial class options : Form
    {
        Account account;
        int presetscount = 0;
        bool presetchanged = false;
        bool changingpreset= false;
        int currentpreset;
        string[] parameters;
        bool n1 = false, n2 = false, n3 = false, c2 = false, c3=false;
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
            SetProfileParameters(currentpreset, Convert.ToString(comboBox2.SelectedIndex), Convert.ToString(comboBox3.SelectedIndex), Convert.ToString(numericUpDown1.Value), Convert.ToString(numericUpDown2.Value), Convert.ToString(numericUpDown3.Value));
            presetchanged = false;
            MessageBox.Show(
                "Профиль был успешно сохранен!",
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (n2 == false)
            {
                n2 = true;
            }
            else
            {
                if (numericUpDown2.Value != Convert.ToInt32(parameters[4]))
                {
                    presetchanged = true;
                }
            }

            if (numericUpDown2.Value ==0 && numericUpDown3.Value<10)
            {
                numericUpDown3.Value = 10;
            }
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (c3 == false)
            {
                c3 = true;
            }
            else
            {
                if (comboBox3.SelectedIndex != Convert.ToInt32(parameters[2]))
                {
                    presetchanged = true;
                }
            }
            
        }

        private void options_Load(object sender, EventArgs e)
        {
            
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            groupBox1.Enabled = false;
            List<string> presetsnames = GetProfilesNames();
            foreach (string presetname in presetsnames)
            {
                comboBox1.Items.Add(presetname);
                presetscount++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы действительно хотите удалить все данные пользователей и настройки программы? Это действие нельзя будет отменить!",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                "Программа будет перезапущена для применения изменений.",
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk);
                try
                {
                    ResetDatabase();
                }
                catch (Exception)
                {
                    MessageBox.Show(
                "Неизвестная ошибка при попытке удаления базы данных!",
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (presetchanged)
            {
                DialogResult result = MessageBox.Show(
                "Профиль был изменен. Выйти из настроек без сохранения профиля?",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    return;
                }

            }
            else
            {
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (presetchanged && !changingpreset)
            {
                DialogResult result =  MessageBox.Show(
                "Профиль был изменен. Выйти из профиля без сохранения?",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    changingpreset = true;
                    comboBox1.SelectedIndex = currentpreset;
                    changingpreset = false;
                    return;
                }
                else
                {
                    presetchanged = false;
                }
                }
            if (!changingpreset)
            {
                groupBox1.Enabled = true;
                button1.Enabled = true;
                currentpreset = comboBox1.SelectedIndex;
                parameters = GetProfileParameters(comboBox1.SelectedIndex);
                numericUpDown1.Value = Convert.ToInt32(parameters[3]);
                comboBox2.SelectedIndex = Convert.ToInt32(parameters[1]);
                comboBox3.SelectedIndex = Convert.ToInt32(parameters[2]);
                numericUpDown2.Value = Convert.ToInt32(parameters[4]);
                numericUpDown3.Value = Convert.ToInt32(parameters[5]);
            }
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (n1==false)
            {
                n1 = true;
            }
            else
            {
                if (numericUpDown1.Value != Convert.ToInt32(parameters[3]))
                {
                    presetchanged = true;
                }
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (c2 == false)
            {
                c2 = true;
            }
            else
            {
                if (comboBox2.SelectedIndex != Convert.ToInt32(parameters[1]))
                {
                    presetchanged = true;
                }
            }
            
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (n3 == false)
            {
                n3 = true;
            }
            else
            {
                if (numericUpDown3.Value != Convert.ToInt32(parameters[5]))
                {
                    presetchanged = true;
                }
            }

            if (numericUpDown2.Value == 0 && numericUpDown3.Value < 10)
            {
                numericUpDown3.Value = 10;
            }
            
        }
    }
}
