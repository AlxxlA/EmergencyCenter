using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public class Citizen : Person
    {
        public Citizen(string name, int health, int strength, Position position, Map map, PersonType personType)
            : base(name, health, strength, position, map, personType)
        {
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
