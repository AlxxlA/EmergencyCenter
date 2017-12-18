using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Map;

namespace EmergencyCenter.Units.Characters
{
    public class Citizen : Person
    {
        public Citizen(string name, int health, int strength, Position position) : base(name, health, strength, position)
        {
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
