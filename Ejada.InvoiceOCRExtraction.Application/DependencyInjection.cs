using Ejada.InvoiceOCRExtraction.Application.IServices;
using Ejada.InvoiceOCRExtraction.Application.Services;
using Ejada.InvoiceOCRExtraction.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ejada.InvoiceOCRExtraction.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjectionForApplication(this IServiceCollection services)
    {
        services.AddScoped<IOcrService, OcrService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddValidatorsFromAssemblyContaining<InvoiceDtoValidator>();
        services.AddFluentValidationAutoValidation(); 
        services.AddFluentValidationClientsideAdapters();

        return services;
    }
}