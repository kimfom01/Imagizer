using ImagizerAPI.Models;

namespace ImagizerAPI.Services;

public interface IImageProcessorService
{
    Task<UrlResponse> ResizeImage(ResizeRequest resizeRequest);

    Task<UrlResponse> ConvertImage(ConvertRequest convertRequest);
}