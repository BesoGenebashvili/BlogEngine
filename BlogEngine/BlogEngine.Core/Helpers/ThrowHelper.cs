using System;
using BlogEngine.Core.Exceptions;

namespace BlogEngine.Core.Helpers
{
    public static class ThrowHelper
    {
        public static void ThrowEntityAlreadyExistsException(object id)
        {
            throw new EntityAlreadyExistsException(id);
        }

        public static void ThrowEntityAlreadyExistsException(object id, Exception innerException)
        {
            throw new EntityAlreadyExistsException(id, innerException);
        }

        public static void ThrowEntityNotFoundException(string entityName)
        {
            throw new EntityNotFoundException(entityName);
        }
        public static void ThrowEntityNotFoundException(string entityName, Exception innerException)
        {
            throw new EntityNotFoundException(entityName, innerException);
        }
    }
}