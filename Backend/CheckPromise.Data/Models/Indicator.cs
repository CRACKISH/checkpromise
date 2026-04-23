namespace CheckPromise.Data.Models;

public class Indicator
{
    public int Id { get; set; }

    public required string Label { get; set; }

    public bool InvertArrow { get; set; }

    public string? Source { get; set; }

    public Measure Measure { get; set; }

    public List<IndicatorValue> Values { get; set; } = new();

    public List<GraphData> GraphData { get; set; } = new();

    public List<MediaInfo> MediaInfoData { get; set; } = new();
}
