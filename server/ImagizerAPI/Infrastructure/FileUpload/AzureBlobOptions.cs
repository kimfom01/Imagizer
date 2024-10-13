namespace ImagizerAPI.Infrastructure.FileUpload;

public class AzureBlobOptions
{
    public required string AccountName { get; init; }
    public required string AccountKey { get; init; }
    public required string Url { get; init; }
}