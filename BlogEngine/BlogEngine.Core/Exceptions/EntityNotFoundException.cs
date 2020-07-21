using System;

namespace BlogEngine.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName) 
            : base($"The entity with a name = '{entityName}' was not found in the Database")
        {
        }

        public EntityNotFoundException(string entityName, Exception innerException)
            : base($"The entity with a name = '{entityName}' was not found in the Database", innerException)
        {
        }
    }
}