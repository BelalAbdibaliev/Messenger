using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Services;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IWebHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception ex,
        CancellationToken cancellationToken)
    {
        logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);

        var (statusCode, message) = ex switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, ex.Message),
            ArgumentException => (StatusCodes.Status400BadRequest, ex.Message),
            ValidationException => (StatusCodes.Status422UnprocessableEntity, ex.Message),
            InvalidOperationException => (StatusCodes.Status409Conflict, ex.Message),
            TimeoutException => (StatusCodes.Status504GatewayTimeout, ex.Message),
            NotImplementedException => (StatusCodes.Status501NotImplemented, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, 
                  environment.IsDevelopment() 
                      ? ex.Message 
                      : "An unexpected error occurred while processing your request.")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = message,
            Type = ex.GetType().Name,
            Instance = context.Request.Path,
            Detail = environment.IsDevelopment() ? ex.StackTrace : null
        };
        
        problemDetails.Extensions.Add("environment", environment.EnvironmentName);
        problemDetails.Extensions.Add("requestId", context.TraceIdentifier);
        problemDetails.Extensions.Add("timestamp", DateTime.UtcNow);
        
        if (environment.IsDevelopment())
        {
            problemDetails.Extensions.Add("exception", new
            {
                message = ex.Message,
                ex.StackTrace,
                source = ex.Source
            });
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        await context.Response.WriteAsJsonAsync(problemDetails, options, cancellationToken);

        return true;
    }
}

