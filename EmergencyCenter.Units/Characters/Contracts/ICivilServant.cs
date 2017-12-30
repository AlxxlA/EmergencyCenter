using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters.Contracts
{
    public interface ICivilServant : IPerson
    {
        Position StationPosition { get; set; }

        bool IsOnMission { get; }

        Route Route { get; }

        IPerson Target { get; }

        Report MakeReport();

        void StartMission(Route newRoute, Person newTarget);
    }
}