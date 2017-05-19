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
    public partial class Labyrinth : Form
    {
        public int style;
        public string labname;
        MazeSolver m_Maze;
        Account account;
        Maze mz;
        int[,] m_iMaze;
        int m_iSize = 10;
        int m_iRowDimensions = 0; 
        int m_iColDimensions = 0; 
        int height = 0;
        int width = 0;
        public int method = 0;
        int startY;
        int endY, endX;
        int StudentCount = 1;
        string Imya, Familiya, Otchestvo;
        int id;
        int minutes, seconds;
        public int defminutes, defseconds;
        bool NoFinish = true;
        public bool IsTeacher;
        public bool GiveUp=false;
        bool protect = true;

        public Labyrinth()
        {
            InitializeComponent();
        }

        private void Labyrinth_FormClosed(object sender, FormClosedEventArgs e)
        {
            account.Visible = true;
        }

        private void Labyrinth_Load(object sender, EventArgs e)
        {
            if (IsTeacher)
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            groupBox1.Text = labname;
            mz = new Maze(100, 100);
            mz.Generate(height, width,method);
            int[,] mzmatrix2 = mz.Getmaze(style, false);
            m_Maze = new MazeSolver(mzmatrix2);
            pictureBox1.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);
            pictureBox1.Location = new Point((660-(m_iColDimensions * m_iSize + 3))/2, ((538-(m_iRowDimensions * m_iSize + 3))/2)+5);
            m_iMaze = m_Maze.GetMaze;
            CheckStartAndEnd();
            if (!IsTeacher)
            {
                AddResult(id, method, style, m_iColDimensions, defminutes, defseconds, 0, StudentCount, minutes, seconds, "Ошибка прохождения");
            }
            UpdateTimerText();
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics myGraphics = e.Graphics;
            for (int i = 0; i < m_iRowDimensions; i++)
                for (int j = 0; j < m_iColDimensions; j++)
                {
                    // print grids
                    myGraphics.DrawRectangle(new Pen(Color.Black), j * m_iSize, i * m_iSize, m_iSize, m_iSize);
                    // print walls
                    if (m_iMaze[i, j] == 1)
                        myGraphics.FillRectangle(new SolidBrush(Color.DarkGray), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    //print path
                    if (m_iMaze[i, j] == 100)
                        myGraphics.FillRectangle(new SolidBrush(Color.Cyan), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    if (m_iMaze[i, j] == 2)
                    
                        myGraphics.FillRectangle(new SolidBrush(Color.Green), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                        
                    
                        

                    if (m_iMaze[i, j] == 3)
                        myGraphics.FillRectangle(new SolidBrush(Color.Green), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    if (m_iMaze[i, j] == 4)
                        myGraphics.FillRectangle(new SolidBrush(Color.LightGreen), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    if (m_iMaze[i, j] == 5)
                        myGraphics.FillRectangle(new SolidBrush(Color.Blue), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            minutes = defminutes;
            seconds = defseconds;
            mz = new Maze(100, 100);
            mz.Generate(height, width, method);
            int[,] mzmatrix2 = mz.Getmaze(style, false);
            m_Maze = new MazeSolver(mzmatrix2);
            m_iMaze = m_Maze.GetMaze;
            this.Refresh();
            CheckStartAndEnd();
            UpdateTimerText();
            timer1.Start();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int iX = e.X / m_iSize;
            int iY = e.Y / m_iSize;
            //m_iMaze = m_Maze.GetMaze;
            if (e.Button == MouseButtons.Left)
            {
                if ((m_iMaze[iY, iX] == 0 || m_iMaze[iY, iX] == 4) && (iX > 0))
                {
                    if (m_iMaze[iY, iX + 1] == 2)
                    {
                        m_iMaze[iY, iX] = 2;
                        m_iMaze[iY, iX + 1] = 3;
                        StudentCount++;

                    }
                    else if (m_iMaze[iY, iX - 1] == 2)
                    {
                        m_iMaze[iY, iX] = 2;
                        m_iMaze[iY, iX - 1] = 3;
                        StudentCount++;
                    }
                    else if (m_iMaze[iY + 1, iX] == 2)
                    {
                        m_iMaze[iY, iX] = 2;
                        m_iMaze[iY + 1, iX] = 3;
                        StudentCount++;
                    }
                    else if (m_iMaze[iY - 1, iX] == 2)
                    {
                        m_iMaze[iY, iX] = 2;
                        m_iMaze[iY - 1, iX] = 3;
                        StudentCount++;
                    }
                }
                if (m_iMaze[iY, iX] == 5)
                {
                    bool checkexitpath = false;
                    if (m_iMaze[iY, iX - 1] == 2)
                    {
                        checkexitpath = true;
                    }
                    else if (m_iMaze[iY + 1, iX] == 2)
                    {
                        checkexitpath = true;
                    }
                    else if (m_iMaze[iY - 1, iX] == 2)
                    {
                        checkexitpath = true;
                    }

                    if (checkexitpath)
                    {
                        m_iMaze[iY, iX] = 2;
                        pictureBox1.Refresh();
                        StudentCount++;
                        NoFinish = false;
                        timer1.Stop();
                        LoadResults(true);
                        protect = false;
                        this.Close();
                    }
                    
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if ((m_iMaze[iY, iX] == 2) && (iX > 0))
                {
                    m_iMaze[iY, iX] = 4;
                    if (m_iMaze[iY, iX + 1] == 3)
                    {
                        m_iMaze[iY, iX + 1] = 2;
                        StudentCount++;
                    }
                    else if (m_iMaze[iY, iX - 1] == 3)
                    {
                        m_iMaze[iY, iX - 1] = 2;
                        StudentCount++;
                    }
                    else if (m_iMaze[iY + 1, iX] == 3)
                    {
                        m_iMaze[iY + 1, iX] = 2;
                        StudentCount++;
                    }
                    else if (m_iMaze[iY - 1, iX] == 3)
                    {
                        m_iMaze[iY - 1, iX] = 2;
                        StudentCount++;
                    }
                }
            }

            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        public Labyrinth(Account account, int size ,int method ,int style, string labname, int minutes, int seconds, string Familiya, string Imya, string Otchestvo, bool IsTeacher, int id)
        {
            this.account = account;
            this.style = style;
            m_iRowDimensions = size;
            m_iColDimensions = size;
            this.height = (size - 1) / 2;
            this.width = (size - 1) / 2;
            this.method = method;
            this.Imya = Imya;
            this.Familiya = Familiya;
            this.Otchestvo = Otchestvo;
            this.minutes = minutes;
            this.seconds = seconds;
            this.defminutes = minutes;
            this.defseconds = seconds;
            this.IsTeacher = IsTeacher;
            this.labname = labname;
            this.id = id;
            m_iSize = (int) 510/size;
            InitializeComponent();
        }

        private void CheckStartAndEnd()
        {
            for (int i = 0; i < m_iRowDimensions; i++)
            {
                for (int j = 0; j < m_iColDimensions; j++)
                {
                    if (m_iMaze[i, j] == 2)
                        startY = i;
                    if (m_iMaze[i, j] == 5)
                    {
                        endY = i;
                        endX = j;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds = seconds - 1;
            if (seconds == -1)
            {
                minutes = minutes - 1;
                seconds = 59;
            }
            if (!IsTeacher)
            {
                UpdateTempResult(id, method, style, m_iColDimensions, defminutes, defseconds, 0, StudentCount, minutes, seconds, "Ошибка прохождения");
            }
            if (minutes == 0 && seconds == 0 && NoFinish)
            {
                timer1.Stop();
                UpdateTimerText();
                MessageBox.Show("Время вышло!","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LoadResults(false);
                protect = false;
                this.Close();
            }
            UpdateTimerText();
        }

        private void Labyrinth_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (protect)
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            seconds = seconds - 1;
            if (seconds == -1)
            {
                minutes = minutes - 1;
                seconds = 59;
            }
            this.Visible = false;
            UpdateTimerText();
            DialogResult result = MessageBox.Show(
                "Лабиринт не пройден! Вы действительно хотите сдаться?",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                timer1.Stop();
                GiveUp = true;
                LoadResults(false);
                protect = false;
                this.Close();
            }
            this.Visible = true;
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            protect = false;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadResults(false);
            timer1.Start();
        }

        private void LoadResults(bool ExitFind)
        {
            this.Visible = false;
            int[,] OptimalMaze = mz.Getmaze(style, true);
            if (!IsTeacher)
            {
                DeleteTempResult();
            }
            results results = new results(this, m_iMaze, OptimalMaze, endY, endX, startY, m_iRowDimensions, m_iColDimensions, m_iSize, StudentCount, ExitFind, Familiya,Imya,Otchestvo, id, minutes, seconds);
            results.ShowDialog();
        }

        private void UpdateTimerText()
        {
            string min, sec;
            if (seconds < 10)
            {
                sec = "0" + Convert.ToString(seconds);
            }
            else
            {
                sec = Convert.ToString(seconds);
            }
            if (minutes < 10)
            {
                min = "0" + Convert.ToString(minutes);
            }
            else
            {
                min = Convert.ToString(minutes);
            }
            label2.Text = "Осталось: " + min + ":" + sec;
        }
    }
}
