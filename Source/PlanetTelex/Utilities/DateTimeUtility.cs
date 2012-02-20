using System;
using System.Collections.ObjectModel;
using System.Linq;
using PlanetTelex.Common;
using PlanetTelex.Properties;
using PlanetTelex.Extensions;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods that assist in <see cref="DateTime"/> manipulation.
    /// </summary>
    public class DateTimeUtility
    {
        #region Static System Methods

        /// <summary>
        /// Gets all system time zones.
        /// </summary>
        public static ReadOnlyCollection<TimeZoneInfo> AllSystemTimeZones()
        {
            if (_allSystemTimeZones == null)
                _allSystemTimeZones = TimeZoneInfo.GetSystemTimeZones();

            return _allSystemTimeZones;
        }
        private static ReadOnlyCollection<TimeZoneInfo> _allSystemTimeZones;

        /// <summary>
        /// Gets a system time zone for the specified system ID.
        /// </summary>
        /// <returns>A <see cref="TimeZoneInfo"/>.</returns>
        public static TimeZoneInfo GetSystemTimeZone(string systemTimeZoneId)
        {
            if (systemTimeZoneId.IsNullOrEmpty())
                throw new ArgumentNullException("systemTimeZoneId");

            return AllSystemTimeZones().First(systemTimeZone => systemTimeZone.Id.Equals(systemTimeZoneId, StringComparison.CurrentCultureIgnoreCase));
        }

        #endregion

        #region Semantic Lookup Methods

        /// <summary>
        /// Returns the date for the next day of the week given after the specified date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="targetDay">The day of the week to get the next occurance of.</param>
        /// <returns>The next DateTime that shares the specified DateTime's day of the week.</returns>
        public virtual DateTime GetNext(DateTime fromDate, DayOfWeek targetDay)
        {
            int dow = (int)fromDate.DayOfWeek;

            if (dow <= (int)targetDay)
                return fromDate.AddDays((int)targetDay - dow);
            
            return fromDate.AddDays((7 - dow) + (int)targetDay);
        }

        /// <summary>
        /// Returns the date for the last day of the week given before the specified date.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="targetDay">The day of the week to get the last occurance of.</param>
        /// <returns>The last DateTime that shares the specified DateTime's day of the week.</returns>
        public virtual DateTime GetLast(DateTime fromDate, DayOfWeek targetDay)
        {
            int dow = (int)fromDate.DayOfWeek;

            if (dow > (int)targetDay)
                return fromDate.AddDays(-(dow - (int)targetDay));
            
            return fromDate.AddDays(-(7 - ((int)targetDay - dow)));
        }

        #endregion

        #region String Generation Methods

        /// <summary>
        /// Gets a resourced string for the day of the week provided.
        /// </summary>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns>The resourced string representation of the day of the week provided.</returns>
        public virtual string GetDayOfWeekString(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return Resources.Sunday;
                case DayOfWeek.Monday:
                    return Resources.Monday;
                case DayOfWeek.Tuesday:
                    return Resources.Tuesday;
                case DayOfWeek.Wednesday:
                    return Resources.Wednesday;
                case DayOfWeek.Thursday:
                    return Resources.Thursday;
                case DayOfWeek.Friday:
                    return Resources.Friday;
                case DayOfWeek.Saturday:
                    return Resources.Saturday;
                default:
                    return Resources.Sunday;
            }
        }

        /// <summary>
        /// Generates a string describing how long ago a given DateTime was from now.
        /// </summary>
        /// <param name="pastDateTime">The past date time.</param>
        /// <param name="timeMode">UTC or Local.</param>
        /// <returns>A string representing time since the date.</returns>
        public virtual string GetTimeAgoString(DateTime pastDateTime, TimeMode timeMode)
        {
            DateTime now = timeMode == TimeMode.Utc ? DateTime.UtcNow : DateTime.Now;
            TimeSpan timeSpan = now.Subtract(pastDateTime);
            const int minutesInDay = 1440;
            int minutesInMonth = minutesInDay * DateTime.DaysInMonth(pastDateTime.Year, pastDateTime.Month);
            int daysInYear = DateTime.IsLeapYear(pastDateTime.Year) ? 366 : 365;
            int daysInMonth = minutesInMonth / minutesInDay;
            int years = timeSpan.Days / daysInYear;
            int months = (timeSpan.Days % daysInYear) / daysInMonth;
            int days = (timeSpan.Days - (years * daysInYear) - (months * daysInMonth));
            int minutesInYear = minutesInDay * daysInYear;
            int minutes = Convert.ToInt32(timeSpan.TotalMinutes);

            if (minutes < 1)
                return Resources.TimeAgoLessThanAMinute;

            if (minutes >= 1 && minutes < 2)
                return Resources.TimeAgoAMinute;

            if (minutes >= 2 && minutes < 60)
                return string.Format(Resources.TimeAgoLessThanAnHour, timeSpan.Minutes);

            if (minutes >= 60 && minutes < 120)
                return Resources.TimeAgoAnHour;

            if (minutes >= 120 && minutes < 1440)
                return string.Format(Resources.TimeAgoLessThanADay, timeSpan.Hours);

            if (minutes >= 1440 && minutes < 2880)
                return Resources.TimeAgoADay;

            if (minutes >= 2880 && minutes < minutesInMonth)
                return string.Format(Resources.TimeAgoMoreThanADay, timeSpan.Days);

            if (minutes >= minutesInMonth && minutes < minutesInYear)
                return string.Format(Resources.TimeAgoMoreThanAMonth, months);

            if (minutes >= minutesInYear && minutes < minutesInYear * 2)
                return string.Format(Resources.TimeAgoMoreThanAYear, months, days);

            return string.Format(Resources.TimeAgoMoreThanTwoYears, years, months, days);
        }

        #endregion
    }
}