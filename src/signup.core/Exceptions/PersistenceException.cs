using System;

namespace SignUp.core.Exceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
