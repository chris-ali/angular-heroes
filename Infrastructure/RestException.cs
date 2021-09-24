using System;
using System.Net;

namespace angular_heroes.Infrastructure
{
    public class RestException : Exception
    {
        public object Errors { get; set; } 
        public HttpStatusCode Code { get; }

        public RestException(HttpStatusCode code, object errors = null)
        {
            Errors = errors;
            Code = code;
        }
    }
}