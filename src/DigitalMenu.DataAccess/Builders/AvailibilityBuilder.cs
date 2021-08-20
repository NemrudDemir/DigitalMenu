using DigitalMenu.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DigitalMenu.DataAccess.Builders
{
    public class AvailibilityBuilder
    {
        private readonly HashSet<DayOfWeek> _daysOfWeek = new HashSet<DayOfWeek>();
        private readonly List<TimeStartEnd> _timeStartEnds = new List<TimeStartEnd>();

        public AvailibilityBuilder AddDay(DayOfWeek dayOfWeek) => AddDays(dayOfWeek);

        public AvailibilityBuilder AddWeekdays() => AddDays(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);

        public AvailibilityBuilder AddWeekends() => AddDays(DayOfWeek.Saturday, DayOfWeek.Sunday);
        
        public AvailibilityBuilder AddAllDaysOfWeek() => AddDays(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday);

        public AvailibilityBuilder AddDays(params DayOfWeek[] daysOfWeek)
        {
            foreach (var dayOfWeek in daysOfWeek)
                _daysOfWeek.Add(dayOfWeek);

            return this;
        }

        public AvailibilityBuilder AddTimeStartEnd(TimeSpan start, TimeSpan end)
        {
            _timeStartEnds.Add(new TimeStartEnd(start, end));
            return this;
        }

        public Availibility Create()
        {
            return new Availibility
            {
                DaysOfWeek = new HashSet<DayOfWeek>(_daysOfWeek),
                TimeStartEnds = _timeStartEnds.ToArray()
            };
        }
    }
}
