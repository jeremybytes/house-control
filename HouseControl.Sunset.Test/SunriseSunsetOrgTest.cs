using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HouseControl.Sunset.Test
{
    [TestClass]
    public class SunriseSunsetOrgTest
    {
        [TestMethod]
        public void UTCTimeString_WithValidValue_IsValid()
        {
            string value = "01:20:30 AM";
            var timeString = new UTCTimeString(value);
            Assert.AreEqual(value, timeString.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UTCTimeString_WithNull_ThrowsException()
        {
            string value = null;
            var timeString = new UTCTimeString(value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UTCTimeString_WithInvalidValue_ThrowsException()
        {
            string value = "Hello";
            var timeString = new UTCTimeString(value);
        }

        [TestMethod]
        public void ResponseContentString_WithJSONValue_IsValid()
        {
            string value = "{\"results\":{\"sunrise\":\"2:56:10 PM\",\"sunset\":\"1:06:09 AM\",\"solar_noon\":\"8:01:09 PM\",\"day_length\":\"10:09:59\",\"civil_twilight_begin\":\"2:29:14 PM\",\"civil_twilight_end\":\"1:33:05 AM\",\"nautical_twilight_begin\":\"1:58:37 PM\",\"nautical_twilight_end\":\"2:03:42 AM\",\"astronomical_twilight_begin\":\"1:28:38 PM\",\"astronomical_twilight_end\":\"2:33:41 AM\"},\"status\":\"OK\"}";
            var contentString = new ResponseContentString(value);
            Assert.AreEqual(value, contentString.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResponseContentString_WithNulValue_ThrowsException()
        {
            string value = null;
            var contentString = new ResponseContentString(value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ResponseContentString_WithInvalidValue_ThrowsException()
        {
            string value = "Hello";
            var contentString = new ResponseContentString(value);
        }
    }
}
