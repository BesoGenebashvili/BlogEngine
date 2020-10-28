using System;
using BlogEngine.Core.Exceptions;

namespace BlogEngine.Core.Helpers
{
    public static partial class Throw
    {
        public static void EntityNotFoundException(string entityName)
        {
            throw new EntityNotFoundException(entityName);
        }

        public static void ArgumentNullException(string paramName)
        {
            throw new ArgumentNullException(paramName);
        }

        public static void InvalidOperationException()
        {
            throw new InvalidOperationException();
        }

        public static void InvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }
    }
}