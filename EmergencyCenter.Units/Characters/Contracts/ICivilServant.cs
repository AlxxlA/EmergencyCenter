using EmergencyCenter.Units.Contracts;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters.Contracts
{
    public interface ICivilServant : IPerson
    {
        Position StationPosition { get; set; }

        bool IsOnMission { get; }

        Route Route { get; }

        IPerson Target { get; }

        IReport MakeReport();

        void StartMission(Route newRoute, IPerson newTarget);
    }
}