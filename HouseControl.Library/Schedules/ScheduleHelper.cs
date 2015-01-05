using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl.Library
{
    public class ScheduleHelper
    {
        public static DateTime RollForwardToNextDay(DateTime currentTime)
        {
            if (currentTime > DateTime.Now)
                return currentTime;
            return DateTime.Today + TimeSpan.FromDays(1) + currentTime.TimeOfDay;
        }

        public static DateTime RollForwardToNextWeekdayDay(DateTime currentTime)
        {
            var nextDay = RollForwardToNextDay(currentTime);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday
                || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        public static DateTime RollForwardToNextWeekendDay(DateTime currentTime)
        {
            var nextDay = RollForwardToNextDay(currentTime);
            while (nextDay.DayOfWeek != DayOfWeek.Saturday
                && nextDay.DayOfWeek != DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }
}
