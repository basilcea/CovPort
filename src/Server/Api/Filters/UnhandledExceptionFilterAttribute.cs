using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Extension;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Api.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<UnhandledExceptionFilterAttribute> _logger;

        public UnhandledExceptionFilterAttribute(ILogger<UnhandledExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var statusCode = StatusCodes.Status500InternalServerError;
                switch (context.Exception)
                {
                    case BadRequestException _:
                        statusCode = StatusCodes.Status400BadRequest;
                        break;
                    case UnauthorizedException _:
                        statusCode = StatusCodes.Status401Unauthorized;
                        break;
                    case NotFoundException _:
                        statusCode = StatusCodes.Status404NotFound;
                        break;
                }
                
                context.Result = new JsonResult(context.Exception.ToApiResponse(context.HttpContext.Request))
                {
                    StatusCode = statusCode
                };
                _logger.LogError("Received request errors: {@Errors} when calling {Request.Path}", context.Exception.Message, context.HttpContext.Request.Path);

            }
            return Task.CompletedTask;
        }
    }

    public static class ExceptionExtensions
    {
        public static ApiResponse<object> ToApiResponse(this Exception exception, HttpRequest request)
        {
            var message = $"{request.Method} {request.Path} Request Failed";
            return new ApiResponse<object>(new List<string> { exception.Message }, message);
        }

    }

}
