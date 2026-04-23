using System.Text.Json.Serialization;

namespace CheckPromise.DTO;

public class News
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
