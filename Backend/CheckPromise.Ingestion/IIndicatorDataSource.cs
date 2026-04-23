namespace CheckPromise.Ingestion;

public interface IIndicatorDataSource
{
    int IndicatorId { get; }

    Task<IndicatorDatapoint?> FetchLatestAsync(CancellationToken cancellationToken = default);
}
