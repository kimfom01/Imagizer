using ImagizerAPI.Models;

namespace ImagizerAPI.Services;

public static class FileExtensions
{
    private static readonly Dictionary<ImageFormats, string> FormatsExtensions = new()
    {
        [ImageFormats.Gif] = ".gif",
        [ImageFormats.Heic] = ".heic",
        [ImageFormats.Jpeg] = ".jpeg",
        [ImageFormats.Jpg] = ".jpg",
        [ImageFormats.Png] = ".png",
        [ImageFormats.Raw] = ".raw",
        [ImageFormats.Tiff] = ".tiff"
    };

    public static string UpdateExtension(string filename, ImageFormats format)
    {
        var extension = FormatsExtensions[format];

        return Path.ChangeExtension(filename, extension);
    }

    public static string RenameFile(string file)
    {
        var name = Path.GetFileNameWithoutExtension(file);
        var extension = Path.GetExtension(file);

        return $"{name}{Guid.NewGuid()}{extension}";
    }
}