using System.Collections.Generic;

namespace EmergencyCenter.Core.Contracts.Commands
{
    public interface ICommand
    {
        string Execute(IList<string> parameters);
    }
}
