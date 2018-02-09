namespace EmergencyCenter.Validation
{
    public interface IValidator
    {
        void ValidateIntRange(int value, int min, int max, string message);

        void ValidateDecimalRange(decimal value, decimal min, decimal max, string message);

        void ValidateNull(object value, string message);

        void ValidateStringEmpty(string value, string message);

        void ValidateStringNullOrEmpty(string value, string message);

        void ValidateSymbols(string value, string pattern, string message);

        void ValidateFilePath(string path, string message);
    }
}
