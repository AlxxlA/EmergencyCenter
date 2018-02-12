using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Contracts.Random;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Characters.Civils
{
    public class Citizen : Person, ICitizen
    {
        private readonly string[] directions = { "up", "down", "left", "right" };
        private string currentDirection;
        private int stepsInDirection;

        public Citizen(string name, int health, int strength, Position position, IMap map, IRandomGenerator random)
            : base(name, health, strength, position, map, PersonType.Citizen, random)
        {
        }

        protected Citizen(string name, int health, int strength, Position position, IMap map, IRandomGenerator random, PersonType personType = PersonType.Citizen)
            : base(name, health, strength, position, map, personType, random)
        {
        }

        public override void Update()
        {
            if (!this.IsInjured)
            {
                this.Walk();
            }
            base.Update();
        }

        private void Walk()
        {
            if (this.stepsInDirection == 0 || string.IsNullOrEmpty(this.currentDirection))
            {
                int nextDirection = this.Random.Next(0, this.directions.Length);
                this.currentDirection = this.directions[nextDirection];
                int stepsCount = this.Random.Next(1, Math.Max(this.Map.MaxPositionX, this.Map.MaxPositionY));
                this.stepsInDirection = stepsCount;
            }
            Position nextPosition;
            switch (this.currentDirection)
            {
                case "up":
                    nextPosition = new Position(this.Position.X - 1, this.Position.Y);
                    if (this.Position.X == 0 || this.Map[nextPosition] != 0)
                    {
                        this.stepsInDirection = 0;
                    }
                    else
                    {
                        this.Position = nextPosition;
                        this.stepsInDirection--;
                    }
                    break;
                case "down":
                    nextPosition = new Position(this.Position.X + 1, this.Position.Y);
                    if (this.Position.X >= this.Map.MaxPositionX - 1 || this.Map[nextPosition] != 0)
                    {
                        this.stepsInDirection = 0;
                    }
                    else
                    {
                        this.Position = nextPosition;
                        this.stepsInDirection--;
                    }
                    break;
                case "left":
                    nextPosition = new Position(this.Position.X, this.Position.Y - 1);
                    if (this.Position.Y == 0 || this.Map[nextPosition] != 0)
                    {
                        this.stepsInDirection = 0;
                    }
                    else
                    {
                        this.Position = nextPosition;
                        this.stepsInDirection--;
                    }
                    break;
                case "right":
                    nextPosition = new Position(this.Position.X, this.Position.Y + 1);
                    if (this.Position.Y >= this.Map.MaxPositionY - 1 || this.Map[nextPosition] != 0)
                    {
                        this.stepsInDirection = 0;
                    }
                    else
                    {
                        this.Position = nextPosition;
                        this.stepsInDirection--;
                    }
                    break;
                default: throw new ArgumentException("Invalid walk direction.");
            }
        }
    }
}
