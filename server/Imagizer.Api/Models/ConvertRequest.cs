namespace Imagizer.Api.Models;

public class ConvertRequest
{
    public IFormFile ImageFile { get; set; }
    public ImageFormats Format { get; set; }
}