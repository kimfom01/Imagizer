using System.Reflection;
using System.Threading.RateLimiting;
using Imagizer.Api.Infrastructure.FileUpload;
using Imagizer.Api.Infrastructure.LinkShortener;
using Imagizer.Api.Services;
using Microsoft.OpenApi.Models;
using Minio;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace Imagizer.Api.Utils;

public static class ConfigureServicesExtension
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
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
        services.ConfigureSwaggerGen(options => { options.AddEnumsWithValuesFixFilters(); });
        services.AddScoped<IImageProcessorService, ImageProcessorService>();
        services.AddRateLimiter(options =>
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
        services.AddHttpClient<IUrlShortener, UrlShortener>(
            opt => { opt.BaseAddress = new Uri(ConfigHelper.GetVariable("SHORTENER:API_HOST", configuration)); });
        services.AddScoped<IUrlShortener, UrlShortener>();
        services.AddMinio(config =>
        {
            config.WithEndpoint(ConfigHelper.GetVariable("MINIO:URL", configuration))
                .WithCredentials(ConfigHelper.GetVariable("MINIO:ACCESS_KEY", configuration),
                    ConfigHelper.GetVariable("MINIO:SECRET_KEY", configuration));

            if (environment.IsDevelopment())
            {
                config.WithSSL(false);
            }
        });
        services.AddTransient<IObjectUploader, ObjectUploader>();

        return services;
    }
}