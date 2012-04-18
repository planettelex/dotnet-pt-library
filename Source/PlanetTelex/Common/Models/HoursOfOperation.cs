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
using System.Collections.Generic;

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// The hours of operation (i.e. open and closing times of a business) for each day of the week.
    /// </summary>
    [Serializable]
    public class HoursOfOperation
    {
        #region Constructor

        /// <summary>
        /// Constructor initializes all 7 days of the week to the default hours of 8am-11pm.
        /// </summary>
        public HoursOfOperation()
        {
            _dailyHours = new List<BusinessHours>(7);
            for (int i = 0; i < 7; i++)
                _dailyHours.Add(new BusinessHours());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a list of the business hours, one for each day of the week.
        /// </summary>
        public List<BusinessHours> DailyHours
        {
            get
            {
                return _dailyHours;
            }
        }
        private readonly List<BusinessHours> _dailyHours;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the open time for a given day.
        /// </summary>
        /// <param name="day">A day of the week.</param>
        /// <param name="time">The time the business opens.</param>
        public void SetOpenTime(DayOfWeek day, DateTime time)
        {
            BusinessHours hours = DailyHours[(int)day];
            hours.SetOpenTime(time);
        }

        /// <summary>
        /// Sets the open time for a given day.
        /// </summary>
        /// <param name="day">A day of the week.</param>
        /// <param name="time">The time the business opens.</param>
        public void SetOpenTime(DayOfWeek day, string time)
        {
            DateTime openTime;
            if(DateTime.TryParse(time, out openTime))
                SetOpenTime(day, openTime);
        }

        /// <summary>
        /// Sets the close time for a given day.
        /// </summary>
        /// <param name="day">A day of the week.</param>
        /// <param name="time">The time the business closes.</param>
        public void SetCloseTime(DayOfWeek day, DateTime time)
        {
            BusinessHours hours = DailyHours[(int)day];
            hours.SetCloseTime(time);
        }

        /// <summary>
        /// Sets the close time for a given day.
        /// </summary>
        /// <param name="day">A day of the week.</param>
        /// <param name="time">The time the business closes.</param>
        public void SetCloseTime(DayOfWeek day, string time)
        {
            DateTime closeTime;
            if (DateTime.TryParse(time, out closeTime))
                SetCloseTime(day, closeTime);
        }

        /// <summary>
        /// Returns all the times between the open and close times, spaced at half hour intervals, for a given day of the week.
        /// </summary>
        /// <param name="day">A day of the week.</param>
        /// <returns>An ArrayList of string values.</returns>
        public List<string> OpenTimes(DayOfWeek day)
        {
            BusinessHours hours = DailyHours[(int)day];
            return hours.OpenTimes();
        }

        /// <summary>
        /// Returns all the times between the open and close times, spaced at half hour intervals, for a given date.
        /// </summary>
        /// <param name="day">A day of the week.</param>
        /// <returns>An ArrayList of string values.</returns>
        public List<string> OpenTimes(DateTime day)
        {
            return OpenTimes(day.DayOfWeek);
        }

        #endregion
    }
}