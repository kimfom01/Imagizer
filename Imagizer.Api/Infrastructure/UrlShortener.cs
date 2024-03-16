using System.Net.Http.Headers;
using System.Text.Json;
using Imagizer.Api.Utils;

namespace Imagizer.Api.Infrastructure;

public class UrlShortener : IUrlShortener
{
    private readonly HttpClient _httpClient;

    public UrlShortener(IConfiguration config, HttpClient httpClient)
    {
        _httpClient = httpClient;

        var shortenerApiKey = ConfigHelper.GetVariable("SHORTENER:API_KEY", config);

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