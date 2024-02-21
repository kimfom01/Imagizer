using ImageMagick;
using Imagizer.Api.Models;

namespace Imagizer.Api.Services;

public class ImageProcessorService : IImageProcessorService
{
    public ImageResponse ResizeImage(ResizeRequest resizeRequest)
    {
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

        await using var writeStream = new MemoryStream(200);

        await image.WriteAsync(writeStream, (MagickFormat)convertRequest.Formats);

        var convertedImage = new MagickImage(writeStream);

        var imageResponse = new ImageResponse
        {
            ImageBytes = convertedImage.ToByteArray(),
            Format = convertRequest.Formats.ToString()
        };

        return imageResponse;
    }
}