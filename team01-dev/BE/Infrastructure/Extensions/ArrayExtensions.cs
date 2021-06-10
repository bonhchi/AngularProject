using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Extensions
{

    public static class ArrayExtensions
    {
        public static TimeSpan Average(this IEnumerable<TimeSpan> timeSpans)
        {
            IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
            double averageTicks = ticksPerTimeSpan.Average();
            long averageTicksLong = Convert.ToInt64(averageTicks);

            TimeSpan averageTimeSpan = TimeSpan.FromTicks(averageTicksLong);

            return averageTimeSpan;
        }
    }
}
