using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters.Contracts
{
    public interface IPerson
    {
        int Id { get; }

        string Name { get; }

        int Health { get; set; }

        int Strength { get; set; }

        Position Position { get; set; }

        PersonType PersonType { get; }

        InjuryType Injury { get; set; }

        bool IsInjured { get; }

        bool IsAlive { get; }

        void Update();
    }
}