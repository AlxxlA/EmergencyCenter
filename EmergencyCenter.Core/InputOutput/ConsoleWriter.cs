using System;
using EmergencyCenter.Core.Contracts;

namespace EmergencyCenter.Core.InputOutput
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(object value)
        {
            Console.WriteLine(value.ToString());
        }
    }
}