using System;
using System.Runtime.Serialization;

namespace WebMVC.Infrastructure.Exceptions
{
    public class ApiRequestExcption
        : Exception
    {
        public int Code { get; set; }

        public ApiRequestExcption()
        {

        }

        public ApiRequestExcption(int code) : this(code, string.Empty)
        {
            Code = code;
        }
        public ApiRequestExcption(string message) : this(0, message)
        {

        }

        public ApiRequestExcption(int code, string message) : base(message)
        {
            Code = code;
        }

        public ApiRequestExcption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiRequestExcption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
