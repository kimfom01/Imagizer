using System.Net;

namespace Imagizer.Tests;

public class GreetingsTests : IDisposable
{
    private readonly ImagizerApiFactory _api;
    private readonly HttpClient _httpClient;

    public GreetingsTests()
    {
        _api = new ImagizerApiFactory();
        _httpClient = _api.CreateClient();
    }

    public void Dispose()
    {
        _api.Dispose();
        _httpClient.Dispose();
    }

    [Fact]
    public async Task ShouldReturn200WhenCalled()
    {
        var result = await _httpClient.GetAsync("/api/Image");

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}