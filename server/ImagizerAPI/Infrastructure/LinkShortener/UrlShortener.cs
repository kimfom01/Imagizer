using System.Net.Http.Headers;
using System.Text.Json;
using ImagizerAPI.Exceptions;
using Microsoft.Extensions.Options;

namespace ImagizerAPI.Infrastructure.LinkShortener;

public class UrlShortener : IUrlShortener
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UrlShortener> _logger;

    public UrlShortener(IOptions<ShortenerOptions> options, HttpClient httpClient, ILogger<UrlShortener> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        _httpClient.BaseAddress = new Uri(options.Value.ApiHost);
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", options.Value.ApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> ShortenUrl(ShortenerRequest shortenerRequest)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(string.Empty, shortenerRequest);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            var responseModel = await JsonSerializer.DeserializeAsync<ShortenerResponse>(responseStream);

            return responseModel?.ShortLink ?? string.Empty;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "An error occured: {Exception}", exception);
            throw new UrlShortenerException("Unable to shorten url");
        }
    }
}