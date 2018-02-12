using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Contracts.Random;
using EmergencyCenter.Units.Navigation;
using EmergencyCenter.Units.Navigation.Enums;

namespace EmergencyCenter.Units.Characters
{
    public abstract class Person : IPerson
    {
        private const string InvalidNameMessage = "Name value cannot be null or empty.";
        private const string InvalidHealthMessage = "Health cannot be less then {0} or greater then {1}.";
        private const string InvalidStrengthMessage = "Strength cannot be less then {0} or greater then {1}.";
        private const string InvalidMapMessage = "Map cannot be null.";
        private const string InvalidRandomGeneratorMessage = "Random generator cannot be null.";
        private const string InvalidPossitionMessage = "Given position is invalid.";

        protected const int MinHealth = 0;
        protected const int MaxHealth = 100;

        protected const int MinStrength = 0;
        protected const int MaxStrength = 100;

        private static int idCounter = 1;

        private string name;
        private int health;
        private int strength;
        private Position position;

        protected Person(string name, int health, int strength, Position position, IMap map, PersonType personType, IRandomGenerator random)
        {
            this.Name = name;
            this.Health = health;
            this.Strength = strength;
            this.Map = map ?? throw new ArgumentNullException(InvalidMapMessage);
            this.Position = position;
            this.PersonType = personType;
            this.Random = random ?? throw new ArgumentNullException(InvalidRandomGeneratorMessage);
            this.Injury = InjuryType.None;
            this.Id = idCounter;
            idCounter++;
        }

        public int Id { get; }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(InvalidNameMessage);
                }
                this.name = value;
            }
        }

        public int Health
        {
            get => this.health;
            set
            {
                var invalidMessage = string.Format(InvalidHealthMessage, MinHealth, MaxHealth);
                if (value < MinHealth || value > MaxHealth)
                {
                    throw new ArgumentException(invalidMessage);
                }

                this.health = value;
            }
        }

        public int Strength
        {
            get => this.strength;
            protected set
            {
                var invalidMessage = string.Format(InvalidStrengthMessage, MinStrength, MaxStrength);
                if (value < MinStrength || value > MaxStrength)
                {
                    throw new ArgumentException(invalidMessage);
                }
                this.strength = value;
            }
        }

        public Position Position
        {
            get => this.position;
            set
            {
                this.Map?.ValidatePosition(value);
                if (this.Map?[value] != (int)MapTileType.Street)
                {
                    throw new ArgumentException(InvalidPossitionMessage);
                }
                this.position = value;
            }
        }

        public PersonType PersonType { get; }

        public InjuryType Injury { get; set; }

        public bool IsInjured => this.Injury != InjuryType.None;

        public bool IsAlive => this.Health > 0;

        public IMap Map { get; }

        protected IRandomGenerator Random { get; }

        public virtual void Update()
        {
            this.Health = Math.Max(0, this.Health - (int)this.Injury); // damage cannot be more then current health
        }
    }
}
