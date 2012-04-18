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
using NUnit.Framework;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.UnitTests.Utilities
{
    /// <summary>
    /// Unit tests for StringUtility
    /// </summary>
    [TestFixture]
    public class StringUtilityTests
    {
        private StringUtility _stringUtility;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _stringUtility = new StringUtility();
        }

        /// <summary>
        /// Bytes the array conversion test.
        /// </summary>
        [Test]
        public void ByteArrayConversionTest()
        {
            const string toTest = "Kicking squealing gucci little piggy!";
            byte[] converted = _stringUtility.ConvertToByteArray(toTest);
            string utf8 = _stringUtility.ConvertFromByteArray(converted, TextEncoding.Utf8);
            string ascii = _stringUtility.ConvertFromByteArray(converted, TextEncoding.Ascii);
            Assert.That(System.String.CompareOrdinal(toTest, utf8) == 0);
            Assert.That(System.String.CompareOrdinal(toTest, ascii) == 0);
        }

        /// <summary>
        /// Shortens the test.
        /// </summary>
        [Test]
        public void ShortenTest()
        {
            const string longString = "'A man, a plan, a canal, Panama' is the longest known palendrome in the english language, discounting spaces and punctuation.";
            const string shortString = "'A man, a plan, a canal, Panama'";
            const string alternateString = "'A man, a plan, a canal, a uh..'";
            const string alternateStringEnd = " a uh..'";
            Assert.That(_stringUtility.Shorten(longString, 32) == shortString);
            Assert.That(_stringUtility.Shorten(longString, 32, alternateStringEnd) == alternateString);
        }

        /// <summary>
        /// Strips the returns test.
        /// </summary>
        [Test]
        public void StripReturnsTest()
        {
            const string withReturns = "Hi there\n\rthis string has several\rnew line\ncharacters.\n\r";
            const string withoutReturns = "Hi there this string has several new line characters.";
            string processed = _stringUtility.StripReturns(withReturns);
            Assert.That(processed == withoutReturns);
        }

        /// <summary>
        /// Strips the non alpha numeric test.
        /// </summary>
        [Test]
        public void StripNonAlphaNumericTest()
        {
            const string with = "\"In 1492 Columbus sailed the ocean blue\" is the #1 school-time rhyme! Why?";
            const string without = "In 1492 Columbus sailed the ocean blue is the 1 schooltime rhyme Why";
            string processed = _stringUtility.StripNonAlphaNumeric(with);
            Assert.That(processed == without);
        }

        /// <summary>
        /// Removes the leading numbers test.
        /// </summary>
        [Test]
        public void RemoveLeadingNumbersTest()
        {
            const string with = "2468 Who do we appreciate? Not 10.";
            const string without = "Who do we appreciate? Not 10.";
            string processed = _stringUtility.RemoveLeadingNumbers(with);
            Assert.That(processed == without);
        }

        /// <summary>
        /// Determines whether [is numeric test].
        /// </summary>
        [Test]
        public void IsNumericTest()
        {
            Assert.That(_stringUtility.IsNumeric("1234"));
            Assert.That(_stringUtility.IsNumeric("Not numeric") == false);
            Assert.That(_stringUtility.IsNumeric("3.14159"));
            Assert.That(_stringUtility.IsNumeric("11l") == false);
        }

        /// <summary>
        /// Replaces the tokens with object values test.
        /// </summary>
        [Test]
        public void ReplaceTokensWithObjectValuesTest()
        {
            DummyObject dummy = new DummyObject("But listen to the color of your dreams", 7, 1000);
            const string textWithTokens = "Revolver was the Beatles [DummyObject.IntProperty]th album. John wanted the song Tomorrow Never Knows to sound like [DummyObject.DecimalProperty] monks chanting to lyrics like '[DummyObject.StringProperty]'.";
            const string textWithoutTokens = "Revolver was the Beatles 7th album. John wanted the song Tomorrow Never Knows to sound like 1000 monks chanting to lyrics like 'But listen to the color of your dreams'.";
            string processed = _stringUtility.ReplaceTokensWithObjectValues(textWithTokens, dummy);
            Assert.That(processed == textWithoutTokens);
        }
        
        /// <summary>
        /// Title Case test.
        /// </summary>
        [Test]
        public void TitleCaseTest()
        {
            const string test0 = "the title";
            const string test0Result = "The Title";
            const string test1 = "return of the jedi";
            const string test1Result = "Return of the Jedi";
            const string test2 = "LORD OF THE RINGS";
            const string test2Result = "Lord of the Rings";
            const string test3 = "portugal. the man";
            const string test3Result = "Portugal. The Man";
            const string test4 = "blood on the dance floor";
            const string test4Result = "Blood on the Dance Floor";
            const string test5 = "mutemath roCks tHe goTHic IN Denver";
            const string test5Result = "MUTEMATH Rocks the Gothic in Denver";
            const string test6 = "Why everybody needs an iphone and ipad";
            const string test6Result = "Why Everybody Needs an iPhone and iPad";
            const string test7 = "I want you (she's so heavy)";
            const string test7Result = "I Want You (She's So Heavy)";
            const string test8 = "(I don't know) and i don't care";
            const string test8Result = "(I Don't Know) And I Don't Care";
            const string test9 = "the zombie! the werewolf! the ghost!!!";
            const string test9Result = "The Zombie! The Werewolf! The Ghost!!!";
            const string test10 = "...and the band played on!";
            const string test10Result = "...And the Band Played On!";
            const string test11 = "hop on pop";
            const string test11Result = "Hop on Pop";
            const string test12 = "has anybody seen my iPhone???";
            const string test12Result = "Has Anybody Seen My iPhone???";
            const string test13 = "PINION/terrible lie";
            const string test13Result = "Pinion/Terrible Lie";
            const string test14 = "bOrn In thE u.s.A.";
            const string test14Result = "Born in the U.S.A.";
            const string test15 = "i got you (at the end of the century)";
            const string test15Result = "I Got You (At the End of the Century)";
            string[] casedWords = new[] {"MUTEMATH"};

            Assert.That(_stringUtility.TitleCase(test0, null) == test0Result);
            Assert.That(_stringUtility.TitleCase(test1, null) == test1Result);
            Assert.That(_stringUtility.TitleCase(test2, casedWords) == test2Result);
            Assert.That(_stringUtility.TitleCase(test3, casedWords) == test3Result);
            Assert.That(_stringUtility.TitleCase(test4, casedWords) == test4Result);
            Assert.That(_stringUtility.TitleCase(test5, casedWords) == test5Result);
            Assert.That(_stringUtility.TitleCase(test6, casedWords) == test6Result);
            Assert.That(_stringUtility.TitleCase(test7, casedWords) == test7Result);
            Assert.That(_stringUtility.TitleCase(test8, casedWords) == test8Result);
            Assert.That(_stringUtility.TitleCase(test9, casedWords) == test9Result);
            Assert.That(_stringUtility.TitleCase(test10, casedWords) == test10Result);
            Assert.That(_stringUtility.TitleCase(test11, casedWords) == test11Result);
            Assert.That(_stringUtility.TitleCase(test12, casedWords) == test12Result);
            Assert.That(_stringUtility.TitleCase(test13, casedWords) == test13Result);
            Assert.That(_stringUtility.TitleCase(test14, casedWords) == test14Result);
            Assert.That(_stringUtility.TitleCase(test15, casedWords) == test15Result);
        }
              
        /// <summary>
        /// Remove Whitespace test.
        /// </summary>
        [Test]
        public void RemoveWhitespaceTest()
        {
            const string test1 = "  Remove"+ "\t" + " Space    Test";
            const string test1Result = "RemoveSpaceTest";
            Assert.That(_stringUtility.RemoveWhitespace(test1) == test1Result);

        }
        
        /// <summary>
        /// Format phone number test.
        /// </summary>
        [Test]
        public void FormatPhoneNumberTest()
        {
            const string ph1 = @"303-555-1212";
            const string ph2 = @"(303)555-1212";
            const string ph3 = @"3035551212";

            const string phn = @"303-555-1212";

            Assert.That(_stringUtility.FormatPhoneNumber(ph1) == phn);
            Assert.That(_stringUtility.FormatPhoneNumber(ph2) == phn);
            Assert.That(_stringUtility.FormatPhoneNumber(ph3) == phn);
        }
    }
}