using System;
using System.IO;
using System.Text;
using EmergencyCenter.Core.InputOutput;

namespace EmergencyCenter.Units.Map
{
    public class Map
    {
        private string[][] map;
        private int rows;
        private int cols;
        private string mapFilePath;

        public Map(string mapFilePath)
        {
            this.MapFilePath = mapFilePath;
            this.LoadMap();
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

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < this.rows; i++)
            {
                result.AppendLine(string.Join(" ", this.map[i]));
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
                        this.map = new string[this.rows][];
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException("Map dimension shoud be integer");
                    }
                }
                else
                {
                    if (args.Length != this.cols)
                    {
                        throw new IndexOutOfRangeException("Inconsistend map dimension (cols).");
                    }
                    if (lineNumber > this.rows)
                    {
                        throw new IndexOutOfRangeException("Inconsistend map dimension (rows).");
                    }

                    this.map[lineNumber - 1] = args;
                }

                lineNumber++;
            }

            if (lineNumber - 1 != this.rows)
            {
                throw new IndexOutOfRangeException("Inconsistend map dimension (rows).");
            }
        }
    }
}