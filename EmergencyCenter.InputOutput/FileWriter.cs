using System;
using System.IO;
using EmergencyCenter.InputOutput.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.InputOutput
{
    public class FileWriter : IFileWriter
    {
        private const string InvalidFileMessage = "File path is invalid.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";

        private string path;
        private readonly IValidator validator;

        public FileWriter(string path, IValidator validator, bool append = false)
        {
            this.validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);
            this.Path = path;
            this.Append = append;
        }

        public string Path
        {
            get => this.path;
            set
            {
                this.validator.ValidateFilePath(value, InvalidFileMessage);
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
