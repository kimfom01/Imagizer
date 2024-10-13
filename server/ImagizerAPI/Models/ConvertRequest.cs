namespace ImagizerAPI.Models;

public record ConvertRequest(IFormFile ImageFile, ImageFormats Format);