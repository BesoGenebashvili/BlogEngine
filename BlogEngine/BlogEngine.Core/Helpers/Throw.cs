using System;
using BlogEngine.Core.Exceptions;

namespace BlogEngine.Core.Helpers
{
    public static class Throw
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
    }
}