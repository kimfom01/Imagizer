using System.Reflection;
using System.Threading.RateLimiting;
using Imagizer.Api.Options;
using Imagizer.Api.Utils;
using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    var corsOriginOptions = builder
        .Configuration.GetSection(nameof(CorsOriginOptions))
        .GetChildren()
        .ToArray();

    options.AddPolicy("default", pol => pol.WithOrigins(corsOriginOptions[0].Value!));
});
builder.Services.AddSwaggerGen(options =>
{
    var openApiOptions = builder
        .Configuration.GetSection(nameof(OpenApiOptions))
        .GetChildren()
        .ToArray();

    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Imagizer API",
            Description = "A Web API for manipulating images based on the Magick.NET library",
            Contact = new OpenApiContact
            {
                Name = "Kim Fom",
                Email = "kimfom01@gmail.com",
                Url = new Uri(openApiOptions[0].Value!)
            },
            License = new OpenApiLicense
            {
                Name = "MIT Licence",
                Url = new Uri(openApiOptions[1].Value!)
            }
        }
    );

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy(
        "fixed-by-ip",
        httpContext =>
        {
            return RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromMinutes(1)
                }
            );
        }
    );
});
builder.Services.ConfigureSwaggerGen(options =>
{
    options.AddEnumsWithValuesFixFilters();
});
builder.Services.RegisterServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRateLimiter();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("default");

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthz");

app.Run();
