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
using System.Collections.Generic;
using PlanetTelex.Attributes;
using PlanetTelex.Common;
using PlanetTelex.Properties;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods for managing enumerations.
    /// </summary>
    public class EnumUtility
    {
        #region General Enum Methods

        /// <summary>
        /// Converts the given enum type into a dictionary.
        /// </summary>
        /// <param name="enumType">Type of enum that we want to make into a dictionary.</param>
        public virtual Dictionary<int, string> ConvertToDictionary(Type enumType)
        {
            string[] enumNames = Enum.GetNames(enumType);
            Array enumValues = Enum.GetValues(enumType);
            Dictionary<int, string> enumDictionary = new Dictionary<int, string>();
            for (int i = 0; i < enumNames.Length; i++)
                enumDictionary.Add((int)enumValues.GetValue(i), enumNames[i]);

            return enumDictionary;
        }

        /// <summary>
        /// Converts the given enum type into a dictionary with interface displable string values.
        /// </summary>
        /// <param name="enumType">Type of enum that we want to make into a dictionary.</param>
        public virtual Dictionary<int, string> ConvertToDictionaryReadable(Type enumType)
        {
            Dictionary<int, string> enumDictionary = ConvertToDictionary(enumType);
            ArrayList keys = new ArrayList(enumDictionary.Keys);
            foreach (int key in keys)
                enumDictionary[key] = enumDictionary[key].Replace("_", " ");

            return enumDictionary;
        }

        #endregion

        #region Specific Enum Methods

        /// <summary>
        /// Gets the MIME type string of a <see cref="FileExtension"/>.
        /// </summary>
        /// <param name="fileExtension">A file extension.</param>
        /// <returns>A MIME type string.</returns>
        public virtual string GetMimeType(FileExtension fileExtension)
        {
            var field = fileExtension.GetType().GetField(fileExtension.ToString());
            MimeTypeAttribute[] attributes = (MimeTypeAttribute[])field.GetCustomAttributes(typeof(MimeTypeAttribute), false);

            if (attributes == null || attributes.Length == 0)
                throw new ArgumentException(Resources.MimeTypeNotFound, "fileExtension");

            return attributes[0].Value;
        }

        #endregion
    }
}