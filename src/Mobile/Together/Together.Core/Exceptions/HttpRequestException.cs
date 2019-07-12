using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Core.Exceptions
{
    public class HttpRequestException : System.Net.Http.HttpRequestException
    {
        public System.Net.HttpStatusCode HttpCode { get; }
        public HttpRequestException(System.Net.HttpStatusCode code) : this(code, null, null)
        {
        }

        public HttpRequestException(System.Net.HttpStatusCode code, string message) : this(code, message, null)
        {
        }

        public HttpRequestException(System.Net.HttpStatusCode code, string message, Exception inner) : base(message,
            inner)
        {
            HttpCode = code;
        }

    }
}
