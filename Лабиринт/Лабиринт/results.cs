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
    public partial class results : Form
    {
        Labyrinth labyrinth;
        int m_iRowDimensions = 0; //16
        int m_iColDimensions = 0; //20
        int m_iSize = 10;
        int[,] StudentMaze;

        public results()
        {
            InitializeComponent();
        }

        public results(Labyrinth labyrinth, int[,] StudentMaze, int m_iRowDimensions, int m_iColDimensions, int m_iSize)
        {
            this.labyrinth = labyrinth;
            this.StudentMaze = StudentMaze;
            this.m_iColDimensions = m_iColDimensions;
            this.m_iRowDimensions = m_iRowDimensions;
            this.m_iSize = (int) ((double)m_iSize/ 1.5);
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
            pictureBox1.Size = new System.Drawing.Size(m_iColDimensions * m_iSize + 3, m_iRowDimensions * m_iSize + 3);
            pictureBox1.Location = new Point((370 - (m_iColDimensions * m_iSize + 3)) / 2, ((370 - (m_iRowDimensions * m_iSize + 3)) / 2) + 5);
        }
    }
}
