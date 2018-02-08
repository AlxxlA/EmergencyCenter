using EmergencyCenter.Core.Contracts.Commands;

namespace EmergencyCenter.Core.Contracts.Factories
{
    public interface ICommandFactory
    {
        ICommand Create(string commandName);
    }
}
