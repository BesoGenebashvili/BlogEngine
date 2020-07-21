using System;

namespace BlogEngine.Core.Exceptions
{
    class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(object id)
            : base($"The entity with an ID = '{id}' already exists in the Database")
        {
        }

        public EntityAlreadyExistsException(object id, Exception innerException)
            : base($"The entity with an ID = '{id}' already exists in the Database", innerException)
        {
        }
    }
}