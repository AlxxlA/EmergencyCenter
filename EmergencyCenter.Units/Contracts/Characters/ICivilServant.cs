using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Contracts.Characters
{
    public interface ICivilServant : IPerson
    {
        Position StationPosition { get; set; }

        bool IsOnMission { get; }

        IRoute Route { get; }

        IPerson Target { get; }

        IReport MakeReport();

        void StartMission(IRoute newRoute, IPerson newTarget);
    }
}
