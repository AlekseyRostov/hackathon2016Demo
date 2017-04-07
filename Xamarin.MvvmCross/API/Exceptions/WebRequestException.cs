using System;
using System.Net;

namespace Feedback.API.Exceptions
{
    public class WebRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Response { get; set; }

        public WebRequestException(HttpStatusCode statusCode, string response)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}