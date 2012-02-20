using System;
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
