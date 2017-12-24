using System;
using System.Collections.Generic;

namespace EmergencyCenter.Units.Maps
{
    public class Route
    {
        private int currentPositionIndex;

        public Route()
        {
            this.Positions = new List<Position>();
        }

        public IList<Position> Positions { get; set; }

        public Position CurrentPosition
        {
            get
            {
                this.CheckForNullOrEmpty();
                return this.Positions[this.currentPositionIndex];
            }
        }

        public Position LastPosition
        {
            get
            {
                this.CheckForNullOrEmpty();
                return this.Positions[this.Positions.Count - 1];
            }
        }

        public void AddPosition(Position position)
        {
            this.Positions.Add(position);
        }

        public void RemoveLastPosition()
        {
            this.CheckForNullOrEmpty();
            int index = this.Positions.Count - 1;
            this.Positions.RemoveAt(index);
        }

        /// <summary>
        /// Return next position or last position if current position is the last one.
        /// </summary>
        /// <returns></returns>
        public Position NextPosition()
        {
            this.CheckForNullOrEmpty();
            if (this.currentPositionIndex == this.Positions.Count - 1)
            {
                return this.LastPosition;
            }

            this.currentPositionIndex++;
            return this.Positions[this.currentPositionIndex];
        }

        private void CheckForNullOrEmpty()
        {
            if (this.Positions == null)
            {
                throw new NullReferenceException("Route positions are null.");
            }
            if (this.Positions.Count == 0)
            {
                throw new NullReferenceException("Route positions are empty.");
            }
        }
    }
}