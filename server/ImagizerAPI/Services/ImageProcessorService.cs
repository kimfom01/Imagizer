using ImageMagick;
using ImagizerAPI.Exceptions;
using ImagizerAPI.Infrastructure.FileUpload;
using ImagizerAPI.Infrastructure.LinkShortener;
using ImagizerAPI.Models;

namespace ImagizerAPI.Services;

public class ImageProcessorService : IImageProcessorService
{
    private readonly IObjectUploader _objectUploader;
    private readonly IUrlShortener _urlShortener;

    public ImageProcessorService(IObjectUploader objectUploader, IUrlShortener urlShortener)
    {
        _objectUploader = objectUploader;
        _urlShortener = urlShortener;
    }

    public async Task<UrlResponse> ResizeImage(ResizeRequest resizeRequest)
    {
        if (resizeRequest.Size <= 0)
        {
            throw new InvalidSizeException("Size cannot be less than or equal to 0");
        }

        var image = await Resize(resizeRequest);

        using var memoryStream = await WriteToMemoryStream(image);

        var contents = new UploadContents(resizeRequest.ImageFile.FileName, memoryStream);

        var downloadUrl = await _objectUploader.UploadImage(contents);

        return await ShortenUrl(downloadUrl);
    }

    private async Task<MemoryStream> WriteToMemoryStream(MagickImage image)
    {
        var memoryStream = new MemoryStream();

        await image.WriteAsync(memoryStream);

        if (memoryStream.CanSeek)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }

        return memoryStream;
    }

    private async Task<MagickImage> Resize(ResizeRequest resizeRequest)
    {
        await using var stream = resizeRequest.ImageFile.OpenReadStream();

        var image = new MagickImage(stream);

        var shape = new MagickGeometry(resizeRequest.Size);

        image.Resize(shape);

        return image;
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

        var contents = new UploadContents(convertRequest.ImageFile.FileName, memoryStream);

        var downloadUrl = await _objectUploader.UploadImage(contents);

        return await ShortenUrl(downloadUrl);
    }

    private async Task<UrlResponse> ShortenUrl(string downloadUrl)
    {
        try
        {
            var shortUrl = await _urlShortener.ShortenUrl(new ShortenerRequest (downloadUrl));

            return new UrlResponse(shortUrl);
        }
        catch (UrlShortenerException)
        {
            return new UrlResponse(downloadUrl);
        }
    }
}