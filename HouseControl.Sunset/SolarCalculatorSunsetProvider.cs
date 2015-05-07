using Innovative.SolarCalculator;
using System;

namespace HouseControl.Sunset
{
    public class SolarCalculatorSunsetProvider : ISunsetProvider
    {
        public DateTime GetSunset(DateTime date)
        {
            var solarTimes = new SolarTimes(date, 33.8361, -117.8897);
            return solarTimes.Sunset;
        }

        public DateTime GetSunrise(DateTime date)
        {
            var solarTimes = new SolarTimes(date, 33.8361, -117.8897);
            return solarTimes.Sunrise;
        }
    }
}
