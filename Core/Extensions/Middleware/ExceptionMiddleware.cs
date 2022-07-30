using Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Core.Extensions.Middleware
{
    public class ExceptionMiddleware
    {
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
            catch (Exception e)
            {
                await HandleException(httpContext, e);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal server error";

            if (e.GetType() == typeof(ValidationException))
            {
                var errors = ((ValidationException)e).Errors;
                message = string.Join(",", errors.Select(x => x.ErrorMessage));
            }
            await httpContext.Response.WriteAsync(
                    JsonConvert.SerializeObject(new ErrorResult(message))
                    );
        }
    }
}
