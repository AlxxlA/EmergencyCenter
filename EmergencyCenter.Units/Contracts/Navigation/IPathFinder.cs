using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Contracts.Navigation
{
    public interface IPathFinder
    {
        IRoute FindShortestRoute(IMap map, Position start, Position destination);
    }
}
