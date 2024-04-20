namespace Imagizer.Api.Infrastructure.FileUpload;

public class ImageUploadArgs
{
    public string BucketName { get; set; }
    public string ObjectName { get; set; }
    public Stream ImageStream { get; set; }
    public string ContentType { get; set; }
}