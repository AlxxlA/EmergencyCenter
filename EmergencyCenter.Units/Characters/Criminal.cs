﻿using System;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Units.Characters
{
    public class Criminal : Citizen
    {
        private int movesToEscape;

        public Criminal(string name, int health, int strength, Position position, Map map)
            : base(name, health, strength, position, map, PersonType.Criminal)
        {
        }

        public bool IsEscape { get; private set; }

        public override void Update()
        {
            var rnd = new Random();
            bool isRun = rnd.Next(0, 101) < 10;

            if (!this.IsAlive || !isRun) return;

            base.Update();
            this.movesToEscape++;
            if (this.movesToEscape >= 10)
            {
                this.IsEscape = true;
            }
        }
    }
}