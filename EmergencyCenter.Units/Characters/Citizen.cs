using System;
using EmergencyCenter.Units.Characters.Contracts;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public class Citizen : Person, ICitizen
    {
        private readonly string[] directions = { "up", "down", "left", "right" };
        private string currentDirection;
        private int stepsInDirection;

        public Citizen(string name, int health, int strength, Position position, Map map)
            : base(name, health, strength, position, map, PersonType.Citizen)
        {
        }

        protected Citizen(string name, int health, int strength, Position position, Map map, PersonType personType = PersonType.Citizen)
            : base(name, health, strength, position, map, personType)
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
            var rnd = new Random();
            if (this.stepsInDirection == 0 || string.IsNullOrEmpty(this.currentDirection))
            {
                int nextDirection = rnd.Next(0, this.directions.Length);
                this.currentDirection = this.directions[nextDirection];
                int stepsCount = rnd.Next(1, Math.Max(this.Map.Rows, this.Map.Cols));
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
                    if (this.Position.X >= this.Map.Rows - 1 || this.Map[nextPosition] != 0)
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
                    if (this.Position.Y >= this.Map.Cols - 1 || this.Map[nextPosition] != 0)
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