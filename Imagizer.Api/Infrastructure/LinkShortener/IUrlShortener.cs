namespace Imagizer.Api.Infrastructure.LinkShortener;

public interface IUrlShortener : IDisposable
{
    public Task<string?> ShortenUrl(ShortenerRequestModel shortenerRequest);
}