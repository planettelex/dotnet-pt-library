using System;
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