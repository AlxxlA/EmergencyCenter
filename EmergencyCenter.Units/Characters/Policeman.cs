using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Map;

namespace EmergencyCenter.Units.Characters
{
    public class Policeman : CivilServant
    {
        private Report report;

        public Policeman(string name, int health, int strength, Position position)
            : base(name, health, strength, position)
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
            // TODO: implement it
        }

        public override Report MakeReport()
        {
            return this.report;
        }

        protected override void CompleteMission()
        {
            var nextPosition = this.Route.NextPosition();
            this.Position = nextPosition;

            if (this.Position == this.Target.Position)
            {
                // TODO : Implement what happens when police catch the target
                this.IsOnMission = false;
                return;
            }

            if (nextPosition == this.Route.LastPosition && this.Position != this.Target.Position)
            {
                string reportContent = "Tagret was run away.";
                this.report = new Report(ReportType.PoliceReport, this.Name, reportContent);
                this.IsOnMission = false;
            }
        }

        private void Patrol()
        {
            // TODO implement simple patrol
        }
    }
}