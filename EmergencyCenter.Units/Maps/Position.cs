namespace EmergencyCenter.Units.Maps
{
    public struct Position
    {
        public Position(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

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