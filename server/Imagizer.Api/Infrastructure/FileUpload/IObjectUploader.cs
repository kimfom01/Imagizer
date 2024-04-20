namespace Imagizer.Api.Infrastructure.FileUpload;

public interface IObjectUploader
{
    public Task<string> UploadImage(ImageUploadArgs imageUploadArgs);
}