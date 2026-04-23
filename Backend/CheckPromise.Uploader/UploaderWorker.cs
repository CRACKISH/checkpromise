using CheckPromise.BusinessLayer;
using Checkpromise.Provider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CheckPromise.Uploader;

public class UploaderWorker(
    IServiceScopeFactory scopeFactory,
    IOptions<UploaderOptions> options,
    ILogger<UploaderWorker> logger,
    TimeProvider timeProvider) : BackgroundService
{
    private static readonly TimeSpan DailyInterval = TimeSpan.FromDays(1);

    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    private readonly UploaderOptions _options = options.Value;

    private readonly ILogger<UploaderWorker> _logger = logger;

    private readonly TimeProvider _timeProvider = timeProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Uploader started. RunAt={RunAt} UTC, RunOnStartup={RunOnStartup}",
            _options.RunAt,
            _options.RunOnStartup);

        if (_options.RunOnStartup)
        {
            await RunOnceSafelyAsync(stoppingToken);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = GetDelayUntilNextRun();
            _logger.LogInformation("Next upload scheduled in {Delay}", delay);
            try
            {
                await Task.Delay(delay, _timeProvider, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            await RunOnceSafelyAsync(stoppingToken);
        }
    }

    private async Task RunOnceSafelyAsync(CancellationToken cancellationToken)
    {
        try
        {
            await RunOnceAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Uploader iteration failed");
        }
    }

    private async Task RunOnceAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var builder = scope.ServiceProvider.GetRequiredService<IClientDataBuilder>();
        var provider = scope.ServiceProvider.GetRequiredService<IClientDataProvider>();

        _logger.LogInformation("Building client data snapshot");
        var clientData = await builder.BuildAsync(cancellationToken);

        _logger.LogInformation(
            "Pushing snapshot ({Indicators} indicators, {Promises} promises, {News} news items)",
            clientData.IndicatorData.Count,
            clientData.PromiseData.Count,
            clientData.News.Count);

        await provider.PushAsync(clientData, cancellationToken);
        _logger.LogInformation("Snapshot pushed successfully");
    }

    private TimeSpan GetDelayUntilNextRun()
    {
        var now = _timeProvider.GetUtcNow();
        var target = new DateTimeOffset(
            now.Year, now.Month, now.Day,
            _options.RunAt.Hour, _options.RunAt.Minute, _options.RunAt.Second,
            TimeSpan.Zero);

        if (target <= now)
        {
            target = target.Add(DailyInterval);
        }

        return target - now;
    }
}
