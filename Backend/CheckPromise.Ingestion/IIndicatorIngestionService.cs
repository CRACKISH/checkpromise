namespace CheckPromise.Ingestion;

public interface IIndicatorIngestionService
{
    Task IngestAllAsync(CancellationToken cancellationToken = default);
}
