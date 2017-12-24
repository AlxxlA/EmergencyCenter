using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public class Patient : Citizen
    {
        public Patient(string name, int health, int strength, Position position, Map map)
            : base(name, health, strength, position, map, PersonType.Patient)
        {
        }
    }
}