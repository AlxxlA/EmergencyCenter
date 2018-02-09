using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Characters
{
    public abstract class CivilServant : Person, ICivilServant
    {
        private const string InvalidRouteMessage = "Route cannot be null.";
        private const string InvalidTargetMessage = "Target person cannot be null.";
        private const string InvalidPathFinderMessage = "Path finder cannot be null.";
        private const string InvalidMissionStart = "Servant cannot change route when it is on mission";

        private Position stationPosition;
        private IRoute route;
        private IPerson target;

        protected CivilServant(string name, int health, int strength, Position position, IMap map, PersonType personType, Position stationPosition, IPathFinder pathFinder)
            : base(name, health, strength, position, map, personType)
        {
            this.StationPosition = stationPosition;
            this.PathFinder = pathFinder ?? throw new ArgumentNullException(InvalidPathFinderMessage);
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

        public IRoute Route
        {
            get => this.route;
            protected set => this.route = value ?? throw new ArgumentNullException(InvalidRouteMessage);
        }

        public IPerson Target
        {
            get => this.target;
            private set => this.target = value ?? throw new ArgumentNullException(InvalidTargetMessage);
        }

        protected IPathFinder PathFinder { get; }

        public virtual void StartMission(IRoute newRoute, IPerson newTarget)
        {
            this.GoToAdress(newRoute);
            this.Target = newTarget;
            this.IsOnMission = true;
        }

        public abstract IReport MakeReport();

        protected void GoToAdress(IRoute newRoute)
        {
            if (this.IsOnMission)
            {
                throw new InvalidOperationException(InvalidMissionStart);
            }
            this.Route = newRoute;
        }

        protected abstract void CompleteMission();
    }
}
