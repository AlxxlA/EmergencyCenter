using System.Collections.Generic;

namespace EmergencyCenter.Core.Contracts.Commands
{
    public interface ICommandProcessor
    {
        string ProcessCommand(ICommand command, IList<string> parameters);
    }
}
