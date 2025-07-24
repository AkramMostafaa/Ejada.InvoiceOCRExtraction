using Ejada.InvoiceOCRExtraction.API.CustomMiddlewares;
using Ejada.InvoiceOCRExtraction.API;
using Ejada.InvoiceOCRExtraction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ejada.InvoiceOCRExtraction.Application;
using Ejada.InvoiceOCRExtraction.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDependencyInjectionForApi(builder.Configuration);
builder.Services.AddDependencyInjectionForApplication();
builder.Services.AddDependencyInjectionForInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        context.HttpContext.Items["__FluentValidation__"] = context.ModelState;
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return new EmptyResult();
    };
});


//builder.Services.AddSwaggerGen();
var app = builder.Build();
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<InvoiceOCRDbContext>();

var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

try
{
    await context.Database.MigrateAsync();

}
catch (Exception)
{
    throw new Exception("AnErrorOccurredWhileApplyMigration");
}
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ValidationExceptionMiddleware>();


//app.MapOpenApi();
app.UseSwagger();
//}
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
