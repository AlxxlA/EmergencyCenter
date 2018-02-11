using EmergencyCenter.Core.Contracts.Commands;

namespace EmergencyCenter.Core.Contracts.CommandProviders
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string line);
    }
}
