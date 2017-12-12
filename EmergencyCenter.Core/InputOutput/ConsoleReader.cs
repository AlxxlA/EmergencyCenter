using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;

namespace EmergencyCenter.Core.InputOutput
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