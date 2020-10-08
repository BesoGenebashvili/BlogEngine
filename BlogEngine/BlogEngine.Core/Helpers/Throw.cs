using System;
using BlogEngine.Core.Exceptions;

namespace BlogEngine.Core.Helpers
{
    public static partial class Throw
    {
        public static void EntityAlreadyExistsException(object id)
        {
            throw new EntityAlreadyExistsException(id);
        }

        public static void EntityAlreadyExistsException(object id, Exception innerException)
        {
            throw new EntityAlreadyExistsException(id, innerException);
        }

        public static void EntityNotFoundException(string entityName)
        {
            throw new EntityNotFoundException(entityName);
        }

        public static void EntityNotFoundException(string entityName, Exception innerException)
        {
            throw new EntityNotFoundException(entityName, innerException);
        }

        public static void EntityNullException(string entityName)
        {
            throw new EntityNullException(entityName);
        }

        public static void EntityNullException(string entityName, Exception innerException)
        {
            throw new EntityNullException(entityName, innerException);
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

        public static void InvalidOperationException(string message, Exception innerException)
        {
            throw new InvalidOperationException(message, innerException);
        }
    }
}