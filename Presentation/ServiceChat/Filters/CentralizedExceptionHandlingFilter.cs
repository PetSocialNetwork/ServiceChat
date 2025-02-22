using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ServiceChat.WebApi.Models.Responses;
using ServiceChat.Domain.Exceptions;
namespace ServiceChat.WebApi.Filters
{
    public class CentralizedExceptionHandlingFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var (message, statusCode) = TryGetUserMessageFromException(context);

            if (message != null && statusCode != 0)
            {
                context.Result = new ObjectResult(new ErrorResponse(message, statusCode))
                {
                    StatusCode = statusCode
                };
                context.ExceptionHandled = true;
            }
        }

        private (string?, int) TryGetUserMessageFromException(ExceptionContext context)
        {
            return context.Exception switch
            {
                ChatAlreadyExistsException => ("Чат с данным пользователем уже существует.", StatusCodes.Status400BadRequest),
                ChatNotFoundException => ("Чат не существует.", StatusCodes.Status400BadRequest),
                MessageNotFoundException => ("Сообщение не существует.", StatusCodes.Status400BadRequest),
                Exception => ("Неизвестная ошибка.", StatusCodes.Status500InternalServerError),
                _ => (null, 0)
            };
        }
    }
}
