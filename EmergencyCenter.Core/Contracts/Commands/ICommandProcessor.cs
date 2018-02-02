namespace EmergencyCenter.Core.Contracts.Commands
{
    public interface ICommandProcessor
    {
        void ProcessCommand(ICommand command);
    }
}
