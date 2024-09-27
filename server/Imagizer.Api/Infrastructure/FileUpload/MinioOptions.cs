namespace Imagizer.Api.Infrastructure.FileUpload;

public class MinioOptions
{
    public string AccessKey { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
}