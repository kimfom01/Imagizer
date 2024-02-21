using Imagizer.Api.Models;

namespace Imagizer.Api.Services;

public interface IImageProcessorService
{
    ImageResponse ResizeImage(ResizeRequest resizeRequest);

    Task<ImageResponse> ConvertImage(ConvertRequest convertRequest);
}