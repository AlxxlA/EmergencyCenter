using System.Collections.Generic;

namespace EmergencyCenter.InputOutput.Contracts
{
    public interface IReader
    {
        IEnumerable<string> ReadLine();
    }
}