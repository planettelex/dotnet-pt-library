/**
 * Copyright (c) 2012 Planet Telex Inc. all rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using NUnit.Framework;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.UnitTests.Utilities
{
    /// <summary>
    /// Unit tests for DateTimeUtility
    /// </summary>
    [TestFixture]
    public class DateTimeUtilityTests
    {
        private DateTimeUtility _dateTimeUtility;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _dateTimeUtility = new DateTimeUtility();
        }

        /// <summary>
        /// Get the next test.
        /// </summary>
        [Test]
        public void GetNextTest()
        {
            DateTime nextMon = _dateTimeUtility.GetNext(DateTime.Today, DayOfWeek.Monday);
            DateTime nextTues = _dateTimeUtility.GetNext(DateTime.Today, DayOfWeek.Tuesday);
            DateTime nextWeds = _dateTimeUtility.GetNext(DateTime.Today, DayOfWeek.Wednesday);
            DateTime nextThurs = _dateTimeUtility.GetNext(DateTime.Today, DayOfWeek.Thursday);
            DateTime nextFri = _dateTimeUtility.GetNext(DateTime.Today, DayOfWeek.Friday);
            DateTime nextSat = _dateTimeUtility.GetNext(DateTime.Today, DayOfWeek.Saturday);
            DateTime nextSun = _dateTimeUtility.GetNext(DateTime.Today, DayOfWeek.Sunday);

            Assert.That(nextMon.DayOfWeek == DayOfWeek.Monday && nextMon >= DateTime.Today && nextMon <= DateTime.Today.AddDays(7));
            Assert.That(nextTues.DayOfWeek == DayOfWeek.Tuesday && nextTues >= DateTime.Today && nextTues <= DateTime.Today.AddDays(7));
            Assert.That(nextWeds.DayOfWeek == DayOfWeek.Wednesday && nextWeds >= DateTime.Today && nextWeds <= DateTime.Today.AddDays(7));
            Assert.That(nextThurs.DayOfWeek == DayOfWeek.Thursday && nextThurs >= DateTime.Today && nextThurs <= DateTime.Today.AddDays(7));
            Assert.That(nextFri.DayOfWeek == DayOfWeek.Friday && nextFri >= DateTime.Today && nextFri <= DateTime.Today.AddDays(7));
            Assert.That(nextSat.DayOfWeek == DayOfWeek.Saturday && nextSat >= DateTime.Today && nextSat <= DateTime.Today.AddDays(7));
            Assert.That(nextSun.DayOfWeek == DayOfWeek.Sunday && nextSun >= DateTime.Today && nextSun <= DateTime.Today.AddDays(7));
        }

        /// <summary>
        /// Get the last test.
        /// </summary>
        [Test]
        public void GetLastTest()
        {
            DateTime lastMon = _dateTimeUtility.GetLast(DateTime.Today, DayOfWeek.Monday);
            DateTime lastTues = _dateTimeUtility.GetLast(DateTime.Today, DayOfWeek.Tuesday);
            DateTime lastWeds = _dateTimeUtility.GetLast(DateTime.Today, DayOfWeek.Wednesday);
            DateTime lastThurs = _dateTimeUtility.GetLast(DateTime.Today, DayOfWeek.Thursday);
            DateTime lastFri = _dateTimeUtility.GetLast(DateTime.Today, DayOfWeek.Friday);
            DateTime lastSat = _dateTimeUtility.GetLast(DateTime.Today, DayOfWeek.Saturday);
            DateTime lastSun = _dateTimeUtility.GetLast(DateTime.Today, DayOfWeek.Sunday);

            Assert.That(lastMon.DayOfWeek == DayOfWeek.Monday && lastMon <= DateTime.Today && lastMon >= DateTime.Today.AddDays(-7));
            Assert.That(lastTues.DayOfWeek == DayOfWeek.Tuesday && lastTues <= DateTime.Today && lastTues >= DateTime.Today.AddDays(-7));
            Assert.That(lastWeds.DayOfWeek == DayOfWeek.Wednesday && lastWeds <= DateTime.Today && lastWeds >= DateTime.Today.AddDays(-7));
            Assert.That(lastThurs.DayOfWeek == DayOfWeek.Thursday && lastThurs <= DateTime.Today && lastThurs >= DateTime.Today.AddDays(-7));
            Assert.That(lastFri.DayOfWeek == DayOfWeek.Friday && lastFri <= DateTime.Today && lastFri >= DateTime.Today.AddDays(-7));
            Assert.That(lastSat.DayOfWeek == DayOfWeek.Saturday && lastSat <= DateTime.Today && lastSat >= DateTime.Today.AddDays(-7));
            Assert.That(lastSun.DayOfWeek == DayOfWeek.Sunday && lastSun <= DateTime.Today && lastSun >= DateTime.Today.AddDays(-7));
        }
        /// <summary>
        /// Test for Day of the Week conversion to String.
        /// </summary>
        [Test]
        public void GetDayOfWeekTest()
        {
            //check for Sunday
            string strTemp = _dateTimeUtility.GetDayOfWeekString(DayOfWeek.Sunday);
            Assert.That(strTemp == "Sunday");
            //check for Monday
            strTemp = _dateTimeUtility.GetDayOfWeekString(DayOfWeek.Monday);
            Assert.That(strTemp == "Monday");
            //check for Tuesday
            strTemp = _dateTimeUtility.GetDayOfWeekString(DayOfWeek.Tuesday);
            Assert.That(strTemp == "Tuesday");
            //check for Wednesday
            strTemp = _dateTimeUtility.GetDayOfWeekString(DayOfWeek.Wednesday);
            Assert.That(strTemp == "Wednesday");
            //check for Thursday
            strTemp = _dateTimeUtility.GetDayOfWeekString(DayOfWeek.Thursday);
            Assert.That(strTemp == "Thursday");
            //check for Friday
            strTemp = _dateTimeUtility.GetDayOfWeekString(DayOfWeek.Friday);
            Assert.That(strTemp == "Friday");
            //check for Saturday
            strTemp = _dateTimeUtility.GetDayOfWeekString(DayOfWeek.Saturday);
            Assert.That(strTemp == "Saturday");
        }

        /// <summary>
        /// Test for GetTimeAgoString
        /// </summary>
        [Test]
        public void GetTimeAgoStringTest()
        {
            DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
            Assert.That(_dateTimeUtility.GetTimeAgoString(fiveDaysAgo, TimeMode.Local) == "5 days ago");

            DateTime oneDayAgo = DateTime.Now.AddDays(-1);
            Assert.That(_dateTimeUtility.GetTimeAgoString(oneDayAgo, TimeMode.Local) == "a day ago");

            DateTime threeHoursAgo = DateTime.Now.AddHours(-3);
            Assert.That(_dateTimeUtility.GetTimeAgoString(threeHoursAgo, TimeMode.Local) == "about 3 hours ago");

            DateTime oneHourAgo = DateTime.Now.AddHours(-1.5);
            Assert.That(_dateTimeUtility.GetTimeAgoString(oneHourAgo, TimeMode.Local) == "about an hour ago");

            DateTime twelveMinutesAgo = DateTime.Now.AddMinutes(-12);
            Assert.That(_dateTimeUtility.GetTimeAgoString(twelveMinutesAgo, TimeMode.Local) == "12 minutes ago");

            DateTime oneMinuteAgo = DateTime.Now.AddMinutes(-1);
            Assert.That(_dateTimeUtility.GetTimeAgoString(oneMinuteAgo, TimeMode.Local) == "a minute ago");

            DateTime tenSecondsAgo = DateTime.Now.AddSeconds(-10);
            Assert.That(_dateTimeUtility.GetTimeAgoString(tenSecondsAgo, TimeMode.Local) == "less than a minute ago");
        }
    }
}