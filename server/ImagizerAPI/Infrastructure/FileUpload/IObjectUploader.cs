namespace ImagizerAPI.Infrastructure.FileUpload;

public interface IObjectUploader
{
    public Task<string> UploadImage(UploadContents uploadContents);
}