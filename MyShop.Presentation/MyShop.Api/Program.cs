using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using MyShop.Api.Middlewares;
using MyShop.Application;
using MyShop.Application.Middlewares;
using MyShop.Databases.Postgres.Repositories;
using MyShop.Databases.Postgres.Repositories.Implementations;
using Npgsql;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Setup configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Add configurations
AddConfigurations(configuration, builder);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

// Add services to the container.
AddCustomServices(builder);

builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddValidatorsFromAssembly(Assembly.Load(typeof(ApplicationAssemblyReference).Assembly.ToString()));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

//TODO : change CORS configuration for development environment only
app.UseCors(options =>
{
    options.WithOrigins("http://localhost/")
           .AllowAnyMethod()
           .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseMiddleware<HttpResponseHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.Run();

void AddConfigurations(IConfiguration configuration, WebApplicationBuilder builder)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));
}

void AddCustomServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IPriceRepository, PriceRepository>();
    builder.Services.AddScoped<IStockRepository, StockRepository>();
}