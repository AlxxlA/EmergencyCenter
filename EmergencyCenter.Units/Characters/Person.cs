using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Map;

namespace EmergencyCenter.Units.Characters
{
    public abstract class Person
    {
        private static int idCounter = 1;
        private string name;
        private readonly int id;
        private int health;
        private int strength;
        private Position position;

        protected Person(string name, int health, int strength, Position position)
        {
            this.Name = name;
            this.Health = health;
            this.Strength = strength;
            this.Position = position;
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
            set { this.health = value; }
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

        public bool IsAlive
        {
            get { return this.Health > 0; }
        }

        public abstract void Update();
    }
}