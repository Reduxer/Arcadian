using Microsoft.AspNetCore.Mvc.Filters;
using Arcadian.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Arcadian.WebAPI.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private Dictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>()
            {
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(RequestValidationException), HandleValidationException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var t = context.Exception.GetType();

            if (_exceptionHandlers.ContainsKey(t))
            {
                var handler = _exceptionHandlers[t];
                handler.Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var ex = context.Exception as RequestValidationException;

            var details = new ValidationProblemDetails(ex!.Errors);

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var ex = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Title = "The specified resource was not found.",
                Detail = ex!.Message
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var problem = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occured while processing your request"
            };

            context.Result = new BadRequestObjectResult(problem);
            context.ExceptionHandled = true;
        }
    }
}
