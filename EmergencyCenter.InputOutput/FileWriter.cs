using System;
using System.IO;
using EmergencyCenter.InputOutput.Contracts;

namespace EmergencyCenter.InputOutput
{
    public class FileWriter : IWriter
    {
        public FileWriter(string path, bool append = true)
        {
            this.Path = path;
            this.Append = append;
        }

        public string Path { get; }

        public bool Append { get; set; }

        public void WriteLine(object value)
        {
            if (string.IsNullOrEmpty(this.Path))
            {
                throw new ArgumentNullException("Path can not be null or empty.");
            }

            using (var writer = new StreamWriter(this.Path, append: true))
            {
                writer.WriteLine(value.ToString());
            }
        }
    }
}