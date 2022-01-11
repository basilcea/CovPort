using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Extension;
// using Application.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var statusCode = StatusCodes.Status500InternalServerError;
                var message = "Internal Server Error";
                switch (context.Exception)
                {
                    // case DuplicateTransactionException _:
                    //     statusCode = StatusCodes.Status409Conflict;
                    //     message = context.Exception.Message;
                    //     break;
                    // case InvalidAccountException _:
                    //     statusCode = StatusCodes.Status403Forbidden;
                    //     message = context.Exception.Message;
                    //     break;
                }

                context.Result = new JsonResult(context.Exception.ToApiResponse(message))
                {
                    StatusCode = statusCode
                };
            }
        }
    }

    public static class ExceptionExtensions
    {
        public static ApiResponse<object> ToApiResponse(this Exception exception, string message)
        {
            return new ApiResponse<object>(false, message, new List<string> { exception.Message });
        }
    }
}
