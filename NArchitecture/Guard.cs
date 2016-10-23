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
                string message = string.Format(Properties.Resources.ArgumentEmptyException, argumentName);
                throw new ArgumentException(message, argumentName);
            }
        }
    }
}
