using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace HouseControl.Sunset
{
    public class ResponseContentString
    {
        public string Value { get; set; }
        public ResponseContentString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "Value cannot be null");
            }
            try
            {
                JsonConvert.DeserializeObject(value);
            }
            catch
            {
                throw new ArgumentException("value", "Value is not a valid JSON object");
            }

            Value = value;
        }
    }

    public class UTCTimeString
    {
        public string Value { get; set; }
        public UTCTimeString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "Value cannot be null");
            }
            DateTime time;
            if (!DateTime.TryParse(value, out time))
            {
                throw new ArgumentException("value", "Value is not a valid time string");
            }

            Value = value;
        }
    }

    public class SunriseSunsetOrg : ISunsetProvider
    {
        private static ResponseContentString cacheData;
        private static DateTime cacheDate;

        public DateTime GetSunset(DateTime date)
        {
            RefreshCache(date);
            return cacheData.GetSunsetString().GetLocalTime(date);
        }

        public DateTime GetSunrise(DateTime date)
        {
            RefreshCache(date);
            return cacheData.GetSunriseString().GetLocalTime(date);
        }

        private void RefreshCache(DateTime date)
        {
            if (cacheDate != date)
            {
                //cacheData = "{\"results\":{\"sunrise\":\"2:56:10 PM\",\"sunset\":\"1:06:09 AM\",\"solar_noon\":\"8:01:09 PM\",\"day_length\":\"10:09:59\",\"civil_twilight_begin\":\"2:29:14 PM\",\"civil_twilight_end\":\"1:33:05 AM\",\"nautical_twilight_begin\":\"1:58:37 PM\",\"nautical_twilight_end\":\"2:03:42 AM\",\"astronomical_twilight_begin\":\"1:28:38 PM\",\"astronomical_twilight_end\":\"2:33:41 AM\"},\"status\":\"OK\"}";
                cacheData = GetContentFromService(date).Result;
                cacheDate = date;
            }
        }

        private static async Task<ResponseContentString> GetContentFromService(DateTime date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.sunrise-sunset.org/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var apiString = string.Format(
                    "json?lat=33.8361&lng=-117.8897&date={0:yyyy-MM-dd}", date);
                HttpResponseMessage response = await client.GetAsync(apiString);

                if (!response.IsSuccessStatusCode)
                    return null;
                var responseString = await response.Content.ReadAsStringAsync();
                return new ResponseContentString(responseString);
            }
        }
    }

    public static class SunriseSunsetOrgHelpers
    {
        public static UTCTimeString GetSunsetString(this ResponseContentString responseContent)
        {
            if (responseContent == null)
                return null;

            dynamic contentObject = JsonConvert.DeserializeObject(responseContent.Value);
            if (contentObject.status != "OK")
                return null;
            var timeString = contentObject.results.sunset.ToString();
            return new UTCTimeString(timeString);
        }

        public static UTCTimeString GetSunriseString(this ResponseContentString responseContent)
        {
            if (responseContent == null)
                return null;

            dynamic contentObject = JsonConvert.DeserializeObject(responseContent.Value);
            if (contentObject.status != "OK")
                return null;
            var timeString = contentObject.results.sunrise.ToString();
            return new UTCTimeString(timeString);
        }

        public static DateTime GetLocalTime(this UTCTimeString sunsetString, DateTime date)
        {
            if (sunsetString == null)
                return date;
            DateTime sunsetTime = DateTime.Parse(sunsetString.Value,
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            DateTime localTime = date.Date + sunsetTime.TimeOfDay;
            return localTime;
        }
    }
}
