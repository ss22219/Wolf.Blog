using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Zaaby.Abstractions;

namespace BlogWeb
{
    internal class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode >= 400)
                {
                    var statusCode = context.Response.StatusCode;
                    context.Response.StatusCode = 200;
                    await HandleExceptionAsync(context, statusCode,
                        new ZaabyException($"{context.Request.Path.Value} httpStatus:{statusCode}"));
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, context.Response.StatusCode, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int httpStatusCode, Exception ex)
        {
            var innerEx = ex;
            while (innerEx.InnerException != null)
                innerEx = innerEx.InnerException;
            var message = innerEx.Message;
            var result = JsonConvert.SerializeObject(new {code = 200, message});
            context.Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}