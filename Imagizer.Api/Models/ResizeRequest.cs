namespace Imagizer.Api.Models;

public class ResizeRequest
{
    public IFormFile ImageFile { get; set; }
    public int Size { get; set; }
}