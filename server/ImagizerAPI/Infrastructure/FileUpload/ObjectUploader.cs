using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using ImagizerAPI.Exceptions;
using Microsoft.Extensions.Options;

namespace ImagizerAPI.Infrastructure.FileUpload;

public class ObjectUploader : IObjectUploader
{
    private readonly ILogger<ObjectUploader> _logger;
    private readonly AzureBlobOptions _azureBlobOptions;
    private const int ExpireBy = 60 * 60 * 2; // 2 hours

    public ObjectUploader(ILogger<ObjectUploader> logger, IOptions<AzureBlobOptions> blobOptions)
    {
        _logger = logger;
        _azureBlobOptions = blobOptions.Value;
    }

    public async Task<string> UploadImage(UploadContents uploadContents)
    {
        try
        {
            var containerClient = new BlobContainerClient(
                new Uri(_azureBlobOptions.Url),
                new StorageSharedKeyCredential(_azureBlobOptions.AccountName, _azureBlobOptions.AccountKey)
            );

            var response = await containerClient.UploadBlobAsync(
                uploadContents.FileName,
                uploadContents.ImageStream
            );
            var blobClient = containerClient.GetBlobClient(uploadContents.FileName);
            
            var downloadUrl = blobClient.GenerateSasUri(
                BlobSasPermissions.Read,
                DateTimeOffset.UtcNow.AddSeconds(ExpireBy)
            );

            _logger.LogInformation("Resource uploaded at: {LastModified}", response.Value.LastModified);

            return downloadUrl.AbsoluteUri;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occured: {Exception}", exception);

            throw new FileUploadException("Unable to upload content");
        }
    }
}