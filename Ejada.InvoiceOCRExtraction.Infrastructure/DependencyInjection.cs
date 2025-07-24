using Ejada.InvoiceOCRExtraction.Domain.IRepositories;
using Ejada.InvoiceOCRExtraction.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ejada.InvoiceOCRExtraction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjectionForInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        return services;
    }

}
