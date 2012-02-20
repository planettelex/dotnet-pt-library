using System;
using System.Collections.Generic;
using NUnit.Framework;
using PlanetTelex.Common.Models;

namespace PlanetTelex.UnitTests.Common.Models
{
    /// <summary>
    /// Unit tests for <see cref="HoursOfOperation"/>.
    /// </summary>
    [TestFixture]
    public class HoursOfOperationTests
    {
        /// <summary>
        /// Test for SetOpenTime - Sets the time to open for a given day.
        /// </summary>
        [Test]
        public void SetOpenTimeTest()
        {
            HoursOfOperation hor = new HoursOfOperation();

            DayOfWeek dowDay = DayOfWeek.Monday;
            DateTime dtmTime = DateTime.Parse("8:39 AM");
            hor.SetOpenTime(dowDay, dtmTime);
            List<string> openTimes = hor.OpenTimes(dowDay);
            string str1 = "8:30 AM";
            string str2 = openTimes[0];
            Assert.That(str1 == str2);

            dowDay = DayOfWeek.Thursday;
            dtmTime = DateTime.Parse("10:36 AM");
            hor.SetOpenTime(dowDay, dtmTime);
            openTimes = hor.OpenTimes(dowDay);
            str1 = "10:30 AM";
            str2 = openTimes[0];
            Assert.That(str1 == str2);
        }

        /// <summary>
        /// TEST for SetCloseTime - Sets the time to close for a given day.
        /// </summary>
        [Test]
        public void SetCloseTimeTest()
        {
            HoursOfOperation hor = new HoursOfOperation();

            DayOfWeek dowDay = DayOfWeek.Monday;
            DateTime dtmTime = DateTime.Parse("5:39 PM");
            hor.SetCloseTime(dowDay, dtmTime);
            List<string> openTimes = hor.OpenTimes(dowDay);
            int intLen = openTimes.Count - 1;
            string str1 = "5:30 PM";
            string str2 = openTimes[intLen];
            Assert.That(str1 == str2);

            dowDay = DayOfWeek.Thursday;
            dtmTime = DateTime.Parse("6:36 PM");
            hor.SetCloseTime(dowDay, dtmTime);
            openTimes = hor.OpenTimes(dowDay);
            intLen = openTimes.Count - 1;
            str1 = "6:30 PM";
            str2 = openTimes[intLen];
            Assert.That(str1 == str2);
        }

        /// <summary>
        /// TEST for OpenTimes - Tests creation of ArrayList of HoursOfOperation.
        /// </summary>
        [Test]
        public void OpenTimesTest()
        {
            HoursOfOperation hor = new HoursOfOperation();

            DayOfWeek dowDay = DayOfWeek.Monday;
            DateTime dtmTime = DateTime.Parse("6:39 AM");
            hor.SetOpenTime(dowDay, dtmTime);
            dtmTime = DateTime.Parse("11:31 PM");
            hor.SetCloseTime(dowDay, dtmTime);

            List<string> openTimes = hor.OpenTimes(dowDay);
            string str1 = "6:30 AM";
            string str2 = openTimes[0];
            Assert.That(str1 == str2);

            str1 = "11:30 PM";
            str2 = openTimes[openTimes.Count - 1];
            Assert.That(str1 == str2);

            dowDay = DayOfWeek.Thursday;
            dtmTime = DateTime.Parse("00:39 AM");
            hor.SetOpenTime(dowDay, dtmTime);
            dtmTime = DateTime.Parse("11:31 PM");
            hor.SetCloseTime(dowDay, dtmTime);

            openTimes = hor.OpenTimes(dowDay);
            str1 = "12:30 AM";
            str2 = openTimes[0];
            Assert.That(str1 == str2);

            str1 = "11:30 PM";
            str2 = openTimes[openTimes.Count - 1];
            Assert.That(str1 == str2);
        }        
    }
}
