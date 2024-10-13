namespace ImagizerAPI.Infrastructure.LinkShortener;

public interface IUrlShortener
{
    public Task<string> ShortenUrl(ShortenerRequest shortenerRequest);
}