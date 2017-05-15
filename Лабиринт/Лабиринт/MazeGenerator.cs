using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лабиринт
{
    class MazeGenerator
    {
        private int w, h, wn, hn;
        private bool manypartion;

        [Flags]
        public enum Directions
        {
            N = 1,
            S = 2,
            E = 4,
            W = 8
        }

        public class Grid
        {
            private const int _rowDimension = 0;
            private const int _columnDimension = 1;

            public int MinSize { get; private set; }
            public int MaxSize { get; private set; }
            public int[,] Cells { get; private set; }

            public Grid() : this(3, 3)
            {

            }

            public Grid(int rows, int columns)
            {
                MinSize = 3;
                MaxSize = 50;
                Cells = Initialise(rows, columns);
            }

            public int[,] Initialise(int rows, int columns)
            {
                if (rows < MinSize)
                    rows = MinSize;

                if (columns < MinSize)
                    columns = MinSize;

                if (rows > MaxSize)
                    rows = MaxSize;

                if (columns > MaxSize)
                    columns = MaxSize;

                var cells = new int[rows, columns];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        cells[i, j] = 0;
                    }
                }

                return cells;
            }

            private Dictionary<Directions, int> DirectionX = new Dictionary<Directions, int>
        {
            { Directions.N, 0 },
            { Directions.S, 0 },
            { Directions.E, 1 },
            { Directions.W, -1 }
        };

            private Dictionary<Directions, int> DirectionY = new Dictionary<Directions, int>
        {
            { Directions.N, -1 },
            { Directions.S, 1 },
            { Directions.E, 0 },
            { Directions.W, 0 }
        };

            private Dictionary<Directions, Directions> Opposite = new Dictionary<Directions, Directions>
        {
            { Directions.N, Directions.S },
            { Directions.S, Directions.N },
            { Directions.E, Directions.W },
            { Directions.W, Directions.E }
        };

            public int[,] Generate()
            {
                var cells = Cells;
                CarvePassagesFrom(0, 0, ref cells);
                return cells;
            }

            public void CarvePassagesFrom(int currentX, int currentY, ref int[,] grid)
            {
                var directions = new List<Directions>
            {
                Directions.N,
                Directions.S,
                Directions.E,
                Directions.W
            }
                .OrderBy(x => Guid.NewGuid());

                foreach (var direction in directions)
                {
                    var nextX = currentX + DirectionX[direction];
                    var nextY = currentY + DirectionY[direction];

                    if (IsOutOfBounds(nextX, nextY, grid))
                        continue;

                    if (grid[nextY, nextX] != 0) // has been visited
                        continue;

                    grid[currentY, currentX] |= (int)direction;
                    grid[nextY, nextX] |= (int)Opposite[direction];

                    CarvePassagesFrom(nextX, nextY, ref grid);
                }
            }

            private bool IsOutOfBounds(int x, int y, int[,] grid)
            {
                if (x < 0 || x > grid.GetLength(_rowDimension) - 1)
                    return true;

                if (y < 0 || y > grid.GetLength(_columnDimension) - 1)
                    return true;

                return false;
            }

            public void Print(int[,] grid)
            {
                var rows = grid.GetLength(_rowDimension);
                var columns = grid.GetLength(_columnDimension);

                // Top line
                Console.Write(" ");
                for (int i = 0; i < columns; i++)
                    Console.Write(" _");
                Console.WriteLine();

                for (int y = 0; y < rows; y++)
                {
                    Console.Write(" |");

                    for (int x = 0; x < columns; x++)
                    {
                        var directions = (Directions)grid[y, x];

                        var s = directions.HasFlag(Directions.S) ? " " : "_";

                        Console.Write(s);

                        s = directions.HasFlag(Directions.E) ? " " : "|";

                        Console.Write(s);
                    }

                    Console.WriteLine();
                }
            }
        }

        public MazeGenerator(int w, int h, bool manypartion)
        {
            this.w = w;
            this.h = h;
            this.manypartion = manypartion;
            wn = (w - 1) / 2;
            hn = (h - 1) / 2;
        }

        public int[,] Gener()
        {
            Grid test = new Grid(wn, hn);
            int[,] result = test.Generate();
            List<int> mazeout = new List<int>();
            for (int i = 0; i < w; i++)
            {
                mazeout.Add(1);
            }
            for (int i = 0; i < wn; i++)
            {
                List<int> maze1 = new List<int>();
                List<int> maze2 = new List<int>();
                maze1.Add(1);
                maze2.Add(1);
                for (int j = 0; j < hn; j++)
                {
                    maze1.Add(0);
                    //maze2.Add()
                    var directions = (Directions)result[i, j];
                    if (directions.HasFlag(Directions.E))
                    {
                        maze1.Add(0);
                    }
                    else
                    {
                        maze1.Add(1);
                    }
                    if (directions.HasFlag(Directions.S))
                    {
                        maze2.Add(0);
                        if (!manypartion)
                        {
                            maze2.Add(0);
                        }
                        //maze2.Add(0);
                    }
                    else
                    {
                        maze2.Add(1);
                        if (!manypartion)
                        {
                            maze2.Add(1);
                        }
                        
                        //maze2.Add(1);
                    }

                    if (manypartion)
                    {
                        maze2.Add(1);
                    }
                    

                }
                for (int j = 0; j < h; j++)
                {
                    mazeout.Add(maze1[j]);
                }
                for (int j = 0; j < h; j++)
                {
                    mazeout.Add(maze2[j]);
                }
            }
            int[,] mazematrix = new int[w, h];
            int count = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    mazematrix[i, j] = mazeout[count];
                    count++;
                }
            }
            mazematrix[0, 0] = 0;
            mazematrix[1, 0] = 0;
            return mazematrix;
        }
    }
}
