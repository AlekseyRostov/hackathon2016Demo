using System;
using System.Net;

namespace Feedback.Core.Services.Implementation
{
    public class WebRequestException : Exception
    {
        public WebRequestException(HttpStatusCode statusCode, string response)
        {
            StatusCode = statusCode;
            Response = response;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Response { get; set; }
    }
}