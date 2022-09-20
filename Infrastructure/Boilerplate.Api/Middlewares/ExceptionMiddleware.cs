using Boilerplate.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Boilerplate.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case DummyException ex:

                    _logger.LogError("Dummy exception occurred. Error message: '{Message}'  Exception stack trace: '{StackTrace}'", ex.Message, ex.StackTrace);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    break;
                default:

                    _logger.LogError("An error occurred. Exception Message: '{Message}'", error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    break;
            }

            var result = JsonConvert.SerializeObject(new { message = error.Message });
            await response.WriteAsync(result);
        }
    }
}