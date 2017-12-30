using System;
using EmergencyCenter.Units.Characters.Contracts;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Units.Characters
{
    public abstract class CivilServant : Person, ICivilServant
    {
        private const string InvalidRouteMessage = "Route cannot be null.";
        private const string InvalidTargetMessage = "Target person cannot be null.";
        private const string InvalidMissionStart = "Servant cannot change route when it is on mission";

        private Position stationPosition;
        private Route route;
        private IPerson target;

        protected CivilServant(string name, int health, int strength, Position position, Map map, PersonType personType, Position stationPosition)
            : base(name, health, strength, position, map, personType)
        {
            this.StationPosition = stationPosition;
        }

        public Position StationPosition
        {
            get => this.stationPosition;
            set
            {
                this.Map?.ValidatePosition(value);
                this.stationPosition = value;
            }
        }

        public bool IsOnMission { get; protected set; }

        public Route Route
        {
            get => this.route;
            protected set
            {
                Validator.ValidateNull(value, InvalidRouteMessage);
                this.route = value;
            }
        }

        public IPerson Target
        {
            get => this.target;
            private set
            {
                Validator.ValidateNull(value, InvalidTargetMessage);
                this.target = value;
            }
        }

        protected void GoToAdress(Route newRoute)
        {
            if (this.IsOnMission)
            {
                throw new InvalidOperationException(InvalidMissionStart);
            }
            this.Route = newRoute;
        }

        public abstract Report MakeReport();

        public virtual void StartMission(Route newRoute, Person newTarget)
        {
            this.GoToAdress(newRoute);
            this.Target = newTarget;
            this.IsOnMission = true;
        }

        protected abstract void CompleteMission();
    }
}