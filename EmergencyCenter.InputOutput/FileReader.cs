using System.Collections.Generic;
using System.IO;
using EmergencyCenter.InputOutput.Contracts;

namespace EmergencyCenter.InputOutput
{
    public class FileReader : IReader
    {
        private int lineCounter;

        public FileReader(string path)
        {
            this.Path = path;
            this.lineCounter = 0;
        }

        public string Path { get; set; }

        public IEnumerable<string> ReadLine()
        {
            if (!File.Exists(this.Path))
            {
                throw new FileNotFoundException("File does not exist");
            }

            using (var reader = new StreamReader(this.Path))
            {
                var line = reader.ReadLine();

                int counter = 0;
                while (!string.IsNullOrEmpty(line))
                {
                    if (counter == this.lineCounter)
                    {
                        this.lineCounter++;
                        yield return line;
                    }
                    counter++;
                    line = reader.ReadLine();
                }
            }
        }
    }
}