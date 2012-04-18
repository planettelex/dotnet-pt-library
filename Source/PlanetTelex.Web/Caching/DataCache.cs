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
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace PlanetTelex.Web.Caching
{
    /// <summary>
    /// A wrapper for System.Web.Caching Cache that can be turned on and off caching using the app setting "CacheEnabled".
    /// </summary>
    public class DataCache
    {
        #region Set Cache Methods

        /// <summary>
        /// Places a provided object in the cache, indexed with a specified key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="obj">The object.</param>
        public void SetCache(string cacheKey, object obj)
        {
            if (cacheKey == null || obj == null || !Settings.Current.CacheEnabled)
                return;

            HttpRuntime.Cache.Insert(cacheKey, obj);
        }

        /// <summary>
        /// Places a provided object in the cache along with dependencies, indexed with a specified key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="obj">The in object.</param>
        /// <param name="dependency">The dependency.</param>
        public void SetCache(string cacheKey, object obj, CacheDependency dependency)
        {
            if (cacheKey == null || obj == null || !Settings.Current.CacheEnabled)
                return;

            HttpRuntime.Cache.Insert(cacheKey, obj, dependency);
        }

        /// <summary>
        /// Places a provided object in the cache, indexed with a specified key.
        /// The object will be removed from the cache if not accessed within a provided TimeSpan.
        /// </summary>
        /// <param name="cacheKey">Key to access cached object.</param>
        /// <param name="obj">Object to cache.</param>
        /// <param name="slidingExpiration">The sliding expiration in seconds.</param>
        public void SetCache(string cacheKey, object obj, int slidingExpiration)
        {
            if (cacheKey == null || obj == null || !Settings.Current.CacheEnabled || slidingExpiration <= 0)
                return;

            HttpRuntime.Cache.Insert(cacheKey, obj, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(slidingExpiration));
        }

        /// <summary>
        /// Places a provided object in the cache, indexed with a specified key.
        /// The object will be removed from the cache by a certain DateTime.
        /// </summary>
        /// <param name="cacheKey">Key to access cached object.</param>
        /// <param name="obj">Object to cache.</param>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        public void SetCache(string cacheKey, object obj, DateTime absoluteExpiration)
        {
            if (cacheKey == null || obj == null || !Settings.Current.CacheEnabled)
                return;

            HttpRuntime.Cache.Insert(cacheKey, obj, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        #endregion

        #region Get Cache Methods

        /// <summary>
        /// Gets a value from the cache given key.
        /// </summary>
        /// <param name="cacheKey">The key for the value to retrieve.</param>
        /// <returns>An object from the cache.</returns>
        public object GetCache(string cacheKey)
        {
            if (Settings.Current.CacheEnabled)
                return HttpRuntime.Cache[cacheKey];
            
            return null;
        }

        /// <summary>
        /// Gets a strongly typed value from the cache given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey">The cache key.</param>
        public T GetCacheOfType<T>(string cacheKey)
        {
            if (Settings.Current.CacheEnabled)
                return (T)HttpRuntime.Cache[cacheKey];
            
            return default(T);
        }

        #endregion

        #region Remove/Clear Cache Methods

        ///<summary>
        /// Removes the cache for a given cache key.
        ///</summary>
        ///<param name="cacheKey">A cache key.</param>
        public void RemoveCache(string cacheKey)
        {
            if (cacheKey != null)
                HttpRuntime.Cache.Remove(cacheKey);
        }

        /// <summary>
        /// Removes all items from the cache with keys like the given key.
        /// </summary>
        /// <param name="likeCacheKey">All keys containing this value will be removed from cache.</param>
        public void RemoveAllCache(string likeCacheKey)
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            ArrayList keysToRemove = new ArrayList();
            while (enumerator.MoveNext())
            {
                string key = Convert.ToString(enumerator.Key);
                if (key.IndexOf(likeCacheKey, StringComparison.Ordinal) > -1)
                    keysToRemove.Add(key);
            }
            foreach (string key in keysToRemove)
                HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        public void EmptyCache()
        {
            foreach (DictionaryEntry entry in HttpRuntime.Cache)
                HttpRuntime.Cache.Remove((string)entry.Key);
        }

        #endregion
    }
}