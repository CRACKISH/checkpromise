using System.Text.Json.Serialization;

namespace CheckPromise.DTO;

public class ClientData
{
    [JsonPropertyName("indicatorData")]
    public IReadOnlyList<Indicator> IndicatorData { get; set; } = [];

    [JsonPropertyName("promiseData")]
    public IReadOnlyList<Promise> PromiseData { get; set; } = [];

    [JsonPropertyName("news")]
    public IReadOnlyList<News> News { get; set; } = [];
}
