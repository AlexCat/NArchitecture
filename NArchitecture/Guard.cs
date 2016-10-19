using System;

namespace NArchitecture
{
    public static class Guard
    {
        public static void AgainstNull(string argumentName, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AgainstEmptyString(string argumentName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Cannot be null or empty", argumentName);
            }
        }
    }
}
