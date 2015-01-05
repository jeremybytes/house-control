using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HouseControl.Library.Test
{
    [TestClass]
    public class ScheduleHelperTests
    {
        [TestMethod]
        public void MondayItemInFuture_OnRollDay_IsUnchanged()
        {
            // Arrange
            DateTime monday = new DateTime(2020, 01, 06, 15, 32, 00);
            Assert.AreEqual(DayOfWeek.Monday, monday.DayOfWeek);

            // Act
            var newDate = ScheduleHelper.RollForwardToNextDay(monday);

            // Assert
            Assert.AreEqual(monday, newDate);
        }

        [TestMethod]
        public void MondayItemInFuture_OnRollWeekdayDay_IsUnchanged()
        {
            // Arrange
            DateTime monday = new DateTime(2020, 01, 06, 15, 32, 00);
            Assert.AreEqual(DayOfWeek.Monday, monday.DayOfWeek);

            // Act
            var newDate = ScheduleHelper.RollForwardToNextWeekdayDay(monday);

            // Assert
            Assert.AreEqual(monday, newDate);
        }

        [TestMethod]
        public void MondayItemInFuture_OnRollWeekendDay_IsSaturday()
        {
            // Arrange
            DateTime monday = new DateTime(2020, 01, 06, 15, 32, 00);
            Assert.AreEqual(DayOfWeek.Monday, monday.DayOfWeek);
            DateTime saturday = new DateTime(2020, 01, 11, 15, 32, 00);

            // Act
            var newDate = ScheduleHelper.RollForwardToNextWeekendDay(monday);

            // Assert
            Assert.AreEqual(saturday, newDate);
        }

        [TestMethod]
        public void SaturdayItemInFuture_OnRollDay_IsUnchanged()
        {
            // Arrange
            DateTime saturday = new DateTime(2020, 01, 11, 15, 32, 00);
            Assert.AreEqual(DayOfWeek.Saturday, saturday.DayOfWeek);

            // Act
            var newDate = ScheduleHelper.RollForwardToNextDay(saturday);

            // Assert
            Assert.AreEqual(saturday, newDate);
        }

        [TestMethod]
        public void SaturdayItemInFuture_OnRollWeekdayDay_IsMonday()
        {
            // Arrange
            DateTime saturday = new DateTime(2020, 01, 11, 15, 32, 00);
            Assert.AreEqual(DayOfWeek.Saturday, saturday.DayOfWeek);
            DateTime monday = new DateTime(2020, 01, 13, 15, 32, 00);

            // Act
            var newDate = ScheduleHelper.RollForwardToNextWeekdayDay(saturday);

            // Assert
            Assert.AreEqual(monday, newDate);
        }

        [TestMethod]
        public void SaturdayItemInFuture_OnRollWeekendDay_IsUnchanged()
        {
            // Arrange
            DateTime saturday = new DateTime(2020, 01, 11, 15, 32, 00);
            Assert.AreEqual(DayOfWeek.Saturday, saturday.DayOfWeek);

            // Act
            var newDate = ScheduleHelper.RollForwardToNextWeekendDay(saturday);

            // Assert
            Assert.AreEqual(saturday, newDate);
        }

        [TestMethod]
        public void MondayItemInPast_OnRollDay_IsTomorrow()
        {
            // TODO: to do really good testing on schedules
            // we need to abstract out the "DateTime.Now"
            // functionality.
            Assert.Fail();
        }
    }
}
