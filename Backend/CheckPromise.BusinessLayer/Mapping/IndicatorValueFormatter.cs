using System.Globalization;
using CheckPromise.Data.Models;
using Domain = CheckPromise.Data.Models;

namespace CheckPromise.BusinessLayer.Mapping;

internal static class IndicatorValueFormatter
{
    private const string DateFormat = "dd.MM.yyyy";

    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    public static string FormatDate(DateTime date) => date.ToString(DateFormat, Culture);

    public static string FormatNumber(double value, Domain.Measure measure) =>
        measure == Domain.Measure.Percent
            ? value.ToString("0.##", Culture)
            : value.ToString("0.00", Culture);
}
