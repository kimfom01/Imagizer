using Minio;
using Minio.DataModel.Args;

namespace Imagizer.Api.Infrastructure.FileUpload;

public class ObjectUploader : IObjectUploader
{
    private readonly IMinioClient _minioClient;
    private const int ExpireBy = 60 * 60 * 2;

    public ObjectUploader(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> UploadImage(ImageUploadArgs imageUploadArgs)
    {
        try
        {
            await CreateBucket(imageUploadArgs.BucketName);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(imageUploadArgs.BucketName)
                .WithObject(imageUploadArgs.ObjectName)
                .WithStreamData(imageUploadArgs.ImageStream)
                .WithObjectSize(imageUploadArgs.ImageStream.Length)
                .WithContentType(imageUploadArgs.ContentType);

            await _minioClient.PutObjectAsync(putObjectArgs);
            
            var downloadUrl = await GetDownloadUrl(imageUploadArgs);

            return downloadUrl;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async Task<string> GetDownloadUrl(ImageUploadArgs imageUploadArgs)
    {
        var presignedGetObjectArgs = new PresignedGetObjectArgs()
            .WithBucket(imageUploadArgs.BucketName)
            .WithObject(imageUploadArgs.ObjectName)
            .WithExpiry(ExpireBy);

        var downloadUrl = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

        return downloadUrl;
    }

    private async Task CreateBucket(string bucketName)
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);

        var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!bucketExists)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);
            await _minioClient.MakeBucketAsync(makeBucketArgs);
        }
    }
}