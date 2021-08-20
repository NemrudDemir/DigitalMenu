using System;

namespace DigitalMenu.DataAccess.Entities
{
    public class TimeStartEnd
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        public TimeStartEnd(TimeSpan start, TimeSpan end)
        {
            if (!IsTimeSpanValid(start))
                throw new ArgumentException(nameof(start));

            if(!IsTimeSpanValid(end))
                throw new ArgumentException(nameof(end));

            if(end < start)
            {
                var temp = start;
                start = end;
                end = temp;
            }

            Start = start;
            End = end;
        }

        private static bool IsTimeSpanValid(TimeSpan timeSpan)
        {
            return timeSpan.Days == 0 && timeSpan.TotalDays <= 1;
        }
    }
}
