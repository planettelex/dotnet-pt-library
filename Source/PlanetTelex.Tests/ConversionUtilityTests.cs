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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PlanetTelex.UnitTests
{
    /// <summary>
    /// Unit tests for ConversionUtiltity class
    /// </summary>
    [TestFixture]
    public class ConversionUtilityTests
    {
        /// <summary>
        /// Unit tests for FahrenheitToCelsius conversion
        /// </summary>
        [Test]
        public void FahrenheitToCelsiusTest()
        {
            Assert.That(ConversionUtility.FahrenheitToCelsius(212D) == Int32.Parse("100"));
            Assert.That(ConversionUtility.FahrenheitToCelsius(32D) == Int32.Parse("0"));
        }
        /// <summary>
        /// Unit tests for CelsiusToFahrenheit conversion
        /// </summary>
        [Test]
        public void CelsiusToFahrenheit()
        {
            Assert.That(ConversionUtility.CelsiusToFahrenheit(100D) == Double.Parse("212"));
            Assert.That(ConversionUtility.CelsiusToFahrenheit(0D) == Double.Parse("32"));
        }
    }
}
