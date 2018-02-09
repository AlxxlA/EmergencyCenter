using System;
using EmergencyCenter.InputOutput.Contracts;

namespace EmergencyCenter.InputOutput
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(object value)
        {
            Console.WriteLine(value.ToString());
        }
    }
}
