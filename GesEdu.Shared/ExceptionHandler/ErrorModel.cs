using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static GesEdu.Shared.WebserviceModels.GenericPostResponse;

namespace GesEdu.Shared.ExceptionHandler
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string? StatusCodeDescription { get; set; }
        public string?[] Messages { get; set; }

        public ErrorModel(HttpStatusCode statusCode, string? message) : this(statusCode, new string?[] { message })
        {
        }

        public ErrorModel(HttpStatusCode statusCode, string?[] messages) 
        {
            StatusCode = (int)statusCode;
            StatusCodeDescription = statusCode.ToString();
            Messages = messages;
        }
    }
}
