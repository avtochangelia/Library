#nullable disable

using Application.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Application.Shared.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            var errorTypeName = error.GetType().Name;
            response.ContentType = "application/json";

            switch (errorTypeName)
            {
                case nameof(ApiException):
                    await HandleApiException(error as ApiException, response);
                    break;
                case nameof(KeyNotFoundException):
                    await HandleKeyNotFoundException(error as KeyNotFoundException, response);
                    break;
                case nameof(ValidationException):
                    await HandleValidationException(error as ValidationException, response);
                    break;
                default:
                    await HandleUnknownException(error, response);
                    break;
            }
        }
    }

    private async Task HandleValidationException(ValidationException exception, HttpResponse response)
    {
        await HandleException(response, HttpStatusCode.BadRequest, exception.Failures.SelectMany(failure => failure.Value), "Validation error");
    }

    private async Task HandleKeyNotFoundException(KeyNotFoundException exception, HttpResponse response)
    {
        await HandleException(response, HttpStatusCode.NotFound, new List<string> { exception.Message }, "Key not found error");
    }

    private async Task HandleApiException(ApiException exception, HttpResponse response)
    {
        await HandleException(response, exception.HttpStatusCode, exception.ErrorMessages, "Bad Request error");
    }

    private async Task HandleUnknownException(Exception exception, HttpResponse response)
    {
        await HandleException(response, HttpStatusCode.InternalServerError, GetInnerExceptions(exception).Select(e => e.Message), "Internal Server Error");
    }

    private async Task HandleException(
        HttpResponse response,
        HttpStatusCode httpStatusCode,
        IEnumerable<string> errors,
        string message = "Internal System Error")
    {
        var responseModel = new BaseResponse(message, httpStatusCode) { Errors = errors?.ToList() };
        response.StatusCode = (int)httpStatusCode;

        var result = JsonSerializer.Serialize(responseModel);
        _logger.LogError($"Exception: {result}");
        await response.WriteAsync(result);
    }

    private static IEnumerable<Exception> GetInnerExceptions(Exception ex)
    {
        ArgumentNullException.ThrowIfNull(ex);

        var innerException = ex;

        do
        {
            yield return innerException;
            innerException = innerException.InnerException;
        }
        while (innerException != null);
    }
}