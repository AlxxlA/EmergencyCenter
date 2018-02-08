namespace EmergencyCenter.Units.Contracts.Characters
{
    public interface ICriminal : ICitizen
    {
        bool IsEscape { get; }
    }
}
