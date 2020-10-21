using DDB.HitPointManager.API.Models;
using DDB.HitPointManager.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DDB.HitPointManager.API.CustomExceptionMiddleware
{
    // Reference: https://code-maze.com/global-error-handling-aspnetcore/
    public class ExceptionMiddleware
    {
        // TODO - Inject a logger

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            // Set default
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal Server Error";

            // Custom error handling
            if (exception is ResourceNotFoundException notFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                message = notFoundException.Message;
            }
            else if (exception is HttpStatusException httpException)
            {
                context.Response.StatusCode = (int)httpException.Status;
                message = httpException.Message;
            }

            return context.Response.WriteAsync(new ErrorDetail
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
