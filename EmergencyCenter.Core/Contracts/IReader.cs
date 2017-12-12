using System.Collections.Generic;

namespace EmergencyCenter.Core.Contracts
{
    public interface IReader
    {
        IEnumerable<string> ReadLine();
    }
}