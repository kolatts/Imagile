using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.Extensions;
public interface IDateRange
{
    DateTimeOffset? Start { get; }
    DateTimeOffset? End { get; set; }
}

public class DateRange : IDateRange
{
    public DateTimeOffset? Start { get; set; }

    public DateTimeOffset? End { get; set; }

}

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
            (range1.Start <= range2.Start && range2.Start <= range2.End && range2.End <= range1.End) ||
            (range2.Start <= range1.Start && range1.Start <= range2.End && range2.End <= range1.End) ||
            (range1.Start <= range2.Start && range2.Start <= range1.End && range1.End <= range2.End) ||
            (range2.Start <= range1.Start && range1.Start <= range1.End && range1.End <= range2.End);

    }
}
