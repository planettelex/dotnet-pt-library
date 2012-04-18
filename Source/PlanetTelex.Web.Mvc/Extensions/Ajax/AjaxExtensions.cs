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
﻿using System.Web.Mvc;
using PlanetTelex.Serialization;

namespace PlanetTelex.Web.Mvc.Extensions.Ajax
{
    /// <summary>
    /// Extension methods to the <see cref="AjaxHelper"/> class.
    /// </summary>
    public static class AjaxExtensions
    {
        private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer();
        
        /// <summary>
        /// Serializes an instance as JSON.
        /// </summary>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        /// <param name="helper">This AJAX helper.</param>
        /// <param name="instance">The object to serialize.</param>
        /// <returns>A JSON string.</returns>
        public static string JsonSerialize<T>(this AjaxHelper helper, T instance) where T : class
        {
            return helper.JavaScriptStringEncode(Serializer.Serialize(instance));
        }
    }
}
