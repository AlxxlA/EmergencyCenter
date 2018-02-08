namespace EmergencyCenter.Units.Contracts.Characters
{
    public interface IPoliceman : ICivilServant
    {
        void CheckCitizen(IPerson person);
    }
}
