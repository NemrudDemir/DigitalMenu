using System;
using System.Collections.Generic;

namespace DigitalMenu.DataAccess.Entities
{
    public class Availibility
    {
        public IEnumerable<DayOfWeek> DaysOfWeek { get; set; }
        public IEnumerable<TimeStartEnd> TimeStartEnds { get; set; }
    }
}