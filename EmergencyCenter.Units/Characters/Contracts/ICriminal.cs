namespace EmergencyCenter.Units.Characters.Contracts
{
    public interface ICriminal : ICitizen
    {
        bool IsEscape { get; }
    }
}