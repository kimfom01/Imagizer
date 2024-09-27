using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Imagizer.Api.Infrastructure.LinkShortener;

public class UrlShortener : IUrlShortener
{
    private readonly HttpClient _httpClient;

    public UrlShortener(IOptions<ShortenerOptions> options, HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(options.Value.ApiHost);
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", options.Value.ApiKey);
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