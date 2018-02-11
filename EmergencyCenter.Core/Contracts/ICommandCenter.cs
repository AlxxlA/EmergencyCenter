using System.Collections.Generic;
using EmergencyCenter.Core.Contracts.Data;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;

namespace EmergencyCenter.Core.Contracts
{
    public interface ICommandCenter
    {
        IDatabase<IPerson> Persons { get; }

        IDatabase<IPoliceman> Policemans { get; }

        IDatabase<IParamedic> Paramedics { get; }

        IDatabase<ICitizen> Citizens { get; }

        IDatabase<ICriminal> Criminals { get; }

        IMap Map { get; }

        IPathFinder PathFinder { get; }

        void UpdateUnits();

        void SendPolicemanToMission(IPerson target);

        void SendParamedicToMission(IPerson target);

        void InjurePerson(IPerson person, InjuryType injury);

        IList<string> Reports();
    }
}
