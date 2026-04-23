namespace CheckPromise.Ingestion;

public sealed record IndicatorDatapoint(
    DateTime Date,
    double Value,
    double? Value2 = null,
    string? Quantity = null);
