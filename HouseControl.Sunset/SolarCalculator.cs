using Innovative.SolarCalculator;
using System;

namespace HouseControl.Sunset
{
    public class SolarCalculator : ISunsetProvider
    {
        public DateTime GetSunset(DateTime date)
        {
            TimeZoneInfo pst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            SolarTimes solarTimes = new SolarTimes(date, 33.8361, -117.8897);
            return TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunset.ToUniversalTime(), pst);
        }

        public DateTime GetSunrise(DateTime date)
        {
            TimeZoneInfo pst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            SolarTimes solarTimes = new SolarTimes(date, 33.8361, -117.8897);
            return TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunrise.ToUniversalTime(), pst);
        }
    }
}
