using System;
using EmergencyCenter.Units.Characters.Contracts;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public class Paramedic : CivilServant, IParamedic
    {
        private const string PersonIsFineMessage = "Person {0} is fine.";
        private const string PersonIsAlreadyDeathMessage = "Person {0} is already death.";
        private const string PersonTransportToHospitalMessage = "Person {0} is successfully transport to hospital.";
        private const string PersonDeadOnWayMessage = "Person {0} dead on way to hospital";
        private const string PersonNotFoundMessage = "Person {0} not found";

        private IReport report;
        private bool isOnWayToTarget;
        private bool isOnWayToHospital;
        private bool isWithPatient;

        public Paramedic(string name, int health, int strength, Position position, Map map, Position stationPosition)
            : base(name, health, strength, position, map, PersonType.Paramedic, stationPosition)
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
                if (this.Position != this.StationPosition)
                {
                    this.ReturnToStation();
                }
                this.report = null;
            }
            base.Update();
        }

        public override void StartMission(Route route, IPerson target)
        {
            base.StartMission(route, target);
            this.isOnWayToTarget = true;
        }

        public override IReport MakeReport()
        {
            return this.report;
        }

        protected override void CompleteMission()
        {
            this.Position = this.Route.NextPosition();

            string reportContent;

            if (this.Position == this.Target.Position && this.isOnWayToTarget)
            {
                this.Route = MapUtils.FindShortestRoute(this.Map, this.Position, this.StationPosition);
                this.isOnWayToTarget = false;
                this.isOnWayToHospital = true;
                this.isWithPatient = true;

                if (!this.Target.IsAlive)
                {
                    reportContent = string.Format(PersonIsAlreadyDeathMessage, this.Target.Name);
                    this.report = new Report(ReportType.MedicalReport, this.Name, reportContent);
                    this.isWithPatient = false;
                    this.IsOnMission = false;
                }
                if (this.Target.Health == MaxHealth && !this.Target.IsInjured)
                {
                    reportContent = string.Format(PersonIsFineMessage, this.Target.Name);
                    this.report = new Report(ReportType.MedicalReport, this.Name, reportContent);
                    this.isWithPatient = false;
                    this.IsOnMission = false;
                }
            }
            if (this.isOnWayToHospital && this.Position != this.StationPosition && this.Target.IsAlive && this.isWithPatient)
            {
                this.Target.Health = Math.Min(MaxHealth, this.Target.Health + 10);
                this.Target.Position = this.Position;
            }
            else if (this.isOnWayToHospital && this.Position == this.StationPosition && this.Target.IsAlive && this.isWithPatient)
            {
                this.isOnWayToHospital = false;
                this.Target.Injury = InjuryType.None;
                this.Target.Health = MaxHealth;
                this.IsOnMission = false;
                reportContent = string.Format(PersonTransportToHospitalMessage, this.Target.Name);
                this.report = new Report(ReportType.MedicalReport, this.Name, reportContent);
                return;
            }
            else if (!this.Target.IsAlive && this.isOnWayToHospital && this.isWithPatient)
            {
                reportContent = string.Format(PersonDeadOnWayMessage, this.Target.Name);
                this.report = new Report(ReportType.MedicalReport, this.Name, reportContent);
                this.IsOnMission = false;
                return;
            }

            if (this.isOnWayToHospital && !this.isWithPatient && this.Position != this.Target.Position)
            {
                this.report = null;
            }

            if (this.Position == this.Route.LastPosition && this.Position != this.Target.Position && this.isOnWayToTarget)
            {
                reportContent = string.Format(PersonNotFoundMessage, this.Target.Name);
                this.report = new Report(ReportType.MedicalReport, this.Name, reportContent);

                this.Route = MapUtils.FindShortestRoute(this.Map, this.Position, this.StationPosition);
                this.isOnWayToTarget = false;
                this.isOnWayToHospital = true;
                this.isWithPatient = false;
                this.IsOnMission = false;
            }
            if (this.Position == this.StationPosition && !this.isWithPatient)
            {
                this.IsOnMission = false;
            }
        }

        private void ReturnToStation()
        {
            if (this.Route.LastPosition != this.StationPosition)
            {
                this.Route = MapUtils.FindShortestRoute(this.Map, this.Position, this.StationPosition);
            }

            this.Position = this.Route.NextPosition();
        }
    }
}