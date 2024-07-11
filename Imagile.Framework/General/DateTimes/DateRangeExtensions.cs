namespace Imagile.Domain.General.DateTimes;

public static class DateRangeExtensions
{
    public static bool OverlapsWith(this IDateRange range, IDateRange comparedRange)
    {
        var range1 = new DateRange()
        {
            Start = range.Start ?? DateTimeOffset.MinValue,
            End = range.End ?? DateTimeOffset.MaxValue
        };
        var range2 = new DateRange()
        {
            Start = comparedRange.Start ?? DateTimeOffset.MinValue,
            End = comparedRange.End ?? DateTimeOffset.MaxValue
        };
        return
            range1.Start <= range2.Start && range2.Start <= range2.End && range2.End <= range1.End ||
            range2.Start <= range1.Start && range1.Start <= range2.End && range2.End <= range1.End ||
            range1.Start <= range2.Start && range2.Start <= range1.End && range1.End <= range2.End ||
            range2.Start <= range1.Start && range1.Start <= range1.End && range1.End <= range2.End;

    }
}