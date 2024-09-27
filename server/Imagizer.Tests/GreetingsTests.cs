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

    [Fact]
    public async Task ShouldReturn200WhenCalled()
    {
        var result = await _httpClient.GetAsync("/api/Image");

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    public void Dispose()
    {
        Dispose(true);
        
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (isDisposing)
        {
            _api.Dispose();
            _httpClient.Dispose();
        }
    }
}