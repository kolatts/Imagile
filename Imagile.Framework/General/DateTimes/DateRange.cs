using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagile.Domain.General.DateTimes;
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