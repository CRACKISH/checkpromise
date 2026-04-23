using System.Text.Json.Serialization;

namespace CheckPromise.DTO;

public enum PromiseStatus
{
    Nothing = 0,
    Done = 1,
    NotPerformed = 2
}

public class Promise
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public PromiseStatus Status { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }
}
