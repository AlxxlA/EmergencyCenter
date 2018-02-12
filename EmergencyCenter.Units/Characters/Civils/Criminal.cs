using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Contracts.Random;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Characters.Civils
{
    public class Criminal : Citizen, ICriminal
    {
        private int movesToEscape;

        public Criminal(string name, int health, int strength, Position position, IMap map, IRandomGenerator random)
            : base(name, health, strength, position, map, random, PersonType.Criminal)
        {
        }

        public bool IsEscape { get; private set; }

        public override void Update()
        {
            bool isRun = this.Random.Next(0, 101) < 10;

            if (!this.IsAlive || !isRun) return;

            base.Update();
            this.movesToEscape++;
            if (this.movesToEscape >= 10)
            {
                this.IsEscape = true;
            }
        }
    }
}
