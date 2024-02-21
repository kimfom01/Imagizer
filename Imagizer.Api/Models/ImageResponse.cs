namespace Imagizer.Api.Models;

public class ImageResponse
{
    public byte[] ImageBytes { get; set; }
    public string Format { get; set; }
}