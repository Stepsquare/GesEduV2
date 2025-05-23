﻿using System.Net.Mime;
using System.Net;
using System.Text.Json;
using GesEdu.Shared.ExceptionHandler.Exceptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GesEdu.Shared.ExceptionHandler
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly ITempDataDictionaryFactory _tempDataFactory;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment, ITempDataDictionaryFactory tempDataFactory)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
            _tempDataFactory = tempDataFactory;
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

            var response = new ErrorModel(ex.StatusCode, ex.Messages);

            context.Response.StatusCode = (int)ex.StatusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            if (IsAjaxRequest(context.Request))
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(json);
            }
            else
            {
                try
                {
                    context.Session.SetString("ErrorMessages", json);

                    // Redireciona para a página anterior
                    var referer = context.Request.Headers["Referer"].ToString();
                    context.Response.Redirect(string.IsNullOrEmpty(referer) ? "/" : referer);
                }
                catch (Exception)
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(json);
                }
            }
        }

        private async Task HandleDefaultExceptionResponseAsync(HttpContext context, Exception ex)
        {
            var stackTrace = new StackTrace(ex, fNeedFileInfo: true);
            var firstFrame = stackTrace.FrameCount > 0 ? stackTrace.GetFrame(0) : null;

            var exceptionType = ex.GetType().ToString();
            var message = ex.Message;
            var fileName = firstFrame?.GetFileName();
            var fileLineNumber = firstFrame?.GetFileLineNumber();

            _logger.LogError("{@exceptionType} - {@message} ({@fileName} at line {@fileLineNumber})", exceptionType, message, fileName, fileLineNumber);
            
            var response = new ErrorModel(HttpStatusCode.InternalServerError, _environment.IsDevelopment() ? ex.Message : "Ocorreu um erro. Contactar suporte.");
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            if (IsAjaxRequest(context.Request))
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(json);
            }
            else
            {
                try
                {
                    context.Session.SetString("ErrorMessages", json);

                    // Redireciona para a página anterior
                    var referer = context.Request.Headers["Referer"].ToString();
                    context.Response.Redirect(string.IsNullOrEmpty(referer) ? "/" : referer);
                }
                catch (Exception)
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(json);
                }
            }
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            return request.Headers.XRequestedWith == "XMLHttpRequest";
        }
    }
}
