using CheckPromise.Data.DataContext;
using CheckPromise.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CheckPromise.Ingestion;

public class IndicatorIngestionService(
    IEnumerable<IIndicatorDataSource> sources,
    CheckPromiseContext context,
    ILogger<IndicatorIngestionService> logger) : IIndicatorIngestionService
{
    private readonly IReadOnlyList<IIndicatorDataSource> _sources = sources.ToList();

    private readonly CheckPromiseContext _context = context;

    private readonly ILogger<IndicatorIngestionService> _logger = logger;

    public async Task IngestAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Ingesting indicators from {SourceCount} sources", _sources.Count);

        foreach (var source in _sources)
        {
            try
            {
                await IngestOneAsync(source, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Ingestion failed for indicator {IndicatorId} from {SourceType}",
                    source.IndicatorId,
                    source.GetType().Name);
            }
        }
    }

    private async Task IngestOneAsync(IIndicatorDataSource source, CancellationToken cancellationToken)
    {
        var datapoint = await source.FetchLatestAsync(cancellationToken);
        if (datapoint is null)
        {
            _logger.LogWarning(
                "Source {SourceType} returned no datapoint for indicator {IndicatorId}",
                source.GetType().Name,
                source.IndicatorId);
            return;
        }

        var date = datapoint.Date.Date;
        var existing = await _context.IndicatorValues
            .FirstOrDefaultAsync(
                v => v.IndicatorId == source.IndicatorId && v.Date == date,
                cancellationToken);

        if (existing is null)
        {
            _context.IndicatorValues.Add(new IndicatorValue
            {
                IndicatorId = source.IndicatorId,
                Date = date,
                Value = datapoint.Value,
                Value2 = datapoint.Value2,
                Quantity = datapoint.Quantity
            });

            _logger.LogInformation(
                "Inserted datapoint for indicator {IndicatorId} on {Date:yyyy-MM-dd}: {Value}",
                source.IndicatorId,
                date,
                datapoint.Value);
        }
        else
        {
            existing.Value = datapoint.Value;
            existing.Value2 = datapoint.Value2;
            existing.Quantity = datapoint.Quantity;

            _logger.LogInformation(
                "Updated datapoint for indicator {IndicatorId} on {Date:yyyy-MM-dd}: {Value}",
                source.IndicatorId,
                date,
                datapoint.Value);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
