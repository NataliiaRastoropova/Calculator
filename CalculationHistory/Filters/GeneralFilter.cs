using System.Net;
using Calculator.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CalculationHistory.Filters
{
    public sealed class GeneralFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DataNotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            else if (context.Exception is BadArgumentException)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            } 
            else
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.WriteAsync($"{{\"Error\": \"{context.Exception.Message}\" }}");
        }
    }
}
