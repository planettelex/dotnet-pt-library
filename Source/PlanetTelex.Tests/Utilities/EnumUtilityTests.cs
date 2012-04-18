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
using NUnit.Framework;
using PlanetTelex.Utilities;

namespace PlanetTelex.UnitTests.Utilities
{
    /// <summary>
    /// Unit tests for EnumUtility
    /// </summary>
    [TestFixture]
    public class EnumUtilityTests
    {
        private EnumUtility _enumUtility;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _enumUtility = new EnumUtility();
        }
        
        /// <summary>
        /// Test Conversion of the DayOfWeek enum type into a dictionary
        /// </summary>
        [Test]
        public void ConvertToDictionaryReadableTest()
        {
            //Test conversion of DayOfWeek
            Dictionary<int, string> dict = _enumUtility.ConvertToDictionaryReadable(typeof(DayOfWeek));
            Assert.That(dict.Count == 7);
            Assert.That(dict[1] == "Monday");
        }
    }
}