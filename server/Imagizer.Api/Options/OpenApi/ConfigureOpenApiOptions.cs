using Microsoft.Extensions.Options;

namespace Imagizer.Api.Options.OpenApi;

public class ConfigureOpenApiOptions : IConfigureOptions<OpenApiOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureOpenApiOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(OpenApiOptions options)
    {
        _configuration.GetSection(nameof(OpenApiOptions)).Bind(options);
    }
}