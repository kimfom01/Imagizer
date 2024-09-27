using Microsoft.Extensions.Options;

namespace Imagizer.Api.Infrastructure.FileUpload;

public class ConfigureMinioOptions : IConfigureOptions<MinioOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureMinioOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(MinioOptions options)
    {
        _configuration.GetSection(nameof(MinioOptions)).Bind(options);
    }
}