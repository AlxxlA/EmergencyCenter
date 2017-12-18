using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Map;

namespace EmergencyCenter.Units.Characters
{
    public class Paramedic : CivilServant
    {
        public Paramedic(string name, int health, int strength, Position position)
            : base(name, health, strength, position)
        {
        }

        public override void Update()
        {
            if (this.IsOnMission)
            {
                this.CompleteMission();
            }
            else
            {
                // stay in base
            }
        }

        public override Report MakeReport()
        {
            throw new NotImplementedException();
        }

        protected override void CompleteMission()
        {
            throw new NotImplementedException();
        }
    }
}