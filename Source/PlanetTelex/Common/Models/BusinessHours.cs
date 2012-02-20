using System;
using System.Collections.Generic;
using System.Globalization;

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// The hours a business is open in a single day.
    /// </summary>
    [Serializable]
    public class BusinessHours
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets whether this location is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the business is closed all day; otherwise, <c>false</c>.
        /// </value>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Gets whether this location is open all day, or 24 hours, which occurs when the open time and close time are equal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the business is open 24 hours this day; otherwise, <c>false</c>.
        /// </value>
        public bool OpenAllDay
        {
            get
            {
                return (_openTime == _closeTime);
            }
        }

        /// <summary>
        /// Gets or sets the opening minute of the day starting from 0 = 12:00 AM.  Defaults to 8:00 AM.
        /// </summary>
        /// <value>
        /// The opening time for the day, as expressed as the number of minutes since midnight.
        /// </value>
        public int OpenTime
        {
            get
            {
                return _openTime;
            }
            set
            {
                _openTime = value;
            }
        }
        private int _openTime = 480; // Default is 8:00 AM

        /// <summary>
        /// Gets or sets the closing minute of the day starting from 0 = 12:00 AM.  Defaults to 11:00 PM.
        /// </summary>
        /// <value>
        /// The closing time for the day, as expressed as the number of minutes since midnight.
        /// </value>
        public int CloseTime
        {
            get
            {
                return _closeTime;
            }
            set
            {
                _closeTime = value;
                // If the close time is less than the open time, add a day to it.
                if (_closeTime < _openTime)
                    _closeTime += 1440;

            }
        }
        private int _closeTime = 1380; // Default 11:00 PM

        /// <summary>
        /// Gets the short time string for the open time.
        /// </summary>
        public string DisplayOpenTime
        {
            get 
            {
                DateTime opentime = new DateTime();
                return opentime.AddMinutes(_openTime).ToShortTimeString();
            }
        }

        /// <summary>
        /// Gets the short time string for the close time.
        /// </summary>
        public string DisplayCloseTime
        {
            get
            {
                DateTime closetime = new DateTime();
                return closetime.AddMinutes(_closeTime).ToShortTimeString();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the OpenTime property from a given DateTime.
        /// </summary>
        /// <param name="openTime">The time the business opens.</param>
        public void SetOpenTime(DateTime openTime)
        {
            OpenTime = (openTime.Hour * 60) + openTime.Minute;
        }

        /// <summary>
        /// Sets the OpenTime property from a given DateTime string.
        /// </summary>
        /// <param name="openTime">The time the business opens.</param>
        public void SetOpenTime(string openTime)
        {
            SetOpenTime(DateTime.Parse(openTime, CultureInfo.CurrentUICulture));
        }

        /// <summary>
        /// Sets the CloseTime property from a given DateTime.
        /// </summary>
        /// <param name="closeTime">The time the business closes.</param>
        public void SetCloseTime(DateTime closeTime)
        {
            CloseTime = (closeTime.Hour * 60) + closeTime.Minute;
        }

        /// <summary>
        /// Sets the CloseTime property from a given DateTime string.
        /// </summary>
        /// <param name="closeTime">The time the business closes.</param>
        public void SetCloseTime(string closeTime)
        {
            SetCloseTime(DateTime.Parse(closeTime, CultureInfo.CurrentUICulture));
        }

        /// <summary>
        /// Gets all the times for the next two days, spaced at half hour intervals.
        /// </summary>
        /// <returns>A list of half hour incremented times.</returns>
        public static List<string> AllTimes()
        {
            if (_allTimes == null)
            {
                _allTimes = new List<string>(96);
                DateTime time = DateTime.Parse("12:00 AM");

                for (int i = 0; i < 96; i++) // Two days worth of times to prevent an index out of range for places that close after midnight.
                {
                    _allTimes.Add(time.ToShortTimeString());
                    time = time.AddMinutes(30);
                }
            }
            return _allTimes;
        }
        private static List<string> _allTimes;

        /// <summary>
        /// Gets all the times between this instance's open and close times, spaced at half hour intervals.
        /// </summary>
        /// <returns>A list of half hour incremented times.</returns>
        public List<string> OpenTimes()
        {
            List<string> returnVal = new List<string>();
            if (OpenAllDay)
            {
                for (int i = 0; i < 48; i++) // Only one day of time.
                    returnVal.Add(AllTimes()[i]);
            }
            else
            {
                int startIndex = OpenTime / 30;
                int endIndex = CloseTime / 30;
               
                for (int i = startIndex; i <= endIndex; i++)
                    returnVal.Add(AllTimes()[i]);                
            }
            return returnVal;
        }

        #endregion
    }
}