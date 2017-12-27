using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public class Patient : Citizen
    {
        public Patient(string name, int health, int strength, Position position, Map map, InjuryType injury)
            : base(name, health, strength, position, map, PersonType.Patient)
        {
        }

        public InjuryType Injury { get; set; }

        public override void Update()
        {
            switch (this.Injury)
            {
                case InjuryType.Bruise:
                    int damage = Math.Min(this.Health, (int)(this.Health * 0.05)); // damage cannot be more then current health
                    this.Health -= damage;
                    break;
                case InjuryType.Wound:
                    damage = Math.Min(this.Health, (int)(this.Health * 0.10));
                    this.Health -= damage;
                    break;
                case InjuryType.LargeFracture:
                    damage = Math.Min(this.Health, (int)(this.Health * 0.20));
                    this.Health -= damage;
                    break;
            }
        }
    }
}