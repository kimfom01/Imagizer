using System.Net;

namespace Imagizer.Tests;

public class ResizeTests : IDisposable
{
    private readonly ImagizerApiFactory _api;
    private readonly HttpClient _httpClient;
    private readonly Stream _stream;
    private readonly StreamContent _fileContent;

    public ResizeTests()
    {
        _api = new ImagizerApiFactory();
        _httpClient = _api.CreateClient();
        _stream = File.OpenRead("./Assets/wallpaper.jpg");
        _fileContent = new StreamContent(_stream);
    }

    [Fact]
    public async Task ResizeRequestShouldReturnBadRequestWhenSizeMissing()
    {
        using var resizeRequest = new MultipartFormDataContent();

        resizeRequest.Add(_fileContent, "ImageFile", "wallpaper.jpg");

        var result = await _httpClient.PostAsync("/api/Image/resize", resizeRequest);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task ResizeRequestShouldReturnBadRequestWhenFormFileMissing()
    {
        using var resizeRequest = new MultipartFormDataContent();
        using var sizeContent = new StringContent(Convert.ToString(400));
        resizeRequest.Add(sizeContent, "Size");

        var result = await _httpClient.PostAsync("/api/Image/resize", resizeRequest);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task ResizeRequestShouldReturn200WhenImageAndSizeCorrect()
    {
        using var sizeContent = new StringContent(Convert.ToString(400));
        using var convertRequest = new MultipartFormDataContent();

        convertRequest.Add(_fileContent, "ImageFile", "wallpaper.jpg");
        convertRequest.Add(sizeContent, "Size");

        var result = await _httpClient.PostAsync("/api/Image/resize", convertRequest);

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
            _stream.Dispose();
            _fileContent.Dispose();
        }
    }
}