using System.Text.Json.Serialization;

namespace ImagizerAPI.Infrastructure.LinkShortener;

public class ShortenerResponse
{
    [JsonPropertyName("url")]
    public string? Url { get; init; }
    
    [JsonPropertyName("key")]
    public string? Key { get; init; }
    
    [JsonPropertyName("shrtlnk")]
    public string ShortLink { get; init; } = string.Empty;
}