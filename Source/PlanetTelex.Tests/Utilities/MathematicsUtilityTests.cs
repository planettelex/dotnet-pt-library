using System;
using NUnit.Framework;
using PlanetTelex.Utilities;

namespace PlanetTelex.UnitTests.Utilities
{
    /// <summary>
    /// Unit tests for MathematicsUtility
    /// </summary>
    [TestFixture]
    public class MathematicsUtilityTests
    {
        private MathematicsUtility _mathematicsUtility;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _mathematicsUtility = new MathematicsUtility();
        }

        /// <summary>
        /// Test the Conversion of degrees into radians.
        /// </summary>
        [Test]
        public void DegreesToRadiansTest()
        {
            const double test1 = 180;
            const double test2 = 270;
            double dblResult = _mathematicsUtility.DegreesToRadians(test1);
            Assert.That((Math.Abs(dblResult - (test1 * Math.PI / 180)) < double.Epsilon));
            dblResult = _mathematicsUtility.DegreesToRadians(test2);
            Assert.That((Math.Abs(dblResult - (test2 * Math.PI / 180)) < double.Epsilon));
        }

        /// <summary>
        /// Test the Conversion of radians into degrees.
        /// </summary>
        [Test]
        public void RadiansToDegreesTest()
        {
            const double test1 = 10;
            const double test2 = 70;
            double dblResult = _mathematicsUtility.RadiansToDegrees(test1);
            Assert.That((Math.Abs(dblResult - (test1 * 180 / Math.PI)) < double.Epsilon));
            dblResult = _mathematicsUtility.RadiansToDegrees(test2);
            Assert.That((Math.Abs(dblResult - (test2 * 180 / Math.PI)) < double.Epsilon));
        }

        /// <summary>
        /// Test Conversion of fractions to doubles.
        /// </summary>
        [Test]
        public void FractionToDoubleTest()
        {
            const string strTest1 = "1/2";
            const double res1 = 0.5D;
            const string strTest2 = "1/4";
            const double res2 = 0.25D;

            double dblResult = _mathematicsUtility.FractionToDouble(strTest1);
            Assert.That(Math.Abs(res1 - dblResult) < double.Epsilon);
            dblResult = _mathematicsUtility.FractionToDouble(strTest2);
            Assert.That(Math.Abs(res2 - dblResult) < double.Epsilon);
        }
        /// <summary>
        /// Test Conversion doubles to fractions using error margin of 0.00000001.
        /// </summary>
        [Test]
        public void DoubleToFractionTest()
        {
            const double dblHalf = 0.5D;
            const string strResHalf = "1/2";
            const double dblQtr = 0.25D;
            const string strResQtr = "1/4";

            string strResult = _mathematicsUtility.DoubleToFraction(dblHalf, 0.00000001D);
            Assert.That(strResHalf == strResult);
            strResult = _mathematicsUtility.DoubleToFraction(dblQtr, 0.00000001D);
            Assert.That(strResQtr == strResult);
        }
    }
}
