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

namespace PlanetTelex.UnitTests
{
    /// <summary>
    /// This class contains properties of various data types and is used as a generic object in unit tests.
    /// </summary>
    [Serializable]
    public class DummyObject
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DummyObject()
        {
        }

        /// <summary>
        /// Alternate construtor sets property values.
        /// </summary>
        /// <param name="stringProperty">A string</param>
        public DummyObject(string stringProperty)
        {
            StringProperty = stringProperty;
        }

        /// <summary>
        /// Alternate construtor sets property values.
        /// </summary>
        /// <param name="stringProperty">A string</param>
        /// <param name="intProperty">An int</param>
        public DummyObject(string stringProperty, int intProperty)
        {
            StringProperty = stringProperty;
            IntProperty = intProperty;
        }

        /// <summary>
        /// Alternate construtor sets property values.
        /// </summary>
        /// <param name="stringProperty">A string</param>
        /// <param name="intProperty">An int</param>
        /// <param name="decimalProperty">A decimal</param>
        public DummyObject(string stringProperty, int intProperty, decimal decimalProperty)
        {
            StringProperty = stringProperty;
            IntProperty = intProperty;
            DecimalProperty = decimalProperty;
        }

        /// <summary>
        /// Alternate construtor sets property values.
        /// </summary>
        /// <param name="stringProperty">A string</param>
        /// <param name="intProperty">An int</param>
        /// <param name="decimalProperty">A decimal</param>
        /// <param name="objectProperty">An object</param>
        public DummyObject(string stringProperty, int intProperty, decimal decimalProperty, object objectProperty)
        {
            StringProperty = stringProperty;
            IntProperty = intProperty;
            DecimalProperty = decimalProperty;
            ObjectProperty = objectProperty;
        }

        #endregion

        /// <summary>
        /// A string property.
        /// </summary>
        public string StringProperty { get; set; }

        /// <summary>
        /// An int property.
        /// </summary>
        public int IntProperty { get; set; }

        /// <summary>
        /// A decimal property.
        /// </summary>
        public decimal DecimalProperty { get; set; }

        /// <summary>
        /// An object property.
        /// </summary>
        public object ObjectProperty { get; set; }
    }

    /// <summary>
    /// This class contains properties of various data types and is used as a generic object in unit tests.
    /// </summary>
    [Serializable]
    public class DummyObjectTwo
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DummyObjectTwo()
        {
        }

        /// <summary>
        /// Alternate construtor sets property values.
        /// </summary>
        /// <param name="stringProperty">A string</param>
        public DummyObjectTwo(string stringProperty)
        {
            StringProperty = stringProperty;
        }

        /// <summary>
        /// Alternate construtor sets property values.
        /// </summary>
        /// <param name="stringProperty">A string</param>
        /// <param name="boolProperty">A bool</param>
        public DummyObjectTwo(string stringProperty, bool boolProperty)
        {
            StringProperty = stringProperty;
            BoolProperty = boolProperty;
        }

        #endregion

        /// <summary>
        /// A string property.
        /// </summary>
        public string StringProperty { get; set; }

        /// <summary>
        /// A bool property.
        /// </summary>
        public bool BoolProperty { get; set; }

    }
}