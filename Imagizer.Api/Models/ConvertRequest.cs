namespace Imagizer.Api.Models;

public class ConvertRequest
{
    public IFormFile ImageFile { get; set; }
    public ImageFormats Formats { get; set; }
}