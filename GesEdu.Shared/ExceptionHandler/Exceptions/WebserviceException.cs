using GesEdu.Shared.WebserviceModels;
using System.Net;

namespace GesEdu.Shared.ExceptionHandler.Exceptions
{
    public class WebserviceException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string?[] Messages { get; set; }

        public WebserviceException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Messages = [message];
        }

        public WebserviceException(HttpStatusCode statusCode, string[] messages)
        {
            StatusCode = statusCode;
            Messages = messages;
        }
    }
}
