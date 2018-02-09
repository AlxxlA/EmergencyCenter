using System;
using System.Collections.Generic;
using EmergencyCenter.InputOutput.Contracts;

namespace EmergencyCenter.InputOutput
{
    public class ConsoleReader : IReader
    {
        public IEnumerable<string> ReadLine()
        {
            while (true)
            {
                yield return Console.ReadLine();
            }
        }
    }
}
