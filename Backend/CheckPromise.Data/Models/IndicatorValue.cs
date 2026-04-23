namespace CheckPromise.Data.Models;

public class IndicatorValue
{
    public int Id { get; set; }

    public int IndicatorId { get; set; }

    public Indicator? Indicator { get; set; }

    public DateTime Date { get; set; }

    public double Value { get; set; }

    public double? Value2 { get; set; }

    public string? Quantity { get; set; }
}
