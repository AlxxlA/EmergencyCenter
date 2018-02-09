using System;
using System.Collections.Generic;
using System.IO;
using EmergencyCenter.InputOutput.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.InputOutput
{
    public class FileReader : IFileReader
    {
        private const string InvalidFileMessage = "File path is invalid.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";

        private string path;
        private readonly IValidator validator;
        private int lineCounter;

        public FileReader(string path, IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);

            this.Path = path;
            this.lineCounter = 0;
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
