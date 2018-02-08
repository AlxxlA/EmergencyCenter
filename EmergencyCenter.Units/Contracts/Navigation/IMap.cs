using System.Collections.Generic;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Contracts.Navigation
{
    public interface IMap
    {
        int this[int x, int y] { get; }

        int this[Position position] { get; }

        int MaxPositionX { get; }

        int MaxPositionY { get; }

        void ValidatePosition(Position position);

        bool IsValidPosition(Position position);

        bool IsValidPosition(int x, int y);

        IEnumerable<Position> PositionNeighbours(Position position);
    }
}
