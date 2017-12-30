namespace EmergencyCenter.InputOutput.Contracts
{
    public interface IFileReader : IReader
    {
        string Path { get; }
    }
}