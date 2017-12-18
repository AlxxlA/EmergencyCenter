using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Map;

namespace EmergencyCenter.Units.Characters
{
    public class Criminal : Citizen
    {
        public Criminal(string name, int health, int strength, Position position)
            : base(name, health, strength, position)
        {
        }
    }
}
