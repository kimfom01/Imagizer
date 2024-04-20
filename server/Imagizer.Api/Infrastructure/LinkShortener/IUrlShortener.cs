namespace Imagizer.Api.Infrastructure.LinkShortener;

public interface IUrlShortener
{
    public Task<string?> ShortenUrl(ShortenerRequestModel shortenerRequest);
}