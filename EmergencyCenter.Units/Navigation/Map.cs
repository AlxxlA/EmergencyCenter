using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmergencyCenter.InputOutput.Contracts;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Units.Navigation
{
    public class Map : IMap
    {
        private const string InvalidFilePathMessage = "Map file reader cannot null.";
        private const string InvalidRowsCountMessage = "Map rows cannot be less then {0} or greater then {1}.";
        private const string InvalidColsCountMessage = "Map cols cannot be less then {0} or greater then {1}.";
        private const string InvalidPositionMessage = "Given position is out of bounds of map.";
        private const string InvalidDimensionCountMessage = "Map dimensions should be two.";
        private const string DimensionsNotIntMessage = "Map dimension should be integer.";
        private const string InconsistentColsCountMessage = "Inconsistent cols count.";
        private const string TileNotIntegerMessage = "Map tile should be integer.";
        private const string XCoordinateOutOfRangeMessage = "Given x coordinate is out of bounds of map.";
        private const string YCoordinateOutOfRangeMessage = "Given y coordinate is out of bounds of map.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";

        private const int MinDimension = 1;
        private const int MaxDimension = 100;

        private int[,] map;
        private IFileReader fileReader;
        private readonly IValidator validator;
        private int rows;
        private int cols;

        public Map(IFileReader fileReader, IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);
            this.FileReader = fileReader;
            this.LoadMap();
        }

        public int MaxPositionX
        {
            get => this.rows;
            private set
            {
                var invalidMessage = string.Format(InvalidRowsCountMessage, MinDimension, MaxDimension);
                this.validator.ValidateIntRange(value, MinDimension, MaxDimension, invalidMessage);
                this.rows = value;
            }
        }

        public int MaxPositionY
        {
            get => this.cols;
            private set
            {
                var invalidMessage = string.Format(InvalidColsCountMessage, MinDimension, MaxDimension);
                this.validator.ValidateIntRange(value, MinDimension, MaxDimension, invalidMessage);
                this.cols = value;
            }
        }

        public IFileReader FileReader
        {
            get => this.fileReader;
            private set
            {
                this.validator.ValidateNull(value, InvalidFilePathMessage);
                this.fileReader = value;
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
            if (position.X < 0 || position.X >= this.MaxPositionX || position.Y < 0 || position.Y >= this.MaxPositionY)
            {
                throw new IndexOutOfRangeException(InvalidPositionMessage);
            }
        }

        public bool IsValidPosition(Position position)
        {
            return this.IsValidPosition(position.X, position.Y);
        }

        public bool IsValidPosition(int x, int y)
        {
            if (x < 0 || x >= this.MaxPositionX || y < 0 || y >= this.MaxPositionY)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Position> PositionNeighbours(Position position)
        {
            this.ValidatePosition(position);

            var neighbours = new List<Position>();

            int[] rowNum = { -1, 0, 0, 1 };
            int[] colNum = { 0, -1, 1, 0 };

            for (int i = 0; i < rowNum.Length; i++)
            {
                int row = position.X + rowNum[i];
                int col = position.Y + colNum[i];

                var neighbourPosition = new Position(row, col);

                if (this.IsValidPosition(neighbourPosition))
                {
                    neighbours.Add(neighbourPosition);
                }
            }

            return neighbours;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
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
            int lineNumber = 0;

            foreach (var line in this.fileReader.ReadLine())
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

                        this.MaxPositionX = parsedRows;
                        this.MaxPositionY = parsedCols;
                        this.map = new int[this.MaxPositionX, this.MaxPositionY];
                    }
                    catch (FormatException)
                    {
                        throw new FormatException(DimensionsNotIntMessage);
                    }
                }
                else
                {
                    if (args.Length != this.cols) // expect line of numbers representing map tiles, e.g 0 1 1 0 1...
                    {
                        throw new IndexOutOfRangeException(InconsistentColsCountMessage);
                    }
                    try
                    {
                        int[] tiles = args.Select(int.Parse).ToArray();

                        for (int j = 0; j < this.cols; j++)
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
            if (x < 0 || x >= this.rows)
            {
                throw new IndexOutOfRangeException(XCoordinateOutOfRangeMessage);
            }
            if (y < 0 || y >= this.cols)
            {
                throw new IndexOutOfRangeException(YCoordinateOutOfRangeMessage);
            }
        }
    }
}
