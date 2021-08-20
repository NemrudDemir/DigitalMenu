using System;

namespace DigitalMenu.DataAccess.Entities
{
    [Flags]
    public enum DishAvailibility //NOT NEEDED ANYMORE - CHECK "Availibility.cs"
    {
        None = 0,
        Breakfast = 1,
        Dinner = 2,
        Lunch = 4,
        Weekdays = 8,
        Weekends = 16
    }
}
