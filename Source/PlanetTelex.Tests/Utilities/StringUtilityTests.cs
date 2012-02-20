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
            const string test1 = "fReddy thudPucker";
            const string test1Result = "Freddy Thudpucker";
            const string test2 = "mary whiner";
            const string test2Result = "Mary Whiner";
            const string test3 = "o two";
            const string test3Result = "O Two";
            const string test4 = "blood on the dance floor";
            const string test4Result = "Blood on the Dance Floor";
            const string test5 = "Why MUTEMATH Is The Best band In the World";
            const string test5Result = "Why MUTEMATH is the Best Band in the World";
            const string test6 = "I HAVE NO IDEA WHY I AM YELLING AND NEITHER DO YOU";
            const string test6Result = "I Have No Idea Why I Am Yelling and Neither Do You";
            const string test7 = "and The Band PlaYed on";
            const string test7Result = "And the Band Played On";

            string processed = _stringUtility.TitleCase(test1);
            Assert.That(processed == test1Result);
            processed = _stringUtility.TitleCase(test2);
            Assert.That(processed == test2Result);
            processed = _stringUtility.TitleCase(test3);
            Assert.That(processed == test3Result);
            processed = _stringUtility.TitleCase(test4);
            Assert.That(processed == test4Result);
            processed = _stringUtility.TitleCase(test5, false);
            Assert.That(processed == test5Result);
            processed = _stringUtility.TitleCase(test6);
            Assert.That(processed == test6Result);
            processed = _stringUtility.TitleCase(test7);
            Assert.That(processed == test7Result);
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