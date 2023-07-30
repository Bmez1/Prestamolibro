using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Middleware
{
    public class HandleError
    {
        private readonly ILogger<HandleError> _logger;
        private readonly RequestDelegate _next;

        public HandleError(RequestDelegate next, ILogger<HandleError> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Title = "Ha ocurrido un error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message
            };
            problemDetails.Extensions.Add("RequestId", context.TraceIdentifier);

            string response = JsonConvert.SerializeObject(problemDetails);

            return context.Response.WriteAsync(response);
        }
    }
}