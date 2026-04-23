namespace CheckPromise.Data.Models;

public class MediaInfo
{
    public int Id { get; set; }

    public int IndicatorId { get; set; }

    public Indicator? Indicator { get; set; }

    public DateTime Date { get; set; }

    public required string Caption { get; set; }

    public required string Source { get; set; }
}
