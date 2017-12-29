using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public abstract class Person
    {
        protected const int MaxHealth = 100;
        private static int idCounter = 1;
        private string name;
        private readonly int id;
        private int health;
        private int strength;
        private Position position;

        protected Person(string name, int health, int strength, Position position, Map map, PersonType personType)
        {
            this.Name = name;
            this.Health = health;
            this.Strength = strength;
            this.Map = map;
            this.Position = position;
            this.PersonType = personType;
            this.Injury = InjuryType.None;
            this.id = idCounter;
            idCounter++;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Person name shoud not be null or empty.");
                }
                this.name = value;
            }
        }

        public int ID
        {
            get { return this.id; }
        }

        public int Health
        {
            get { return this.health; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Health cannot be less the zero.");
                }
                this.health = value;
            }
        }

        public int Strength
        {
            get { return this.strength; }
            set { this.strength = value; }
        }

        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public PersonType PersonType { get; set; }

        public InjuryType Injury { get; set; }

        public bool IsInjured
        {
            get { return this.Injury != InjuryType.None; }
        }

        public bool IsAlive
        {
            get { return this.Health > 0; }
        }

        protected Map Map { get; }

        public virtual void Update()
        {
            if (this.IsInjured)
            {
                switch (this.Injury)
                {
                    case InjuryType.Bruise:
                        this.Health = Math.Max(0, this.Health - 5); // damage cannot be more then current health
                        break;
                    case InjuryType.Wound:
                        this.Health = Math.Max(0, this.Health - 10);
                        break;
                    case InjuryType.LargeFracture:
                        this.Health = Math.Max(0, this.Health - 20);
                        break;
                    case InjuryType.None: // no injury
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Invalid injury type.");
                }
            }
        }
    }
}