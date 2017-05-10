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
    public partial class Labyrinth : Form
    {
        int time;
        bool manypartion;
        MazeSolver m_Maze;
        MazeGenerator mz;
        Account account;
        int[,] m_iMaze;
        int m_iSize = 10;
        int m_iRowDimensions = 0; //16
        int m_iColDimensions = 0; //20
        int iSelectedX, iSelectedY;

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
            mz = new MazeGenerator(m_iColDimensions, m_iRowDimensions);
            int[,] mzmatrix = mz.Gener();
            m_Maze = new MazeSolver(mzmatrix);
            pictureBox1.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);
            pictureBox1.Location = new Point((660-(m_iColDimensions * m_iSize + 3))/2, ((538-(m_iRowDimensions * m_iSize + 3))/2)+5);
            m_iMaze = m_Maze.GetMaze;
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
                }
            //print ball
            //myGraphics.FillEllipse(new SolidBrush(Color.DarkCyan), this.iSelectedX * m_iSize + 5, this.iSelectedY * m_iSize + 5, m_iSize - 10, m_iSize - 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mz = new MazeGenerator(m_iColDimensions, m_iRowDimensions);
            int[,] mzmatrix = mz.Gener();
            m_Maze = new MazeSolver(mzmatrix);
            m_iMaze = m_Maze.GetMaze;
            this.Refresh();
        }

        public Labyrinth(Account account, int height, int width, int size , bool manypartion, int time)
        {
            this.account = account;
            this.time = time;
            this.manypartion = manypartion;
            m_iRowDimensions = height;
            m_iColDimensions = width;
            m_iSize = size;
            InitializeComponent();
        }
    }
}
