namespace CheckPromise.Ingestion;

public interface IIndicatorDataSource
{
    int IndicatorId { get; }

    IngestionCadence Cadence { get; }

    Task<IndicatorDatapoint?> FetchLatestAsync(CancellationToken cancellationToken = default);
}
