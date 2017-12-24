using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public class Policeman : CivilServant
    {
        private Report report;
        private bool isOnPath;

        public Policeman(string name, int health, int strength, Position position, Map map, Position stationPosition)
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

        public void CheckCitizen(Person person)
        {
            string reportContent;

            if (person.PersonType != PersonType.Criminal)
            {
                reportContent = $"Person {person.Name} is clear";
                this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
                return;
            }
            if (this.Position == person.Position)
            {
                if (this.Strength >= person.Strength / 1.3)
                {
                    reportContent = $"Target {person.Name} has caught";
                    person.Health = 0;
                }
                else if (this.Strength >= person.Strength / 2)
                {
                    reportContent = $"Targer {person.Name} manage to escape";
                }
                else
                {
                    this.Health = 0;
                    reportContent = "Policeman tragically died.";
                }
            }
            else
            {
                reportContent = $"Person {person.Name} is not found";
            }

            this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
        }

        public override Report MakeReport()
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
                string reportContent = $"Tagret {this.Target.Name} has run away.";
                this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
                this.IsOnMission = false;
            }
        }

        private void Patrol()
        {
            Random random = new Random();
            if (!this.isOnPath)
            {
                while (true)
                {
                    int row = random.Next(0, this.Map.Rows);
                    int col = random.Next(0, this.Map.Cols);

                    if (this.Map[row, col] == 0 && (row != this.Position.X || col != this.Position.Y))
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
            string reportContent = $"Police patrol. Position {this.Position.X}, {this.Position.Y}";
            this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
        }
    }
}