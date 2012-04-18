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
using System.Globalization;
using System.Text.RegularExpressions;
using PlanetTelex.Common;
using PlanetTelex.Properties;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods that assist with mathematical calculations.
    /// </summary>
    public class MathematicsUtility
    {
        #region Conversion Methods

        /// <summary>
        /// Converts degrees into radians.
        /// </summary>
        /// <param name="degrees">Degrees</param>
        /// <returns>Radians</returns>
        public virtual double DegreesToRadians(double degrees)
        {
            return (degrees * Math.PI / 180);
        }

        /// <summary>
        /// Converts radians into degrees.
        /// </summary>
        /// <param name="radians">Radians</param>
        /// <returns>Degrees</returns>
        public virtual double RadiansToDegrees(double radians)
        {
            return (radians * 180 / Math.PI);
        }

        /// <summary>
        /// Converts fractions to doubles.
        /// </summary>
        /// <param name="fraction">A string in the format 'a/b'.</param>
        /// <returns>A double.</returns>
        public virtual double FractionToDouble(string fraction)
        {
            if (!Regex.IsMatch(fraction, RegExPattern.FRACTION))
                throw new ArgumentException(string.Format(Resources.FractionToDoubleArgumentException, fraction), "fraction");
            
            string[] fractionParts = fraction.Split('/');
            Double numerator = Double.Parse(fractionParts[0], CultureInfo.CurrentCulture);
            Double denominator = Double.Parse(fractionParts[1], CultureInfo.CurrentCulture);
            return numerator / denominator;
        }

        /// <summary>
        /// Converts doubles to fractions.
        /// </summary>
        /// <param name="toConvert">The decimal to convert.</param>
        /// <param name="errorMargin">The error margin, a good starting one is 0.00000001.  Adjust as needed, processing time will increase as this precision does.</param>
        /// <returns>A string in the format 'a/b'.</returns>
        public virtual string DoubleToFraction(double toConvert, double errorMargin)
        {
            double currentApproximation = 1;
            int numerator = 1;
            int denominator = 1;

            while (Math.Abs(currentApproximation - toConvert) > errorMargin)
            {
                if (currentApproximation < toConvert)
                    numerator++;
                else
                    numerator = (int)(toConvert * ++denominator);

                currentApproximation = (numerator / (double)denominator);
            }
            return String.Format(CultureInfo.CurrentCulture, "{0}/{1}", numerator, denominator);
        }

        #endregion
    }
}