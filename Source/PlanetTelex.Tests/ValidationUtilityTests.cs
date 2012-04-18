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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace PlanetTelex.UnitTests
{    
    /// <summary>
    /// Unit tests for ValidationUtility
    /// </summary>
    [TestFixture]
    public class ValidationUtilityTests
    {
        /// <summary>
        /// Test IsValidEmailAddress test
        /// </summary>
        [Test]
        public void IsValidEmailAddressTest()
        {
            string em1 = "123GoodEmail@Planettelex.net";
            Assert.That(PlanetTelex.Web.ValidationUtility.IsValidEmailAddress(em1));
            em1 = "123BadEmail@Planettelex";
            Assert.That(!(PlanetTelex.Web.ValidationUtility.IsValidEmailAddress(em1)));
            em1 = "123BadEmail@Planettelex.";
            Assert.That(!(PlanetTelex.Web.ValidationUtility.IsValidEmailAddress(em1)));
            em1 = "-BadEmail@Planettelex.com";
            Assert.That(!(PlanetTelex.Web.ValidationUtility.IsValidEmailAddress(em1)));
            em1 = "ItsAGoodEmail@Planettelex.net.com.uk";
            Assert.That((PlanetTelex.Web.ValidationUtility.IsValidEmailAddress(em1)));
            em1 = "123ItsABadEmailPlanettelex.net.com.uk";
            Assert.That(!(PlanetTelex.Web.ValidationUtility.IsValidEmailAddress(em1)));
            em1 = "@Planettelex.net";
            Assert.That(!(PlanetTelex.Web.ValidationUtility.IsValidEmailAddress(em1)));
        }
    }
}
