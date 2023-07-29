using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FelineInquiry.Application.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {

            _logger = logger;

        }
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            _logger.LogCritical(ex.InnerException != null ? ex.InnerException.Message : context.Exception.Message);

            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true;
            context.Result = new JsonResult(new 
            { 
                Error = "Internal Server Error!",
                ErrorDescription = "An error occurred. Please try again later.",
                IsError = true
            });
        }
    }
}
