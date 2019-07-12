using System;

namespace Together.Core.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public ServiceAuthenticationException(string message) : base(message)
        {
        }
    }
}
