using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;

namespace EmergencyCenter.Core.Contracts.Factories
{
    public interface ICharacterFactory
    {
        IPerson CreatePoliceman(string name, int health, int strength, int x, int y, int stationX, int stationY, IMap map);

        IPerson CreateParamedic(string name, int health, int strength, int x, int y, int stationX, int stationY, IMap map);

        IPerson CreateCitizen(string name, int health, int strength, int x, int y, IMap map);

        IPerson CreateCriminal(string name, int health, int strength, int x, int y, IMap map);
    }
}
