namespace EmergencyCenter.Units.Characters.Contracts
{
    public interface IPoliceman : ICivilServant
    {
        void CheckCitizen(IPerson person);
    }
}