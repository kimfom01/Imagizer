using System.Reflection;
using System.Threading.RateLimiting;
using Imagizer.Api.Infrastructure.FileUpload;
using Imagizer.Api.Infrastructure.LinkShortener;
using Imagizer.Api.Options.OpenApi;
using Imagizer.Api.Services;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Minio;

namespace Imagizer.Api;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services.AddScoped<IImageProcessorService, ImageProcessorService>();

        services.AddHttpClient<IUrlShortener, UrlShortener>();
        services.AddScoped<IUrlShortener, UrlShortener>();
        services.AddMinio(config =>
        {
            var minioOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<MinioOptions>>();

            config.WithEndpoint(minioOptions.Value.Url)
                .WithCredentials(minioOptions.Value.AccessKey, minioOptions.Value.SecretKey);

            if (environment.IsDevelopment())
            {
                config.WithSSL(false);
            }
        });
        services.AddTransient<IObjectUploader, ObjectUploader>();
        services.AddHealthChecks();

        return services;
    }

    public static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            var openApiOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<OpenApiOptions>>();

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
                        Url = new Uri(openApiOptions.Value.WebsiteUrl)
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT Licence",
                        Url = new Uri(openApiOptions.Value.LicenceUrl)
                    }
                }
            );

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection ConfigureRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
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

        return services;
    }
}