using System.Text.Json.Serialization;

namespace Imagizer.Api.Infrastructure.LinkShortener;

public class ShortenerResponseModel
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("key")]
    public string? Key { get; set; }
    
    [JsonPropertyName("shrtlnk")]
    public string? ShortLink { get; set; }
}