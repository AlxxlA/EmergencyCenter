using System.Collections.Generic;
using System.IO;
using EmergencyCenter.Core.Contracts;

namespace EmergencyCenter.Core.InputOutput
{
    public class FileReader : IReader
    {
        public FileReader(string path)
        {
            this.Path = path;
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

                while (!string.IsNullOrEmpty(line))
                {
                    yield return line;
                    line = reader.ReadLine();
                }
            }
        }
    }
}