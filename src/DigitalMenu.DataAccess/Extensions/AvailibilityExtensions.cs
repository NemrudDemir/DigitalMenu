using DigitalMenu.DataAccess.Entities;
using System;
using System.Linq;

namespace DigitalMenu.DataAccess.Extensions
{
    public static class AvailibilityExtensions
    {
        /// <summary>
        /// Checks if the dish is available on the given date and time
        /// </summary>
        /// <param name="availibility"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public static bool IsAvailable(this DishAvailibility availibility, DateTime onDate) //DEPRECATED - CHECK "Availibility.cs"
        {
            var isWeekdayHit = availibility.HasFlag(DishAvailibility.Weekdays) && onDate.DayOfWeek >= DayOfWeek.Monday && onDate.DayOfWeek <= DayOfWeek.Friday;
            var isWeekendHit = availibility.HasFlag(DishAvailibility.Weekends) && (onDate.DayOfWeek == DayOfWeek.Saturday || onDate.DayOfWeek == DayOfWeek.Sunday);
            var isDayOfWeekHit = isWeekdayHit || isWeekendHit;

            if (!isDayOfWeekHit)
                return false;

            var breakfastStart = new TimeSpan(7, 0, 0); //ggf. auslagern und anpassen
            var breakfastEnd = new TimeSpan(11, 0, 0); //ggf. auslagern und anpassen
            var dinnerStart = new TimeSpan(12, 0, 0); //ggf. auslagern und anpassen
            var dinnerEnd = new TimeSpan(14, 30, 0); //ggf. auslagern und anpassen
            var lunchStart = new TimeSpan(16, 0, 0); //ggf. auslagern und anpassen
            var lunchEnd = new TimeSpan(20, 0, 0); //ggf. auslagern und anpassen

            var isBreakfastHit = availibility.HasFlag(DishAvailibility.Breakfast) && onDate.TimeOfDay >= breakfastStart && onDate.TimeOfDay <= breakfastEnd;
            var isDinnerHit = availibility.HasFlag(DishAvailibility.Dinner) && onDate.TimeOfDay >= dinnerStart && onDate.TimeOfDay <= dinnerEnd;
            var isLunchHit = availibility.HasFlag(DishAvailibility.Lunch) && onDate.TimeOfDay >= lunchStart && onDate.TimeOfDay <= lunchEnd;
            var isTimeHit = isBreakfastHit || isDinnerHit || isLunchHit;

            return isDayOfWeekHit && isTimeHit;
        }


        public static bool IsAvailable(this Availibility availibility) => IsAvailable(availibility, DateTime.Now);

        /// <summary>
        /// Checks the availibility on the given date and time
        /// </summary>
        /// <param name="onDateTime"></param>
        /// <returns></returns>
        public static bool IsAvailable(this Availibility availibility, DateTime onDateTime)
        {
            if (availibility?.DaysOfWeek?.Any() != true || availibility?.TimeStartEnds?.Any() != true)
                return false;

            var isDayOfWeekHit = availibility.DaysOfWeek.Contains(onDateTime.DayOfWeek);
            if (!isDayOfWeekHit)
                return false;

            var isTimeHit = availibility.TimeStartEnds.Any(t => t.Start <= onDateTime.TimeOfDay && t.End >= onDateTime.TimeOfDay);

            return isDayOfWeekHit && isTimeHit;
        }
    }
}
