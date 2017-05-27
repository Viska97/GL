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
    public partial class results : Form
    {
        Labyrinth labyrinth;
        MazeSolver m_Maze;
        int m_iRowDimensions = 0; //16
        int m_iColDimensions = 0; //20
        int m_iSize = 10;
        int[,] StudentMaze;
        int[,] OptimalMaze;
        int[,] m_iMaze;
        int iY, iX;
        int iSelectedX=0, iSelectedY;
        int OptimalCount = 0, StudentCount;
        bool ExitFind;
        string Result;
        string StatResult;
        string Familiya, Imya, Otchestvo;
        int id;
        int minutes, seconds;

        public results()
        {
            InitializeComponent();
        }

        public results(Labyrinth labyrinth, int[,] StudentMaze, int[,] OptimalMaze, int iY, int iX, int iSelectedY, int m_iRowDimensions, int m_iColDimensions, int m_iSize, int StudentCount, bool ExitFind, string Familiya, string Imya, string Otchestvo, int id, int minutes, int seconds)
        {
            this.labyrinth = labyrinth;
            this.StudentMaze = StudentMaze;
            this.m_iColDimensions = m_iColDimensions;
            this.m_iRowDimensions = m_iRowDimensions;
            this.m_iSize = (int) ((double)m_iSize/ 1.5);
            this.OptimalMaze = OptimalMaze;
            this.iY = iY;
            this.iX = iX;
            this.iSelectedY = iSelectedY;
            this.StudentCount = StudentCount;
            this.ExitFind = ExitFind;
            this.Imya = Imya;
            this.Familiya = Familiya;
            this.Otchestvo = Otchestvo;
            this.minutes = minutes;
            this.seconds = seconds;
            this.id = id;
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void results_FormClosed(object sender, FormClosedEventArgs e)
        {
            labyrinth.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    if (StudentMaze[i, j] == 1)
                        myGraphics.FillRectangle(new SolidBrush(Color.DarkGray), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    //print path
                    if (StudentMaze[i, j] == 100)
                        myGraphics.FillRectangle(new SolidBrush(Color.Cyan), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    if (StudentMaze[i, j] == 2)
                        myGraphics.FillRectangle(new SolidBrush(Color.Green), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    if (StudentMaze[i, j] == 3)
                        myGraphics.FillRectangle(new SolidBrush(Color.Green), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    if (StudentMaze[i, j] == 4)
                        myGraphics.FillRectangle(new SolidBrush(Color.LightGreen), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                    if (StudentMaze[i, j] == 5)
                        myGraphics.FillRectangle(new SolidBrush(Color.Blue), j * m_iSize + 1, i * m_iSize + 1, m_iSize - 1, m_iSize - 1);
                }
        }

        private void results_Load(object sender, EventArgs e)
        {
            m_Maze = new MazeSolver(OptimalMaze);
            m_iMaze = m_Maze.GetMaze;
            pictureBox1.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);
            pictureBox1.Location = new Point((370 - (m_iColDimensions * m_iSize + 3)) / 2, ((370 - (m_iRowDimensions * m_iSize + 3)) / 2) + 5);
            pictureBox2.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);
            pictureBox2.Location = new Point((370 - (m_iColDimensions * m_iSize + 3)) / 2, ((370 - (m_iRowDimensions * m_iSize + 3)) / 2) + 5);
            int[,] iSolvedMaze = m_Maze.FindPath(iSelectedY, iSelectedX, iY, iX);
            if (iSolvedMaze != null)
            {
                m_iMaze = iSolvedMaze;
            }
            pictureBox2.Refresh();
            for (int i = 0; i < m_iRowDimensions; i++)
            {
                for (int j = 0; j < m_iColDimensions; j++)
                {
                    if (m_iMaze[i, j] == 100)
                    {
                        OptimalCount++;
                    }
                }
            }
            label4.Text = "Кол-во клеток:" + Convert.ToString(StudentCount);
            label5.Text = "Кол-во клеток:" + Convert.ToString(OptimalCount);
            if (ExitFind && (StudentCount==OptimalCount))
            {
                Result = "Лабиринт пройден! Ваш путь оптимален!";
                StatResult = "Путь оптимален";
            }
            if (ExitFind && (StudentCount != OptimalCount))
            {
                Result = "Лабиринт пройден, но ваш путь не оптимален.";
                StatResult = "Путь не оптимален";
            }
            if (!ExitFind)
            {
                if (labyrinth.GiveUp == true)
                {
                    Result = "Вы сдались.";
                    StatResult = "Ученик сдался";
                }
                else
                {
                    Result = "Лабиринт не пройден!";
                    StatResult = "Время закончилось";
                }
            }
            label6.Text = Result;
            label2.Text = Familiya + " " + Imya + " " + Otchestvo;
            UpdateTimerText();
            if (!labyrinth.IsTeacher)
            {
                SQLHelper.AddResult(id, labyrinth.method, labyrinth.style, m_iRowDimensions, labyrinth.defminutes, labyrinth.defseconds, OptimalCount, StudentCount, minutes, seconds, StatResult);
            }
            
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
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
                }
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
            label3.Text = "Осталось времени: " + min + ":" + sec;
        }
    }
}
