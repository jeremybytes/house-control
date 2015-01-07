using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl.Library
{
    public static class ScheduleHelper
    {
        public static TimeSpan DurationFromNow(this DateTime checkTime)
        {
            return (checkTime - DateTime.Now).Duration();
        }

        public static bool IsInPast(this DateTime checkTime)
        {
            return checkTime < DateTime.Now;
        }

        public static DateTime RollForwardToNextDay(DateTime checkTime)
        {
            if (checkTime.IsInPast())
                return DateTime.Today + TimeSpan.FromDays(1) + checkTime.TimeOfDay;
            return checkTime;
        }

        public static DateTime RollForwardToNextWeekdayDay(DateTime checkTime)
        {
            var nextDay = RollForwardToNextDay(checkTime);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday
                || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        public static DateTime RollForwardToNextWeekendDay(DateTime checkTime)
        {
            var nextDay = RollForwardToNextDay(checkTime);
            while (nextDay.DayOfWeek != DayOfWeek.Saturday
                && nextDay.DayOfWeek != DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }
}
