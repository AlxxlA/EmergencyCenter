namespace EmergencyCenter.Units.Maps
{
    public struct Position
    {
        public bool Equals(Position other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Position position && this.Equals(position);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X * 397) ^ this.Y;
            }
        }

        public Position(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !p1.Equals(p2);
        }

        public override string ToString()
        {
            return $"X = {this.X} Y = {this.Y}";
        }
    }
}