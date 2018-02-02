namespace EmergencyCenter.Core.Contracts.Commands
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string line);
    }
}
