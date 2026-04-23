using System.Text.Json.Serialization;

namespace CheckPromise.DTO;

public class Indicator
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("invertArrow")]
    public bool InvertArrow { get; set; }

    [JsonPropertyName("measure")]
    public string Measure { get; set; } = string.Empty;

    [JsonPropertyName("initialData")]
    public IndicatorValue? InitialData { get; set; }

    [JsonPropertyName("currentData")]
    public IndicatorValue? CurrentData { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("graphData")]
    public IReadOnlyList<GraphData> GraphData { get; set; } = [];

    [JsonPropertyName("mediaInfoData")]
    public IReadOnlyList<MediaInfo> MediaInfoData { get; set; } = [];
}
