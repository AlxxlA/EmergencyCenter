using System;
using System.Linq;
using System.Text;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Units.Maps
{
    public class Map
    {
        private const string InvalidFilePathMessage = "Map file was not found.";
        private const string InvalidRowsCountMessage = "Map rows cannot be less then {0} or greater then {1}.";
        private const string InvalidColsCountMessage = "Map cols cannot be less then {0} or greater then {1}.";
        private const string InvalidPositionMessage = "Given position is out of bounds of map.";
        private const string InvalidDimensionCountMessage = "Map dimensions should be two.";
        private const string DimensionsNotIntMessage = "Map dimension should be integer.";
        private const string InconsistentColsCountMessage = "Inconsistent cols count.";
        private const string TileNotIntegerMessage = "Map tile should be integer.";
        private const string XCoordinateOutOfRangeMessage = "Given x coordinate is out of bounds of map.";
        private const string YCoordinateOutOfRangeMessage = "Given y coordinate is out of bounds of map.";

        private const int MinDimension = 1;
        private const int MaxDimension = 100;

        private int[,] map;
        private string mapFilePath;
        private int rows;
        private int cols;

        public Map(string mapFilePath)
        {
            this.MapFilePath = mapFilePath;
            this.LoadMap();
        }

        public int Rows
        {
            get => this.rows;
            private set
            {
                var invalidMessage = string.Format(InvalidRowsCountMessage, MinDimension, MaxDimension);
                Validator.ValidateIntRange(value, MinDimension, MaxDimension, invalidMessage);
                this.rows = value;
            }
        }

        public int Cols
        {
            get => this.cols;
            private set
            {
                var invalidMessage = string.Format(InvalidColsCountMessage, MinDimension, MaxDimension);
                Validator.ValidateIntRange(value, MinDimension, MaxDimension, invalidMessage);
                this.cols = value;
            }
        }

        public string MapFilePath
        {
            get => this.mapFilePath;
            private set
            {
                Validator.ValidateFilePath(value, InvalidFilePathMessage);
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

        public int this[Position position]
        {
            get
            {
                this.ValidatePosition(position);
                return this.map[position.X, position.Y];
            }
            set
            {
                this.ValidatePosition(position);
                this.map[position.X, position.Y] = value;
            }
        }

        public void ValidatePosition(Position position)
        {
            if (position.X < 0 || position.X >= this.Rows || position.Y < 0 || position.Y >= this.Cols)
            {
                throw new IndexOutOfRangeException(InvalidPositionMessage);
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Cols; col++)
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
                string[] args = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // on the first line of file is dimensions e.g. 10(rows) 15(cols)
                if (lineNumber == 0)
                {
                    if (args.Length != 2)
                    {
                        throw new ArgumentException(InvalidDimensionCountMessage);
                    }

                    try
                    {
                        int parsedRows = int.Parse(args[0]);
                        int parsedCols = int.Parse(args[1]);

                        this.Rows = parsedRows;
                        this.Cols = parsedCols;
                        this.map = new int[this.Rows, this.Cols];
                    }
                    catch (FormatException)
                    {
                        throw new FormatException(DimensionsNotIntMessage);
                    }
                }
                else
                {
                    if (args.Length != this.Cols) // expect line in format: row col tileType
                    {
                        throw new IndexOutOfRangeException(InconsistentColsCountMessage);
                    }
                    try
                    {
                        int[] tiles = args.Select(int.Parse).ToArray();

                        for (int j = 0; j < this.Cols; j++)
                        {
                            this.map[lineNumber - 1, j] = tiles[j];
                        }
                    }
                    catch (FormatException)
                    {
                        throw new FormatException(TileNotIntegerMessage);
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
            if (x < 0 || x >= this.Rows)
            {
                throw new IndexOutOfRangeException(XCoordinateOutOfRangeMessage);
            }
            if (y < 0 || y >= this.Cols)
            {
                throw new IndexOutOfRangeException(YCoordinateOutOfRangeMessage);
            }
        }
    }
}