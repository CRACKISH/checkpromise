using CheckPromise.BusinessLayer.Mapping;
using CheckPromise.Data.DataContext;
using CheckPromise.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CheckPromise.BusinessLayer;

public class ClientDataBuilderOptions
{
    public const string SectionName = "ClientDataBuilder";

    public DateTime? InitialValueDate { get; set; }

    public DateTime? CurrentValueDate { get; set; }
}

public class ClientDataBuilder(
    CheckPromiseContext context,
    IOptions<ClientDataBuilderOptions> options) : IClientDataBuilder
{
    private readonly CheckPromiseContext _context = context;

    private readonly ClientDataBuilderOptions _options = options.Value;

    public async Task<ClientData> BuildAsync(CancellationToken cancellationToken = default)
    {
        var indicators = await _context.Indicators
            .AsNoTracking()
            .Include(i => i.Values)
            .Include(i => i.GraphData)
            .Include(i => i.MediaInfoData)
            .ToListAsync(cancellationToken);

        var promises = await _context.Promises
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);

        var news = await _context.News
            .AsNoTracking()
            .OrderByDescending(n => n.Date)
            .ToListAsync(cancellationToken);

        var currentDate = _options.CurrentValueDate ?? DateTime.UtcNow;

        return new ClientData
        {
            IndicatorData = indicators
                .OrderBy(i => i.Id)
                .Select(i => i.ToDto(_options.InitialValueDate, currentDate))
                .ToList(),
            PromiseData = promises
                .Select(p => p.ToDto())
                .ToList(),
            News = news
                .Select(n => n.ToDto())
                .ToList()
        };
    }
}
