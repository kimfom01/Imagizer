using Imagizer.Api.Models;

namespace Imagizer.Api.Services;

public interface IImageProcessorService
{
    Task<UrlResponse> ResizeImage(ResizeRequest resizeRequest);

    Task<UrlResponse> ConvertImage(ConvertRequest convertRequest);
}