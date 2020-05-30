using System;
using System.Collections.Generic;
using System.Text;

namespace Together.BuildingBlocks.Domain
{
    // <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException()
        { }

        public DomainException(string message)
            : base(message)
        { }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
