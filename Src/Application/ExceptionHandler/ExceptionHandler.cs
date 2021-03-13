using System;
using System.Net;

namespace Application.ExceptionHandler
{
    public class ExceptionHandler : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object Errors { get; }

        public ExceptionHandler(HttpStatusCode statusCode, object errors = null)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
