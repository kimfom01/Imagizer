namespace ImagizerAPI.Models;

public record ResizeRequest(IFormFile ImageFile, uint Size);