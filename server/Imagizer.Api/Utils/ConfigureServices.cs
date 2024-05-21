using Imagizer.Api.Infrastructure.FileUpload;
using Imagizer.Api.Infrastructure.LinkShortener;
using Imagizer.Api.Services;
using Minio;

namespace Imagizer.Api.Utils;

public static class ConfigureServicesExtension
{
    public static IServiceCollection RegisterServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddScoped<IImageProcessorService, ImageProcessorService>();
        
        services.AddHttpClient<IUrlShortener, UrlShortener>();
        services.AddScoped<IUrlShortener, UrlShortener>();
        services.AddMinio(config =>
        {
            config.WithEndpoint(ConfigHelper.GetVariable("MINIO:URL", configuration))
                .WithCredentials(ConfigHelper.GetVariable("MINIO:ACCESS_KEY", configuration),
                    ConfigHelper.GetVariable("MINIO:SECRET_KEY", configuration));

            if (environment.IsDevelopment()) // restore after adding ssl to minio deployment
            {
                config.WithSSL(false);
            }
        });
        services.AddTransient<IObjectUploader, ObjectUploader>();
        services.AddHealthChecks();

        return services;
    }
}