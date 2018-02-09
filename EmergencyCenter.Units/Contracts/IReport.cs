namespace EmergencyCenter.Units.Contracts
{
    public interface IReport
    {
        ReportType ReportType { get; }

        string Author { get; }

        string Content { get; }
    }
}
