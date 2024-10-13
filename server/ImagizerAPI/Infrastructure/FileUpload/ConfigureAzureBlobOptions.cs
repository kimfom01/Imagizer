using Microsoft.Extensions.Options;

namespace ImagizerAPI.Infrastructure.FileUpload;

public class ConfigureAzureBlobOptions : IConfigureOptions<AzureBlobOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureAzureBlobOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(AzureBlobOptions options)
    {
        _configuration.GetSection(nameof(AzureBlobOptions)).Bind(options);
    }
}