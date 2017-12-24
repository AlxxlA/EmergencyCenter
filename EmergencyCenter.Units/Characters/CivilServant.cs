using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public abstract class CivilServant : Person
    {
        protected CivilServant(string name, int health, int strength, Position position, Map map, PersonType personType, Position stationPosition)
            : base(name, health, strength, position, map, personType)
        {
            this.StationPosition = stationPosition;
        }

        public Position StationPosition { get; set; }
        public bool IsOnMission { get; set; }

        public Route Route { get; private set; }
        public Person Target { get; private set; }

        protected void GoToAdress(Route route)
        {
            if (this.IsOnMission)
            {
                throw new InvalidOperationException("Servant cannot change route when it is on mission");
            }
            this.Route = route;
        }

        public abstract Report MakeReport();

        public void StartMission(Route route, Person target)
        {
            this.GoToAdress(route);
            this.Target = target;
            this.IsOnMission = true;
        }

        protected abstract void CompleteMission();
    }
}
