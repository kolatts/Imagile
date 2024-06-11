using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.Extensions;

public static class DateTimeOffsetExtensions
{
  
    public static DateTimeOffset RoundToNearest(this DateTimeOffset dto, TimeSpan d)
    {
        var ticks = (long)Math.Round(dto.Ticks / (double)d.Ticks) * d.Ticks;
        return new DateTimeOffset(ticks, dto.Offset);
    }

    public static DateTimeOffset RoundDown(this DateTimeOffset dto, TimeSpan d)
    {
        var ticks = (long)Math.Floor(dto.Ticks / (double)d.Ticks) * d.Ticks;
        return new DateTimeOffset(ticks, dto.Offset);
    }
    public static DateTimeOffset RoundUp(this DateTimeOffset dto, TimeSpan d)
    {
        var ticks = (long)Math.Ceiling(dto.Ticks / (double)d.Ticks) * d.Ticks;
        return new DateTimeOffset(ticks, dto.Offset);
    }
}