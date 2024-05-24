using System.Net.Mime;
using System.Net;
using System.Text.Json;
using GesEdu.Shared.ExceptionHandler.Exceptions;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace GesEdu.Shared.ExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (WebserviceException ex)
            {
                await HandleWebserviceExceptionResponseAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleDefaultExceptionResponseAsync(context, ex);
            }
        }

        private async Task HandleWebserviceExceptionResponseAsync(HttpContext context, WebserviceException ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)ex.StatusCode;

            var response = new ErrorModel(ex.StatusCode, ex.Messages);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }

        private async Task HandleDefaultExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorModel(HttpStatusCode.InternalServerError, _environment.IsDevelopment() ? ex.Message : "Ocorreu um erro. Contactar suporte.");
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}
