﻿using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EmergencyCenter.Validation
{
    public class Validator : IValidator
    {
        public void ValidateIntRange(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException(message);
            }
        }

        public void ValidateDecimalRange(decimal value, decimal min, decimal max, string message)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException(message);
            }
        }

        public void ValidateNull(object value, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public void ValidateStringEmpty(string value, string message)
        {
            if (value == string.Empty)
            {
                throw new ArgumentException(message);
            }
        }

        public void ValidateStringNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(message);
            }
        }

        public void ValidateSymbols(string value, string pattern, string message)
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(value))
            {
                throw new ArgumentException(message);
            }
        }

        public void ValidateFilePath(string path, string message)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(message);
            }
        }
    }
}
