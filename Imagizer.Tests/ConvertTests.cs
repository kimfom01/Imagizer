using System.Net;
using System.Net.Http.Json;

namespace Imagizer.Tests;

public class ConvertTests : IDisposable
{
    private readonly ImagizerApiFactory _api;
    private readonly HttpClient _httpClient;
    private readonly Stream _stream;
    private readonly StreamContent _fileContent;

    public ConvertTests()
    {
        _api = new ImagizerApiFactory();
        _httpClient = _api.CreateClient();
        _stream = File.OpenRead("./Assets/wallpaper.jpg");
        _fileContent = new StreamContent(_stream);
    }

    public void Dispose()
    {
        _api.Dispose();
        _httpClient.Dispose();
        _stream.Dispose();
        _fileContent.Dispose();
    }

    [Fact]
    public async Task ConvertRequestShouldReturn400WhenFormFileMissing()
    {
        var result = await _httpClient.PostAsJsonAsync("/api/Image/convert", new { Format = 182 });

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }


    [Fact]
    public async Task ConvertRequestShouldReturn400WhenFormatMissing()
    {
        using var convertRequest = new MultipartFormDataContent();

        convertRequest.Add(_fileContent, "ImageFile", "wallpaper.jpg");

        var result = await _httpClient.PostAsync("/api/Image/convert", convertRequest);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task ConvertRequestShouldReturn200WhenImageAndFormatCorrect()
    {
        using var format = new StringContent(Convert.ToString(182));
        using var convertRequest = new MultipartFormDataContent();

        convertRequest.Add(_fileContent, "ImageFile", "wallpaper.jpg");
        convertRequest.Add(format, "Format");

        var result = await _httpClient.PostAsync("/api/Image/convert", convertRequest);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}