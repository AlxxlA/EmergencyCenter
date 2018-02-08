using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Characters
{
    public class Policeman : CivilServant, IPoliceman
    {
        private const string PersonClearMessage = "Person {0} is clear";
        private const string PersonCaughtMessage = "Person {0} has caught";
        private const string PersonEscapeMessage = "Person {0} manage to escape";
        private const string PersonNotFoundMessage = "Person {0} has run away.";
        private const string PolicemanDiedMessage = "Policeman tragically died.";
        private const string PersonNullMessage = "Policeman cannot check null value person.";

        private IReport report;
        private bool isOnPath;

        public Policeman(string name, int health, int strength, Position position, IMap map, Position stationPosition)
            : base(name, health, strength, position, map, PersonType.Policeman, stationPosition)
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
                this.Patrol();
            }
        }

        public void CheckCitizen(IPerson person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(PersonNullMessage);
            }
            string reportContent;

            if (person.PersonType != PersonType.Criminal)
            {
                reportContent = string.Format(PersonClearMessage, person.Name);
                this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
                return;
            }
            if (this.Position == person.Position)
            {
                if (this.Strength >= person.Strength / 1.3)
                {
                    reportContent = string.Format(PersonCaughtMessage, person.Name);
                    person.Health = 0;
                }
                else if (this.Strength >= person.Strength / 2)
                {
                    reportContent = string.Format(PersonEscapeMessage, person.Name);
                }
                else
                {
                    this.Health = 0;
                    reportContent = PolicemanDiedMessage;
                }
            }
            else
            {
                reportContent = string.Format(PersonNotFoundMessage, person.Name);
            }

            this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
        }

        public override IReport MakeReport()
        {
            return this.report;
        }

        protected override void CompleteMission()
        {
            this.Position = this.Route.NextPosition();

            if (this.Position == this.Target.Position)
            {
                this.CheckCitizen(this.Target);
                this.IsOnMission = false;
                return;
            }

            if (this.Position == this.Route.LastPosition && this.Position != this.Target.Position)
            {
                string reportContent = string.Format(PersonNotFoundMessage, this.Target.Name);
                this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
                this.IsOnMission = false;
            }
        }

        private void Patrol()
        {
            var random = new Random();
            if (!this.isOnPath)
            {
                while (true)
                {
                    int row = random.Next(0, this.Map.MaxPositionX);
                    int col = random.Next(0, this.Map.MaxPositionY);

                    if (this.Map.IsValidPosition(row, col) && this.Map[row, col] == 0 && (row != this.Position.X || col != this.Position.Y))
                    {
                        var nextAdress = MapUtils.FindShortestRoute(this.Map, this.Position, new Position(row, col));
                        this.GoToAdress(nextAdress);
                        this.isOnPath = true;
                        break;
                    }
                }
            }
            else
            {
                this.Position = this.Route.NextPosition();
                if (this.Position == this.Route.LastPosition)
                {
                    this.isOnPath = false;
                }
            }

            this.report = null;
        }
    }
}
