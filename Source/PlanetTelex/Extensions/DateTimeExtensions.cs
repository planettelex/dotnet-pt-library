using System;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.Extensions
{
    /// <summary>
    /// <see cref="DateTime"/> extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTimeUtility DateTimeUtility = new DateTimeUtility();

        #region Conversion Methods

        /// <summary>
        /// Converts this UTC DateTime into a local DateTime based on the time zone provided.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <param name="timeZone">The time zone of the local DateTime to return.</param>
        /// <returns>A local DateTime.</returns>
        public static DateTime ToTimeZone(this DateTime d, DateTimeOffset timeZone)
        {
            return DateTime.SpecifyKind(d.AddHours(timeZone.Offset.Hours), DateTimeKind.Local);
        }

        /// <summary>
        /// Converts this UTC DateTime into a local DateTime based on the time zone ID provided.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <param name="timeZoneId">The system time zone id.</param>
        /// <returns>A local DateTime.</returns>
        public static DateTime ToTimeZone(this DateTime d, string timeZoneId)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTime(d, timeZoneInfo);
        }

        /// <summary>
        /// Converts this local DateTime into a UTC DateTime based on the time zone provided.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <param name="timeZone">The time zone of the local DateTime provided.</param>
        /// <returns>A UTC DateTime.</returns>
        public static DateTime ToUtc(this DateTime d, DateTimeOffset timeZone)
        {
            return DateTime.SpecifyKind(d.AddHours((timeZone.Offset.Hours * -1)), DateTimeKind.Utc);
        }

        #endregion

        #region Semantic Lookup Methods

        /// <summary>
        /// Gets the first date of month.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <returns>The first DateTime on this datetime's month.</returns>
        public static DateTime GetFirstDateOfMonth(this DateTime d)
        {
            return d.AddDays(-d.Day + 1);
        }

        /// <summary>
        /// Gets the last date of month.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <returns>The last DateTime on this datetime's month.</returns>
        public static DateTime GetLastDateOfMonth(this DateTime d)
        {
            return d.AddMonths(1).GetFirstDateOfMonth().AddDays(-1);
        }

        /// <summary>
        /// Returns the date for the next day of the week given after this date.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <param name="targetDay">The day of the week to get the next occurance of.</param>
        /// <returns>The next DateTime that shares this DateTime's day of the week.</returns>
        public static DateTime GetNext(this DateTime d, DayOfWeek targetDay)
        {
            return DateTimeUtility.GetNext(d, targetDay);
        }

        /// <summary>
        /// Returns the date for the last day of the week given before this date.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <param name="targetDay">The day of the week to get the last occurance of.</param>
        /// <returns>The last DateTime that shares this DateTime's day of the week.</returns>
        public static DateTime GetLast(this DateTime d, DayOfWeek targetDay)
        {
            return DateTimeUtility.GetLast(d, targetDay);
        }

        #endregion

        #region String Generation Methods

        /// <summary>
        /// Gets a resourced string for the day of the week of this DateTime.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <returns>The resourced string representation of the day of the week of this DateTime.</returns>
        public static string GetDayOfWeekString(this DateTime d)
        {
            return DateTimeUtility.GetDayOfWeekString(d.DayOfWeek);
        }

        /// <summary>
        /// Generates a string describing how long ago this DateTime was from now.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <param name="timeMode">UTC or Local.</param>
        /// <returns>A string representing time since the date.</returns>
        public static string ToTimeAgoString(this DateTime d, TimeMode timeMode)
        {
            return DateTimeUtility.GetTimeAgoString(d, timeMode);
        }

        #endregion

        #region Operation Methods

        /// <summary>
        /// Determines the difference between this date and provided one.
        /// </summary>
        /// <param name="d">This DateTime.</param>
        /// <param name="secondDate">Date to be subtracted.</param>
        /// <returns>TimeSpan representing the difference between two dates.</returns>
        public static TimeSpan GetDifference(this DateTime d, DateTime secondDate)
        {
            return (d - secondDate);
        }

        #endregion
    }
}