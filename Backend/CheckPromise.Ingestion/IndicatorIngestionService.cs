using CheckPromise.Data.DataContext;
using CheckPromise.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CheckPromise.Ingestion;

public class IndicatorIngestionService(
    IEnumerable<IIndicatorDataSource> sources,
    CheckPromiseContext context,
    ILogger<IndicatorIngestionService> logger,
    TimeProvider timeProvider) : IIndicatorIngestionService
{
    private readonly IReadOnlyList<IIndicatorDataSource> _sources = sources.ToList();

    private readonly CheckPromiseContext _context = context;

    private readonly ILogger<IndicatorIngestionService> _logger = logger;

    private readonly TimeProvider _timeProvider = timeProvider;

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
        var latest = await _context.IndicatorValues
            .Where(v => v.IndicatorId == source.IndicatorId)
            .OrderByDescending(v => v.Date)
            .FirstOrDefaultAsync(cancellationToken);

        var now = _timeProvider.GetUtcNow().UtcDateTime;
        if (!IsDueForFetch(source.Cadence, latest, now))
        {
            _logger.LogDebug(
                "Skipping indicator {IndicatorId} ({Cadence}): latest datapoint on {Date:yyyy-MM-dd} is still fresh",
                source.IndicatorId,
                source.Cadence,
                latest!.Date);
            return;
        }

        var datapoint = await source.FetchLatestAsync(cancellationToken);
        if (datapoint is null)
        {
            _logger.LogWarning(
                "Source {SourceType} returned no datapoint for indicator {IndicatorId}",
                source.GetType().Name,
                source.IndicatorId);
            return;
        }

        if (latest is not null && IsSameValue(latest, datapoint))
        {
            _logger.LogInformation(
                "Indicator {IndicatorId}: value unchanged since {Date:yyyy-MM-dd} ({Value}) — skipping insert",
                source.IndicatorId,
                latest.Date,
                latest.Value);
            return;
        }

        _context.IndicatorValues.Add(new IndicatorValue
        {
            IndicatorId = source.IndicatorId,
            Date = datapoint.Date.Date,
            Value = datapoint.Value,
            Value2 = datapoint.Value2,
            Quantity = datapoint.Quantity
        });

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Indicator {IndicatorId}: inserted new datapoint {Value} on {Date:yyyy-MM-dd}",
            source.IndicatorId,
            datapoint.Value,
            datapoint.Date);
    }

    private static bool IsDueForFetch(IngestionCadence cadence, IndicatorValue? latest, DateTime now)
    {
        if (latest is null)
        {
            return true;
        }

        return cadence switch
        {
            IngestionCadence.Daily => latest.Date.Date < now.Date,
            IngestionCadence.Monthly => !SameMonth(latest.Date, now),
            IngestionCadence.Quarterly => !SameQuarter(latest.Date, now),
            IngestionCadence.Yearly => latest.Date.Year != now.Year,
            _ => true
        };
    }

    private static bool IsSameValue(IndicatorValue latest, IndicatorDatapoint incoming) =>
        Math.Abs(latest.Value - incoming.Value) < 1e-9
        && Nullable.Equals(latest.Value2, incoming.Value2)
        && string.Equals(latest.Quantity, incoming.Quantity, StringComparison.Ordinal);

    private static bool SameMonth(DateTime a, DateTime b) =>
        a.Year == b.Year && a.Month == b.Month;

    private static bool SameQuarter(DateTime a, DateTime b) =>
        a.Year == b.Year && Quarter(a) == Quarter(b);

    private static int Quarter(DateTime date) => (date.Month - 1) / 3 + 1;
}
