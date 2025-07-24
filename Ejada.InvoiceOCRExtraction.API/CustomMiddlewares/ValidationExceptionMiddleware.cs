using Ejada.InvoiceOCRExtraction.Application.Shared;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Ejada.InvoiceOCRExtraction.API.CustomMiddlewares;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == 400 &&
            context.Items["__FluentValidation__"] is ModelStateDictionary modelState &&
            !context.Response.HasStarted)
        {
            var allErrors = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = GeneralResponse<object>.FailResponse(
                "Validation failed.",
                allErrors,
                System.Net.HttpStatusCode.BadRequest
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

