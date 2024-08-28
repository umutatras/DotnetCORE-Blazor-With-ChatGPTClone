using ChatGPTClone.Application.Common.Models.Errors;
using ChatGPTClone.Application.Common.Models.General;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatGPTClone.WebApi.Filters
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
            _logger.LogError(context.Exception,context.Exception.Message);
            context.ExceptionHandled = true;
            if (context.Exception is ValidationException)
            {
                var exception = context.Exception as ValidationException;
                var responseMessage = "One or more validation errors occurred.";
                List<ErrorDto> errors = new();
                var propertyNames = exception.Errors.Select(s => s.PropertyName).Distinct();

                foreach (var propertyName in propertyNames)
                {
                    var messages = exception.Errors
                        .Where(e => e.PropertyName == propertyName)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    errors.Add(new ErrorDto(propertyName, messages));
                }

                context.Result=new BadRequestObjectResult(new ResponseDto<string>(responseMessage, errors))
                {
                    StatusCode=StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
