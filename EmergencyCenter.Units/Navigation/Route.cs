using System;
using System.Collections.Generic;
using EmergencyCenter.Units.Contracts.Navigation;

namespace EmergencyCenter.Units.Navigation
{
    public class Route : IRoute
    {
        private const string PositionsCannotBeNullMessage = "List of positions cannot be null.";

        private readonly IList<Position> positions;
        private int currentPositionIndex;

        public Route()
        {
            this.positions = new List<Position>();
        }

        public Route(IEnumerable<Position> positions)
        {
            if (positions == null)
            {
                throw new ArgumentNullException(PositionsCannotBeNullMessage);
            }
            this.positions = new List<Position>(positions);
        }

        public Position CurrentPosition
        {
            get
            {
                this.CheckForNullOrEmpty();
                return this.positions[this.currentPositionIndex];
            }
        }

        public Position LastPosition
        {
            get
            {
                this.CheckForNullOrEmpty();
                return this.positions[this.positions.Count - 1];
            }
        }

        public int Length => this.positions.Count;

        public void AddPosition(Position position)
        {
            this.positions.Add(position);
        }

        /// <summary>
        /// Return next position or last position if current position is the last one.
        /// </summary>
        /// <returns></returns>
        public Position NextPosition()
        {
            this.CheckForNullOrEmpty();
            if (this.currentPositionIndex == this.positions.Count - 1)
            {
                return this.LastPosition;
            }

            this.currentPositionIndex++;
            return this.positions[this.currentPositionIndex];
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.positions);
        }

        private void CheckForNullOrEmpty()
        {
            if (this.positions == null)
            {
                throw new NullReferenceException("Route positions are null.");
            }
            if (this.positions.Count == 0)
            {
                throw new NullReferenceException("Route positions are empty.");
            }
        }
    }
}
