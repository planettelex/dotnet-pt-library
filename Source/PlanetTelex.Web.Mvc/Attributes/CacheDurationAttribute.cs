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
﻿using System;
using System.Web;
using System.Web.Mvc;

namespace PlanetTelex.Web.Mvc.Attributes
{
    ///<summary>
    /// An action filter attribute that writes caching HTTP headers with the specified duration.
    ///</summary>
    public class CacheDurationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the cache duration in seconds. The default is 10 seconds.
        /// </summary>
        /// <value>The cache duration in seconds.</value>
        public int Duration { get; set; }

        /// <summary>
        /// Sets the cache duration in minutes.
        /// </summary>
        /// <value>
        /// The duration in minutes.
        /// </value>
        public int DurationInMinutes
        {
            set { Duration = value * 60; }
        }

        /// <summary>
        /// Sets the cache duration in hours.
        /// </summary>
        /// <value>
        /// The duration in minutes.
        /// </value>
        public int DurationInHours
        {
            set { Duration = value * 60 * 60; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheDurationAttribute"/> class.
        /// </summary>
        public CacheDurationAttribute()
        {
            Duration = 10;
        }

        /// <summary>
        /// Called after an action is executed.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Duration <= 0) return;

            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            TimeSpan cacheDuration = TimeSpan.FromSeconds(Duration);

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(DateTime.Now.Add(cacheDuration));
            cache.SetMaxAge(cacheDuration);
            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }
    }
}
