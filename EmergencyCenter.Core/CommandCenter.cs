using System;
using System.Collections.Generic;
using System.Linq;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core
{
    public class CommandCenter : ICommandCenter
    {
        private const string UnitAlreadyExistMessage = "Unit already exist and cannot be added again.";
        private const string CannotSendPoliceMessage = "Cannot send police.";
        private const string CannotSendParamedicMessage = "Cannot send paramedic.";

        private ICollection<IPerson> units;
        private ICollection<IPoliceman> police;
        private ICollection<IParamedic> paramedics;
        private ICollection<ICitizen> citizens;
        private ICollection<ICriminal> criminals;

        public CommandCenter(Map map)
        {
            Validator.ValidateNull(map, "Map cannot be null.");
            this.Map = map;
            this.units = new List<IPerson>();
            this.police = new List<IPoliceman>();
            this.paramedics = new List<IParamedic>();
            this.citizens = new List<ICitizen>();
            this.criminals = new List<ICriminal>();
        }

        public IEnumerable<IPerson> Units => this.units;

        public Map Map { get; }

        public void UpdateUnits()
        {
            foreach (var person in this.units)
            {
                person.Update();

                if (person.PersonType == PersonType.Policeman)
                {
                    foreach (var citizen in this.citizens)
                    {
                        if (person.Position == citizen.Position && person != citizen)
                        {
                            var policeman = person as IPoliceman;
                            policeman?.CheckCitizen(citizen);
                        }
                    }
                    foreach (var criminal in this.criminals)
                    {
                        if (person.Position == criminal.Position && person != criminal)
                        {
                            var policeman = person as IPoliceman;
                            policeman?.CheckCitizen(criminal);
                        }
                    }
                }
            }

            var remaining = this.units.Where(person => person.IsAlive).ToList();
            var policemanRemaining = this.police.Where(person => person.IsAlive).ToList();
            var paramedicRemaining = this.paramedics.Where(person => person.IsAlive).ToList();
            var citizensRemaining = this.citizens.Where(person => person.IsAlive).ToList();
            var criminalsRemaining = this.criminals.Where(person => person.IsAlive).ToList();

            this.units = remaining;
            this.police = policemanRemaining;
            this.paramedics = paramedicRemaining;
            this.citizens = citizensRemaining;
            this.criminals = criminalsRemaining;
        }

        public void AddCharacter(IPerson person)
        {
            if (this.Map != person.Map)
            {
                throw new ArgumentException("Cannot add persons from another worlds.");
            }
            if (!this.units.Contains(person))
            {
                this.units.Add(person);

                switch (person.PersonType)
                {
                    case PersonType.Policeman:
                        this.police.Add(person as IPoliceman);
                        break;
                    case PersonType.Paramedic:
                        this.paramedics.Add(person as IParamedic);
                        break;
                    case PersonType.Citizen:
                        this.citizens.Add(person as ICitizen);
                        break;
                    case PersonType.Criminal:
                        this.criminals.Add(person as ICriminal);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentException(UnitAlreadyExistMessage);
            }
        }

        public void RemoveCharacter(IPerson person)
        {
            this.units.Remove(person);

            switch (person.PersonType)
            {
                case PersonType.Policeman:
                    this.police.Remove(person as IPoliceman);
                    break;
                case PersonType.Paramedic:
                    this.paramedics.Remove(person as IParamedic);
                    break;
                case PersonType.Citizen:
                    this.citizens.Remove(person as ICitizen);
                    break;
                case PersonType.Criminal:
                    this.criminals.Remove(person as ICriminal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IPerson ReturnCharacterById(int id)
        {
            // TODO: throw exception when not found
            return this.units.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveCharacterById(int id)
        {
            var person = this.units.FirstOrDefault(p => p.Id == id);
            this.RemoveCharacter(person);
        }

        public void SendPolicemanToMission(IPerson target)
        {
            IPoliceman policeman = null;
            IRoute route = null;
            int minDistance = int.MaxValue;

            foreach (var person in this.police)
            {
                if (person.IsAlive && !person.IsOnMission)
                {
                    route = MapUtils.FindShortestRoute(this.Map, person.Position, target.Position);

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
            IParamedic paramedic = null;
            IRoute route = null;
            int minDistance = int.MaxValue;

            foreach (var person in this.paramedics)
            {
                if (person.IsAlive && !person.IsOnMission)
                {
                    route = MapUtils.FindShortestRoute(this.Map, person.Position, target.Position);

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
            Validator.ValidateNull(person, "Person cannot be null.");

            person.Injury = injury;
        }

        public IList<string> Reports()
        {
            var result = new List<string>();

            foreach (var policeman in this.police)
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
