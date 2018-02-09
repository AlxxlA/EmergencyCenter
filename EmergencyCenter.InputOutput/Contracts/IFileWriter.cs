namespace EmergencyCenter.InputOutput.Contracts
{
    public interface IFileWriter : IWriter
    {
        string Path { get; }

        bool Append { get; set; }
    }
}
