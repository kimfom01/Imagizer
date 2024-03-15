using System.Reflection;
using System.Threading.RateLimiting;
using Imagizer.Api.Exceptions;
using Imagizer.Api.Infrastructure;
using Imagizer.Api.Services;
using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Imagizer API",
        Description = "A Web API for manipulating images based on the Magick.NET library",
        Contact = new OpenApiContact
        {
            Name = "Kim Fom",
            Email = "kimfom01@gmail.com",
            Url = new Uri("https://kimfom.space")
        },
        License = new OpenApiLicense
        {
            Name = "MIT Licence",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.ConfigureSwaggerGen(options => { options.AddEnumsWithValuesFixFilters(); });
builder.Services.AddScoped<IImageProcessorService, ImageProcessorService>();
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("fixed-by-ip", httpContext =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            });
    });
});
builder.Services.AddHttpClient<IUrlShortener, UrlShortener>(
    opt =>
    {
        opt.BaseAddress = new Uri(builder.Configuration.GetValue<string>("SHORTENER:API_HOST") ??
                                  Environment.GetEnvironmentVariable("SHORTENER_API_HOST") ??
                                  throw new NotFoundException("API_HOST not found"));
    });
builder.Services.AddScoped<IUrlShortener, UrlShortener>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRateLimiter();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();