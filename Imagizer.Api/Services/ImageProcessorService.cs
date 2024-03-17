using ImageMagick;
using Imagizer.Api.Exceptions;
using Imagizer.Api.Infrastructure.FileUpload;
using Imagizer.Api.Models;

namespace Imagizer.Api.Services;

public class ImageProcessorService : IImageProcessorService
{
    private readonly IObjectUploader _objectUploader;

    public ImageProcessorService(IObjectUploader objectUploader)
    {
        _objectUploader = objectUploader;
    }

    public async Task<UrlResponse> ResizeImage(ResizeRequest resizeRequest)
    {
        if (resizeRequest.Size <= 0)
        {
            throw new InvalidSizeException("Size cannot be less than or equal to 0");
        }

        await using var stream = resizeRequest.ImageFile.OpenReadStream();

        var image = new MagickImage(stream);

        var shape = new MagickGeometry(resizeRequest.Size);

        image.Resize(shape);

        await using var memoryStream = new MemoryStream();

        await image.WriteAsync(memoryStream);
        
        if (memoryStream.CanSeek)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }

        var imageUploadArgs = new ImageUploadArgs
        {
            BucketName = "resize-bucket",
            ContentType = resizeRequest.ImageFile.ContentType,
            ImageStream = memoryStream,
            ObjectName = resizeRequest.ImageFile.FileName
        };

        var downloadUrl = await _objectUploader.UploadImage(imageUploadArgs);

        return new UrlResponse
        {
            DownloadUrl = downloadUrl
        };
    }

    public async Task<UrlResponse> ConvertImage(ConvertRequest convertRequest)
    {
        await using var stream = convertRequest.ImageFile.OpenReadStream();

        var image = new MagickImage(stream);

        await using var memoryStream = new MemoryStream();

        await image.WriteAsync(memoryStream, (MagickFormat)convertRequest.Format);

        if (memoryStream.CanSeek)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }

        var imageUploadArgs = new ImageUploadArgs
        {
            BucketName = "convert-bucket",
            ContentType = convertRequest.ImageFile.ContentType,
            ImageStream = memoryStream,
            ObjectName = convertRequest.ImageFile.FileName
        };

        var downloadUrl = await _objectUploader.UploadImage(imageUploadArgs);

        return new UrlResponse
        {
            DownloadUrl = downloadUrl
        };
    }
}