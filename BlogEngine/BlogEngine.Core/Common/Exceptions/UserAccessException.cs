using System;

namespace BlogEngine.Core.Common.Exceptions
{
    public class UserAccessException : Exception
    {
        public UserAccessException(string message) : base($"Illegal access. {message}")
        {
        }
    }
}