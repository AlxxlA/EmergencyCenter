using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Units.Characters.Civils;
using EmergencyCenter.Units.Characters.Servants;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Contracts.Random;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Core.Factories
{
    public class CharacterFactory : ICharacterFactory
    {
        public IPerson CreatePoliceman(string name, int health, int strength, int x, int y, int stationX, int stationY, IMap map, IPathFinder pathFinder, IRandomGenerator random)
        {
            return new Policeman(name, health, strength, new Position(x, y), map, new Position(stationX, stationY), pathFinder, random);
        }

        public IPerson CreateParamedic(string name, int health, int strength, int x, int y, int stationX, int stationY, IMap map, IPathFinder pathFinder, IRandomGenerator random)
        {
            return new Paramedic(name, health, strength, new Position(x, y), map, new Position(stationX, stationY), pathFinder, random);
        }

        public IPerson CreateCitizen(string name, int health, int strength, int x, int y, IMap map, IRandomGenerator random)
        {
            return new Citizen(name, health, strength, new Position(x, y), map, random);
        }

        public IPerson CreateCriminal(string name, int health, int strength, int x, int y, IMap map, IRandomGenerator random)
        {
            return new Criminal(name, health, strength, new Position(x, y), map, random);
        }
    }
}
