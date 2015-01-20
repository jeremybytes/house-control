using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace HouseControl.Sunset
{
    public class SunriseSunsetOrg : ISunsetProvider
    {
        public DateTime GetSunset(DateTime date)
        {
            string responseContent = GetContentFromService(date).Result;
            string sunsetString = responseContent.GetSunsetString();
            DateTime localTime = sunsetString.GetLocalTime(date);
            return localTime;
        }

        public DateTime GetSunrise(DateTime date)
        {
            string responseContent = GetContentFromService(date).Result;
            string sunriseString = responseContent.GetSunriseString();
            DateTime localTime = sunriseString.GetLocalTime(date);
            return localTime;
        }

        private static async Task<string> GetContentFromService(DateTime date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.sunrise-sunset.org/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var apiString = string.Format(
                    "json?lat=33.8361&lng=-117.8897&date={0:yyyy-MM-dd}", date);
                HttpResponseMessage response = await client.GetAsync(apiString);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                return null;
            }
        }
    }

    public static class SunriseSunsetOrgHelpers
    {
        public static string GetSunsetString(this string responseContent)
        {
            if (responseContent == null)
                return null;

            //string responseContent = "{\"results\":{\"sunrise\":\"2:56:10 PM\",\"sunset\":\"1:06:09 AM\",\"solar_noon\":\"8:01:09 PM\",\"day_length\":\"10:09:59\",\"civil_twilight_begin\":\"2:29:14 PM\",\"civil_twilight_end\":\"1:33:05 AM\",\"nautical_twilight_begin\":\"1:58:37 PM\",\"nautical_twilight_end\":\"2:03:42 AM\",\"astronomical_twilight_begin\":\"1:28:38 PM\",\"astronomical_twilight_end\":\"2:33:41 AM\"},\"status\":\"OK\"}";
            dynamic contentObject = JsonConvert.DeserializeObject(responseContent);
            if (contentObject.status != "OK")
                return null;
            return contentObject.results.sunset.ToString();
        }

        public static string GetSunriseString(this string responseContent)
        {
            if (responseContent == null)
                return null;

            //string responseContent = "{\"results\":{\"sunrise\":\"2:56:10 PM\",\"sunset\":\"1:06:09 AM\",\"solar_noon\":\"8:01:09 PM\",\"day_length\":\"10:09:59\",\"civil_twilight_begin\":\"2:29:14 PM\",\"civil_twilight_end\":\"1:33:05 AM\",\"nautical_twilight_begin\":\"1:58:37 PM\",\"nautical_twilight_end\":\"2:03:42 AM\",\"astronomical_twilight_begin\":\"1:28:38 PM\",\"astronomical_twilight_end\":\"2:33:41 AM\"},\"status\":\"OK\"}";
            dynamic contentObject = JsonConvert.DeserializeObject(responseContent);
            if (contentObject.status != "OK")
                return null;
            return contentObject.results.sunrise.ToString();
        }

        public static DateTime GetLocalTime(this string sunsetString, DateTime date)
        {
            if (sunsetString == null)
                return date;
            DateTime sunsetTime = DateTime.Parse(sunsetString, 
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            DateTime localTime = date.Date + sunsetTime.TimeOfDay;
            return localTime;
        }
    }
}
