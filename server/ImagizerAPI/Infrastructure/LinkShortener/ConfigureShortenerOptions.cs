using Microsoft.Extensions.Options;

namespace ImagizerAPI.Infrastructure.LinkShortener;

public class ConfigureShortenerOptions : IConfigureOptions<ShortenerOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureShortenerOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ShortenerOptions options)
    {
        _configuration.GetSection(nameof(ShortenerOptions)).Bind(options);
    }
}