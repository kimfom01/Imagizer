using System.Net.Http.Headers;
using System.Text.Json;
using Imagizer.Api.Exceptions;

namespace Imagizer.Api.Infrastructure;

public class UrlShortener : IUrlShortener
{
    private readonly HttpClient _httpClient;

    public UrlShortener(
        IConfiguration config,
        HttpClient httpClient)
    {
        _httpClient = httpClient;

        var shortenerApiKey = config.GetValue<string>("SHORTENER:API_KEY") ??
                              Environment.GetEnvironmentVariable("SHORTENER_API_KEY") ??
                              throw new NotFoundException("API_KEY not found");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", shortenerApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string?> ShortenUrl(ShortenerRequestModel shortenerRequest)
    {
        var response = await _httpClient.PostAsJsonAsync(string.Empty, shortenerRequest);

        response.EnsureSuccessStatusCode();

        var responseStream = await response.Content.ReadAsStreamAsync();

        var responseModel =
            await JsonSerializer.DeserializeAsync<ShortenerResponseModel>(responseStream);

        return responseModel?.ShortLink;
    }
}