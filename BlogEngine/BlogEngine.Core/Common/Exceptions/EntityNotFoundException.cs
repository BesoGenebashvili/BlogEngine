using System;

namespace BlogEngine.Core.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName)
            : base($"The entity with a name = '{entityName}' was not found in the Database")
        {
        }
    }
}