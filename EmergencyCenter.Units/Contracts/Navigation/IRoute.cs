using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Units.Contracts.Navigation
{
    public interface IRoute
    {
        Position CurrentPosition { get; }

        Position LastPosition { get; }

        int Length { get; }

        void AddPosition(Position position);

        Position NextPosition();
    }
}
