using Ejada.InvoiceOCRExtraction.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ejada.InvoiceOCRExtraction.API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjectionForApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvoiceOCRDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("OCRDbContext")));

      
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        return services;
    }
}