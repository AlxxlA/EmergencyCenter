using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Contracts.Random;

namespace EmergencyCenter.Core.Contracts.Factories
{
    public interface ICharacterFactory
    {
        IPerson CreatePoliceman(string name, int health, int strength, int x, int y, int stationX, int stationY, IMap map, IPathFinder pathFinder, IRandomGenerator random);

        IPerson CreateParamedic(string name, int health, int strength, int x, int y, int stationX, int stationY, IMap map, IPathFinder pathFinder, IRandomGenerator random);

        IPerson CreateCitizen(string name, int health, int strength, int x, int y, IMap map, IRandomGenerator random);

        IPerson CreateCriminal(string name, int health, int strength, int x, int y, IMap map, IRandomGenerator random);
    }
}
