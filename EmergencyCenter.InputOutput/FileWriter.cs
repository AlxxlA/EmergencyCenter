using System.IO;
using EmergencyCenter.InputOutput.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.InputOutput
{
    public class FileWriter : IFileWriter
    {
        private const string InvalidFileMessage = "File path is invalid.";

        private string path;

        public FileWriter(string path, bool append = false)
        {
            this.Path = path;
            this.Append = append;
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

        public bool Append { get; set; }

        public void WriteLine(object value)
        {
            using (var writer = new StreamWriter(this.Path, this.Append))
            {
                writer.WriteLine(value.ToString());
            }
        }
    }
}