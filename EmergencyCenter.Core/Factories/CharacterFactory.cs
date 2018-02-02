using EmergencyCenter.Units.Characters;
using EmergencyCenter.Units.Characters.Contracts;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Core.Factories
{
    public class CharacterFactory : ICharacterFactory
    {
        public IPerson CreatePoliceman(string name, int health, int strength, int x, int y, int stationX, int stationY, Map map)
        {
            return new Policeman(name, health, strength, new Position(x, y), map, new Position(stationX, stationY));
        }

        public IPerson CreateParamedic(string name, int health, int strength, int x, int y, int stationX, int stationY, Map map)
        {
            return new Paramedic(name, health, strength, new Position(x, y), map, new Position(stationX, stationY));
        }

        public IPerson CreateCitizen(string name, int health, int strength, int x, int y, Map map)
        {
            return new Citizen(name, health, strength, new Position(x, y), map);
        }

        public IPerson CreateCriminal(string name, int health, int strength, int x, int y, Map map)
        {
            return new Criminal(name, health, strength, new Position(x, y), map);
        }
    }
}
