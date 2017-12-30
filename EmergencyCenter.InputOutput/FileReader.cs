using System.Collections.Generic;
using System.IO;
using EmergencyCenter.InputOutput.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.InputOutput
{
    public class FileReader : IFileReader
    {
        private const string InvalidFileMessage = "File path is invalid.";

        private string path;
        private int lineCounter;

        public FileReader(string path)
        {
            this.Path = path;
            this.lineCounter = 0;
        }

        public string Path
        {
            get => this.path;
            set
            {
                Validator.ValidateFilePath(value, InvalidFileMessage);
                this.path = value;
            }
        }

        public IEnumerable<string> ReadLine()
        {
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