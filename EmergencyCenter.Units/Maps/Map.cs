using System;
using System.IO;
using System.Linq;
using System.Text;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Units.Maps
{
    public class Map
    {
        private int[,] map;
        private int rows;
        private int cols;
        private string mapFilePath;

        public Map(string mapFilePath)
        {
            this.MapFilePath = mapFilePath;
            this.LoadMap();
        }

        public int Rows
        {
            get { return this.rows; }
        }

        public int Cols
        {
            get { return this.cols; }
        }

        public string MapFilePath
        {
            get { return this.mapFilePath; }
            set
            {
                if (!File.Exists(value))
                {
                    throw new FileNotFoundException("Map file was not found.");
                }

                this.mapFilePath = value;
            }
        }

        public int this[int x, int y]
        {
            get
            {
                this.ValidateCoordonates(x, y);
                return this.map[x, y];
            }
            set
            {
                this.ValidateCoordonates(x, y);
                this.map[x, y] = value;
            }
        }

        public void ValidatePosition(Position position)
        {
            if (position.X < 0 || position.X >= this.rows || position.Y < 0 || position.Y >= this.cols)
            {
                throw new IndexOutOfRangeException("Given position is out of bounds of map.");
            }
        }

        public int this[Position position]
        {
            get
            {
                this.ValidateCoordonates(position.X, position.Y);
                return this.map[position.X, position.Y];
            }
            set
            {
                this.ValidateCoordonates(position.X, position.Y);
                this.map[position.X, position.Y] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.cols; col++)
                {
                    result.Append(this.map[row, col]);
                }
                result.AppendLine();
            }

            return result.ToString().Trim();
        }

        /// <summary>
        /// Load map from given file path
        /// </summary>
        private void LoadMap()
        {
            var reader = new FileReader(this.mapFilePath);
            int lineNumber = 0;

            foreach (var line in reader.ReadLine())
            {
                string[] args = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // on the first line of file is dimensions e.g. 10(rows) 15(cols)
                if (lineNumber == 0)
                {
                    if (args.Length != 2)
                    {
                        throw new ArgumentException("Map dimensions shoud be two.");
                    }

                    try
                    {
                        int rows = int.Parse(args[0]);
                        int cols = int.Parse(args[1]);

                        this.rows = rows;
                        this.cols = cols;
                        this.map = new int[this.rows, this.cols];
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException("Map dimension shoud be integer");
                    }
                }
                else
                {
                    if (args.Length != this.cols) // expect line in format: row col tileType
                    {
                        throw new IndexOutOfRangeException("Invalid cols count.");
                    }
                    try
                    {
                        int[] tiles = args.Select(int.Parse).ToArray();

                        for (int j = 0; j < this.cols; j++)
                        {
                            this.map[lineNumber - 1, j] = tiles[j];
                        }
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException("Map tile shoud be integer.");
                    }
                }

                lineNumber++;
            }
        }

        /// <summary>
        /// Check if given coorinates are in boundas of map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ValidateCoordonates(int x, int y)
        {
            if (x < 0 || x >= this.rows)
            {
                throw new IndexOutOfRangeException("Given x coordinate is out of bounds of map.");
            }
            if (y < 0 || y >= this.cols)
            {
                throw new IndexOutOfRangeException("Given y coordinate is out of bounds of map.");
            }
        }
    }
}