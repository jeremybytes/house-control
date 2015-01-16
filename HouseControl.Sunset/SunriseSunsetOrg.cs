using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;


namespace HouseControl.Sunset
{
    public class SunriseSunsetOrg : ISunsetProvider
    {
        public DateTime GetSunset(DateTime date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.sunrise-sunset.org/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var apiString = string.Format("json?lat=33.8361&lng=-117.8897&date={0:yyyy-MM-dd}", date);
                HttpResponseMessage response = client.GetAsync(apiString).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    //string responseContent = "{\"results\":{\"sunrise\":\"2:56:10 PM\",\"sunset\":\"1:06:09 AM\",\"solar_noon\":\"8:01:09 PM\",\"day_length\":\"10:09:59\",\"civil_twilight_begin\":\"2:29:14 PM\",\"civil_twilight_end\":\"1:33:05 AM\",\"nautical_twilight_begin\":\"1:58:37 PM\",\"nautical_twilight_end\":\"2:03:42 AM\",\"astronomical_twilight_begin\":\"1:28:38 PM\",\"astronomical_twilight_end\":\"2:33:41 AM\"},\"status\":\"OK\"}";
                    dynamic contentObject = JsonConvert.DeserializeObject(responseContent);

                    if (contentObject.status != "OK")
                        return date;

                    string sunsetString = contentObject.results.sunset.ToString();
                    DateTime sunsetTime = DateTime.Parse(sunsetString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                    DateTime localTime = date.Date + sunsetTime.TimeOfDay;
                    return localTime;
                }
                return date;
            }
        }

    //{
    //  "results":
    //  {
    //    "sunrise":"7:27:02 AM",
    //    "sunset":"5:05:55 PM",
    //    "solar_noon":"12:16:28 PM",
    //    "day_length":"9:38:53",
    //    "civil_twilight_begin":"6:58:14 AM",
    //    "civil_twilight_end":"5:34:43 PM",
    //    "nautical_twilight_begin":"6:25:47 AM",
    //    "nautical_twilight_end":"6:07:10 PM",
    //    "astronomical_twilight_begin":"5:54:14 AM",
    //    "astronomical_twilight_end":"6:38:43 PM"
    //  },
    //   "status":"OK"
    //}


        private static void InitializeClient(HttpClient client)
        {
            client.BaseAddress = new Uri("http://api.sunrise-sunset.org/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
