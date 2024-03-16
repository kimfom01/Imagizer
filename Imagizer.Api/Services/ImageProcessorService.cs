using ImageMagick;
using Imagizer.Api.Exceptions;
using Imagizer.Api.Models;

namespace Imagizer.Api.Services;

public class ImageProcessorService : IImageProcessorService
{
    public ImageResponse ResizeImage(ResizeRequest resizeRequest)
    {
        if (resizeRequest.Size <= 0)
        {
            throw new InvalidSizeException("Size cannot be less than or equal to 0");
        }

        using var stream = resizeRequest.ImageFile.OpenReadStream();

        var image = new MagickImage(stream);

        var shape = new MagickGeometry(resizeRequest.Size);

        image.Resize(shape);

        var imageResponse = new ImageResponse
        {
            ImageBytes = image.ToByteArray(),
            Format = image.Format.ToString()
        };

        return imageResponse;
    }

    public async Task<ImageResponse> ConvertImage(ConvertRequest convertRequest)
    {
        await using var stream = convertRequest.ImageFile.OpenReadStream();

        var image = new MagickImage(stream);

        await using var memoryStream = new MemoryStream();

        await image.WriteAsync(memoryStream, (MagickFormat)convertRequest.Format);

        if (memoryStream.CanSeek)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }

        var convertedImage = new MagickImage(memoryStream);

        var imageResponse = new ImageResponse
        {
            ImageBytes = convertedImage.ToByteArray(),
            Format = convertRequest.Format.ToString()
        };

        return imageResponse;
    }
}