using System.Collections.Generic;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;

namespace EmergencyCenter.Core.Contracts
{
    public interface ICommandCenter
    {
        IEnumerable<IPerson> Units { get; }

        IMap Map { get; }

        void UpdateUnits();

        void AddCharacter(IPerson person);

        void RemoveCharacter(IPerson person);

        IPerson ReturnCharacterById(int id);

        void RemoveCharacterById(int id);

        void SendPolicemanToMission(IPerson target);

        void SendParamedicToMission(IPerson target);

        void InjurePerson(IPerson person, InjuryType injury);

        IList<string> Reports();
    }
}
