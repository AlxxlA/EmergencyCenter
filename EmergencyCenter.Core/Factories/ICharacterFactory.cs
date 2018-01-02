using EmergencyCenter.Units.Characters.Contracts;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Core.Factories
{
    public interface ICharacterFactory
    {
        IPerson CreatePoliceman(string name, int health, int strength, int x, int y, int stationX, int stationY, Map map);

        IPerson CreateParamedic(string name, int health, int strength, int x, int y, int stationX, int stationY, Map map);

        IPerson CreateCitizen(string name, int health, int strength, int x, int y, Map map);

        IPerson CreateCriminal(string name, int health, int strength, int x, int y, Map map);
    }
}