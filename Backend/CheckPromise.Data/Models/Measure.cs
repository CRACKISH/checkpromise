namespace CheckPromise.Data.Models;

public enum Measure
{
    Empty = 0,
    UAH = 1,
    USD = 2,
    Percent = 3
}

public static class MeasureExtensions
{
    public static string ToSymbol(this Measure measure) => measure switch
    {
        Measure.UAH => "₴",
        Measure.USD => "$",
        Measure.Percent => "%",
        _ => string.Empty
    };
}
