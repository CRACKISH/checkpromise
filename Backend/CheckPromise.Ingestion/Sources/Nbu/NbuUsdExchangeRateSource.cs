using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace CheckPromise.Ingestion.Sources.Nbu;

/// <summary>
/// Fetches the current USD/UAH exchange rate from the NBU public JSON endpoint.
/// Contract: https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=USD&amp;json
/// Returns today's rate (or last business day if markets are closed).
/// </summary>
public class NbuUsdExchangeRateSource(
    HttpClient httpClient,
    ILogger<NbuUsdExchangeRateSource> logger) : IIndicatorDataSource
{
    private const string Endpoint = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=USD&json";

    private readonly HttpClient _httpClient = httpClient;

    private readonly ILogger<NbuUsdExchangeRateSource> _logger = logger;

    public int IndicatorId => 1;

    public IngestionCadence Cadence => IngestionCadence.Daily;

    public async Task<IndicatorDatapoint?> FetchLatestAsync(CancellationToken cancellationToken = default)
    {
        var entries = await _httpClient.GetFromJsonAsync<NbuRateEntry[]>(Endpoint, cancellationToken);
        if (entries is null || entries.Length == 0)
        {
            _logger.LogWarning("NBU returned no USD exchange rate entries");
            return null;
        }

        var entry = entries[0];
        if (!DateTime.TryParseExact(
                entry.ExchangeDate,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))
        {
            _logger.LogWarning("NBU returned unparseable exchange date: {RawDate}", entry.ExchangeDate);
            return null;
        }

        return new IndicatorDatapoint(date, entry.Rate);
    }

    private sealed record NbuRateEntry(
        [property: JsonPropertyName("exchangedate")] string ExchangeDate,
        [property: JsonPropertyName("rate")] double Rate);
}
