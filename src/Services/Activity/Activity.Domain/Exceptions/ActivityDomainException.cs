using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Domain.Exceptions
{
    public class ActivityDomainException
        : Exception
    {
        public ActivityDomainException()
        {
        }

        public ActivityDomainException(string message)
            : base(message) { }

        public ActivityDomainException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
