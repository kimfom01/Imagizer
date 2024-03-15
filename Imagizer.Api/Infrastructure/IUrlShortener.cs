namespace Imagizer.Api.Infrastructure;

public interface IUrlShortener
{
    public Task<string?> ShortenUrl(ShortenerRequestModel shortenerRequest);
}