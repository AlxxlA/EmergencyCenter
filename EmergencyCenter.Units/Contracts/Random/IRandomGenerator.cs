namespace EmergencyCenter.Units.Contracts.Random
{
    public interface IRandomGenerator
    {
        int Next(int minValue, int maxValue);
    }
}
