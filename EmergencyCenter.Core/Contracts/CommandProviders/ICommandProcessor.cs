using System.Collections.Generic;
using EmergencyCenter.Core.Contracts.Commands;

namespace EmergencyCenter.Core.Contracts.CommandProviders
{
    public interface ICommandProcessor
    {
        string ProcessCommand(ICommand command, IList<string> parameters);
    }
}
