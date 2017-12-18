using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Map;

namespace EmergencyCenter.Units.Characters
{
    public abstract class CivilServant : Person
    {
        protected CivilServant(string name, int health, int strength, Position position)
            : base(name, health, strength, position)
        {
        }

        public bool IsOnMission { get; set; }

        public Route Route { get; private set; }
        public Person Target { get; private set; }

        public void GoToAdress(Route route, Person target)
        {
            this.Route = route;
            this.Target = target;
            this.IsOnMission = true;
        }

        public abstract Report MakeReport();

        protected abstract void CompleteMission();
    }
}
