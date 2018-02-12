using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Contracts.Characters
{
    public interface IPerson
    {
        int Id { get; }

        string Name { get; }

        int Health { get; set; }

        int Strength { get; }

        Position Position { get; set; }

        IMap Map { get; }

        PersonType PersonType { get; }

        InjuryType Injury { get; set; }

        bool IsInjured { get; }

        bool IsAlive { get; }

        void Update();
    }
}
