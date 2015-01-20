using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseControl.Sunset;

namespace HouseControl.Library
{
    public static class ScheduleHelper
    {
        private static ITimeProvider timeProvider;
        public static ITimeProvider TimeProvider
        {
            get
            {
                if (timeProvider == null)
                    timeProvider = new CurrentTimeProvider();
                return timeProvider;
            }
            set { timeProvider = value; }
        }

        private static ISunsetProvider sunsetProvider;
        public static ISunsetProvider SunsetProvider
        {
            get
            {
                if (sunsetProvider == null)
                    sunsetProvider = new SunriseSunsetOrg();
                return sunsetProvider;
            }
            set { sunsetProvider = value; }
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

        public static DateTime RollForwardToNextDay(DateTime checkTime,
            ScheduleTimeType timeType)
        {
            if (checkTime.IsInPast())
                switch (timeType)
                {
                    case ScheduleTimeType.Standard:
                        return Today() + TimeSpan.FromDays(1) + checkTime.TimeOfDay;
                    case ScheduleTimeType.Sunset:
                        return SunsetProvider.GetSunset(Today() + TimeSpan.FromDays(1));
                    case ScheduleTimeType.Sunrise:
                        return SunsetProvider.GetSunrise(Today() + TimeSpan.FromDays(1));
                }
            return checkTime;
        }

        public static DateTime RollForwardToNextWeekdayDay(DateTime checkTime,
            ScheduleTimeType timeType)
        {
            var nextDay = RollForwardToNextDay(checkTime, timeType);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday
                || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        public static DateTime RollForwardToNextWeekendDay(DateTime checkTime,
            ScheduleTimeType timeType)
        {
            var nextDay = RollForwardToNextDay(checkTime, timeType);
            while (nextDay.DayOfWeek != DayOfWeek.Saturday
                && nextDay.DayOfWeek != DayOfWeek.Sunday)
            {
                nextDay = nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }
}
