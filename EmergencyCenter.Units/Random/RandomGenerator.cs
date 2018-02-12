using EmergencyCenter.Units.Contracts.Random;

namespace EmergencyCenter.Units.Random
{
    public class RandomGenerator : IRandomGenerator
    {
        readonly System.Random generator;

        public RandomGenerator()
        {
            this.generator = new System.Random();
        }

        public int Next(int minValue, int maxValue)
        {
            return this.generator.Next(minValue, maxValue);
        }
    }
}
