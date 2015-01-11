using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl.Library
{
    public static class ScheduleHelper
    {
        private static ITimeProvider timeProvider;
        public static ITimeProvider TimeProvider
        {
            get {
                if (timeProvider == null)
                    timeProvider = new CurrentTimeProvider();
                return timeProvider; }
            set { timeProvider = value; }
        }

        public static DateTime Now()
        {
            return TimeProvider.Now();
        }

        public static DateTime Today()
        {
            return TimeProvider.Now().Date;
        }

        public static TimeSpan DurationFromNow(this DateTime checkTime)
        {
            return (checkTime - TimeProvider.Now()).Duration();
        }

        public static bool IsInPast(this DateTime checkTime)
        {
            return checkTime < TimeProvider.Now();
        }

        public static DateTime RollForwardToNextDay(DateTime checkTime)
        {
            if (checkTime.IsInPast())
                return Today() + TimeSpan.FromDays(1) + checkTime.TimeOfDay;
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
