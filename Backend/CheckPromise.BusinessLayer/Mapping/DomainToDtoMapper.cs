using CheckPromise.Data.Models;
using Dto = CheckPromise.DTO;
using Domain = CheckPromise.Data.Models;

namespace CheckPromise.BusinessLayer.Mapping;

internal static class DomainToDtoMapper
{
    public static Dto.Indicator ToDto(this Domain.Indicator source, DateTime? initialDate, DateTime? currentDate)
    {
        var initial = initialDate.HasValue
            ? source.Values.FirstOrDefault(v => v.Date.Date == initialDate.Value.Date)
            : source.Values.OrderBy(v => v.Date).FirstOrDefault();

        var current = currentDate.HasValue
            ? source.Values.FirstOrDefault(v => v.Date.Date == currentDate.Value.Date)
            : source.Values.OrderByDescending(v => v.Date).FirstOrDefault();

        return new Dto.Indicator
        {
            Id = source.Id,
            Label = source.Label,
            InvertArrow = source.InvertArrow,
            Measure = source.Measure.ToSymbol(),
            Source = source.Source,
            InitialData = initial?.ToDto(source.Measure),
            CurrentData = current?.ToDto(source.Measure),
            GraphData = source.GraphData
                .OrderBy(g => g.Date)
                .Select(ToDto)
                .ToList(),
            MediaInfoData = source.MediaInfoData
                .OrderByDescending(m => m.Date)
                .Select(ToDto)
                .ToList()
        };
    }

    public static Dto.IndicatorValue ToDto(this Domain.IndicatorValue source, Domain.Measure measure) => new()
    {
        Date = IndicatorValueFormatter.FormatDate(source.Date),
        Value = IndicatorValueFormatter.FormatNumber(source.Value, measure),
        Value2 = source.Value2,
        Quantity = source.Quantity
    };

    public static Dto.GraphData ToDto(this Domain.GraphData source) => new()
    {
        Date = IndicatorValueFormatter.FormatDate(source.Date),
        Value = source.Value
    };

    public static Dto.MediaInfo ToDto(this Domain.MediaInfo source) => new()
    {
        Date = IndicatorValueFormatter.FormatDate(source.Date),
        Caption = source.Caption,
        Source = source.Source
    };

    public static Dto.Promise ToDto(this Domain.Promise source) => new()
    {
        Description = source.Description,
        Status = (Dto.PromiseStatus)(int)source.Status,
        Source = source.Source
    };

    public static Dto.News ToDto(this Domain.News source) => new()
    {
        Date = IndicatorValueFormatter.FormatDate(source.Date),
        Value = source.Value
    };
}
