﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Лабиринт
{
    public partial class Labyrinth : Form
    {
        int time;
        bool manypartion;
        MazeSolver m_Maze;
        MazeGenerator mz;
        Account account;
        Maze mz2;
        int[,] m_iMaze;
        int m_iSize = 10;
        int m_iRowDimensions = 0; //16
        int m_iColDimensions = 0; //20
        int height = 0;
        int width = 0;
        int method = 0;
        int startY;
        int endY, endX;
        int StudentCount = 1;
        public readonly string Imya, Familiya, Otchestvo;

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
            mz2 = new Maze(100, 100);
            mz2.Generate(height, width,method);
            int[,] mzmatrix2 = mz2.Getmaze(false);
            //mz = new MazeGenerator(m_iColDimensions, m_iRowDimensions, manypartion);
            //int[,] mzmatrix = mz.Gener();
            m_Maze = new MazeSolver(mzmatrix2);
            pictureBox1.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);
            pictureBox1.Location = new Point((660-(m_iColDimensions * m_iSize + 3))/2, ((538-(m_iRowDimensions * m_iSize + 3))/2)+5);
            m_iMaze = m_Maze.GetMaze;
            CheckStartAndEnd();

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
            //print ball
            //myGraphics.FillEllipse(new SolidBrush(Color.DarkCyan), this.iSelectedX * m_iSize + 5, this.iSelectedY * m_iSize + 5, m_iSize - 10, m_iSize - 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mz2 = new Maze(100, 100);
            mz2.Generate(height, width, method);
            int[,] mzmatrix2 = mz2.Getmaze(false);
            //mz = new MazeGenerator(m_iColDimensions, m_iRowDimensions, manypartion);
            //int[,] mzmatrix = mz.Gener();
            m_Maze = new MazeSolver(mzmatrix2);
            m_iMaze = m_Maze.GetMaze;
            this.Refresh();
            CheckStartAndEnd();
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
                        LoadResults(true);
                        this.Close();
                    }
                    
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if ((m_iMaze[iY, iX] == 3 || m_iMaze[iY, iX] == 2) && (iX > 0))
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

        public Labyrinth(Account account, int size ,int method ,bool manypartion, int time, string Familiya, string Imya, string Otchestvo)
        {
            this.account = account;
            this.time = time;
            this.manypartion = manypartion;
            m_iRowDimensions = size;
            m_iColDimensions = size;
            this.height = (size - 1) / 2;
            this.width = (size - 1) / 2;
            this.method = method;
            this.Imya = Imya;
            this.Familiya = Familiya;
            this.Otchestvo = Otchestvo;
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadResults(false);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadResults(false);
        }

        private void LoadResults(bool ExitFind)
        {
            this.Visible = false;
            int[,] OptimalMaze = mz2.Getmaze(true);
            int[,] testmat = OptimalMaze;
            results results = new results(this, m_iMaze, OptimalMaze, endY, endX, startY, m_iRowDimensions, m_iColDimensions, m_iSize, StudentCount, ExitFind);
            results.ShowDialog();
        }
    }
}
