using System;
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
