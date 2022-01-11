using System.Collections.Generic;
using System.Linq;
using Api.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Api.Filters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger<ValidationFilterAttribute> _logger;

        public ValidationFilterAttribute(ILogger<ValidationFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var responseBody = context.ModelState.ToApiResponse();
                context.Result =
                    new JsonResult(responseBody)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                _logger.LogWarning("Received model errors: {@Errors} when calling {Request.Path}", responseBody.Errors, context.HttpContext.Request.Path);
            }

            base.OnActionExecuting(context);
        }
    }

    public static class ModelStateExtensions
    {
        public static ApiResponse<object> ToApiResponse(this ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var key in modelState.Keys)
            {
                errors.AddRange(modelState[key].Errors.Select(error => error.ErrorMessage));
            }

            return new ApiResponse<object>(false, "Validation failed", errors);
        }
    }
}
