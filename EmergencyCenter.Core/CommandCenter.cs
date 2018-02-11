using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Data;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core
{
    public class CommandCenter : ICommandCenter
    {
        private const string DatabaseCannnotBeNullMessage = "Database cannot be null.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";
        private const string MapCannnotBeNullMessage = "Map cannot be null.";
        private const string PathFinderCannnotBeNullMessage = "Path Finder cannot be null.";
        private const string PersonCannotBeNullMessage = "Person cannot be null.";
        private const string CannotSendPoliceMessage = "Cannot send policemans.";
        private const string CannotSendParamedicMessage = "Cannot send paramedic.";

        private IDatabase<IPerson> persons;
        private IDatabase<IPoliceman> policemans;
        private IDatabase<IParamedic> paramedics;
        private IDatabase<ICitizen> citizens;
        private IDatabase<ICriminal> criminals;
        private readonly IValidator validator;

        public CommandCenter(
            IMap map, IPathFinder pathFinder,
            IDatabase<IPerson> persons, IDatabase<ICitizen> citizens, IDatabase<ICriminal> criminals,
            IDatabase<IPoliceman> policemans, IDatabase<IParamedic> paramedics,
            IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);

            this.validator.ValidateNull(map, MapCannnotBeNullMessage);
            this.validator.ValidateNull(pathFinder, PathFinderCannnotBeNullMessage);
            this.PathFinder = pathFinder;
            this.Map = map;

            this.Persons = persons;
            this.Policemans = policemans;
            this.Paramedics = paramedics;
            this.Citizens = citizens;
            this.Criminals = criminals;
        }

        public IDatabase<IPerson> Persons
        {
            get => this.persons;
            private set
            {
                this.validator.ValidateNull(value, DatabaseCannnotBeNullMessage);
                this.persons = value;
            }
        }

        public IDatabase<IPoliceman> Policemans
        {
            get => this.policemans;
            private set
            {
                this.validator.ValidateNull(value, DatabaseCannnotBeNullMessage);
                this.policemans = value;
            }
        }

        public IDatabase<IParamedic> Paramedics
        {
            get => this.paramedics;
            private set
            {
                this.validator.ValidateNull(value, DatabaseCannnotBeNullMessage);
                this.paramedics = value;
            }
        }

        public IDatabase<ICitizen> Citizens
        {
            get => this.citizens;
            private set
            {
                this.validator.ValidateNull(value, DatabaseCannnotBeNullMessage);
                this.citizens = value;
            }
        }

        public IDatabase<ICriminal> Criminals
        {
            get => this.criminals;
            private set
            {
                this.validator.ValidateNull(value, DatabaseCannnotBeNullMessage);
                this.criminals = value;
            }
        }

        public IMap Map { get; }

        public IPathFinder PathFinder { get; }

        public void UpdateUnits()
        {
            foreach (var paramedic in this.paramedics)
            {
                paramedic.Update();
            }
            foreach (var citizen in this.citizens)
            {
                citizen.Update();
            }
            foreach (var criminal in this.criminals)
            {
                criminal.Update();
            }
            foreach (var policeman in this.policemans)
            {
                foreach (var citizen in this.citizens)
                {
                    if (policeman.Position == citizen.Position)
                    {
                        policeman.CheckCitizen(citizen);
                    }
                }
                foreach (var criminal in this.criminals)
                {
                    if (policeman.Position == criminal.Position)
                    {
                        policeman.CheckCitizen(criminal);
                    }
                }
            }

            this.Persons.RemoveAllByCriteria(p => !p.IsAlive);
            this.Policemans.RemoveAllByCriteria(p => !p.IsAlive);
            this.Paramedics.RemoveAllByCriteria(p => !p.IsAlive);
            this.Citizens.RemoveAllByCriteria(p => !p.IsAlive);
            this.Criminals.RemoveAllByCriteria(p => !p.IsAlive);
        }

        public void SendPolicemanToMission(IPerson target)
        {
            this.validator.ValidateNull(target, PersonCannotBeNullMessage);

            IPoliceman policeman = null;
            IRoute route = null;
            int minDistance = int.MaxValue;

            foreach (var person in this.policemans)
            {
                if (person.IsAlive && !person.IsOnMission && person != target)
                {
                    route = this.PathFinder.FindShortestRoute(this.Map, person.Position, target.Position);

                    if (route.Length != 0 && route.Length < minDistance)
                    {
                        policeman = person;
                    }
                }
            }

            if (policeman != null)
            {
                policeman.StartMission(route, target);
            }
            else
            {
                throw new ArgumentException(CannotSendPoliceMessage);
            }
        }

        public void SendParamedicToMission(IPerson target)
        {
            this.validator.ValidateNull(target, PersonCannotBeNullMessage);

            IParamedic paramedic = null;
            IRoute route = null;
            int minDistance = int.MaxValue;

            foreach (var person in this.paramedics)
            {
                if (person.IsAlive && !person.IsOnMission && person != target)
                {
                    route = this.PathFinder.FindShortestRoute(this.Map, person.Position, target.Position);

                    if (route.Length != 0 && route.Length < minDistance)
                    {
                        paramedic = person;
                    }
                }
            }

            if (paramedic != null)
            {
                paramedic.StartMission(route, target);
            }
            else
            {
                throw new ArgumentException(CannotSendParamedicMessage);
            }
        }

        public void InjurePerson(IPerson person, InjuryType injury)
        {
            this.validator.ValidateNull(person, PersonCannotBeNullMessage);

            person.Injury = injury;
        }

        public IList<string> Reports()
        {
            var result = new List<string>();

            foreach (var policeman in this.policemans)
            {
                var report = policeman.MakeReport();
                if (report != null)
                {
                    result.Add(report.ToString());
                }
            }
            foreach (var paramedic in this.paramedics)
            {
                var report = paramedic.MakeReport();
                if (report != null)
                {
                    result.Add(report.ToString());
                }
            }

            return result;
        }
    }
}
