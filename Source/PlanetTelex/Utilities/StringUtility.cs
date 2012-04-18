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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using PlanetTelex.Common;
using PlanetTelex.Properties;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods that assist in string manipulation.
    /// </summary>
    public class StringUtility
    {
        #region Conversion Methods

        /// <summary>
        /// Converts this string to a byte array. It is faster than System.Text.UnicodeEncoding().GetBytes() and won't convert or drop characters like an Encoding (ASCII or Unicode) would do.
        /// </summary>
        /// <param name="toConvert">The string to convert.</param>
        /// <returns>A new byte array.</returns>
        public virtual byte[] ConvertToByteArray(string toConvert)
        {
            if (toConvert == null) return null;
            byte[] returnVal = new byte[toConvert.Length];
            for (int i = 0; i < toConvert.Length; i++)
                returnVal[i] = (byte)toConvert[i];

            return returnVal;
        }

        /// <summary>
        /// Converts a byte array into a string of the provided encoding, supports UTF8 and ASCII.
        /// </summary>
        /// <param name="toConvert">The byte array to convert into a string.</param>
        /// <param name="encoding">UTF8 or ASCII encoding.</param>
        /// <returns>A new string.</returns>
        public virtual string ConvertFromByteArray(byte[] toConvert, TextEncoding encoding)
        {
            switch (encoding)
            {
                case TextEncoding.Ascii:
                    return new ASCIIEncoding().GetString(toConvert);
                default:
                    return new UTF8Encoding().GetString(toConvert);
            }
        }

        /// <summary>
        /// Converts a string of comma separated integer values into a flags enum of the provided type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="toConvert">The string to convert into a flags enum.</param>
        /// <param name="defaultValue">The value the enum should have.</param>
        /// <returns>A flags enum of the specified type and values.</returns>
        public virtual TEnum ToFlagsEnum<TEnum>(string toConvert, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            Type t = typeof(TEnum);
            if (!t.IsEnum)
                throw new ArgumentException(Resources.TypeNotEnum);

            TEnum returnVal = defaultValue;
            try
            {
                // ReSharper disable RedundantAssignment
                if (!string.IsNullOrEmpty(toConvert))
                {
                    int enumValue = toConvert.Split(',').Aggregate(0, (acc, v) => acc |= Convert.ToInt32(v), acc => acc);
                    returnVal = (TEnum)Enum.ToObject(t, enumValue);
                }
                // ReSharper restore RedundantAssignment
            }
            catch (FormatException)
            {
                // Default gets returned.
            }
            return returnVal;
        }

        /// <summary>
        /// Converts this string into a nullable enum equivalent of the provided type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="toConvert">The string to convert into a nullable enum.</param>
        /// <param name="defaultValue">The value the enum should have.</param>
        /// <param name="ignoreCase">If set to <c>true</c> ignore string casing.</param>
        /// <returns>A nullable enum of the specified type and default value.</returns>
        public virtual TEnum? ToNullableEnum<TEnum>(string toConvert, TEnum? defaultValue, bool ignoreCase) where TEnum : struct, IConvertible
        {
            Type t = typeof(TEnum);
            if (!t.IsEnum)
                throw new ArgumentException(Resources.TypeNotEnum);

            TEnum? returnVal = defaultValue;
            if (!string.IsNullOrEmpty(toConvert))
            {
                toConvert = toConvert.Trim();
                if (toConvert.Length > 0)
                {
                    try
                    {
                        returnVal = (TEnum)Enum.Parse(t, toConvert, ignoreCase);
                    }
                    catch (ArgumentException)
                    {
                        // Default gets returned.
                    }
                }
            }
            return returnVal;
        }

        #endregion

        #region Removal Methods

        /// <summary>
        /// Shortens a given string to a specified length. If the given string is shorter than the desired length, it will be returned unchanged.
        /// </summary>
        /// <param name="toShorten">The string to shorten.</param>
        /// <param name="desiredLength">The length the returning string should not exceed.</param>
        /// <param name="suffix">A string that can be appended to the end of the given string after it is shortened. The resulting string will still not exceed the desired length, more of the original string will be shortened.</param>
        /// <returns>A new string shortened to the specified length.</returns>
        public virtual string Shorten(string toShorten, int desiredLength, string suffix)
        {
            if (toShorten == null)
                return string.Empty;
            if (suffix == null)
                suffix = string.Empty;

            if (suffix.Length > desiredLength)
                throw new ArgumentOutOfRangeException("suffix", Resources.ShortenArgumentOutOfRangeException);

            if (toShorten.Length > desiredLength)
                return toShorten.Substring(0, desiredLength - suffix.Length) + suffix;
            
            return toShorten;
        }

        /// <summary>
        /// Shortens a given string to a specified length. If the given string is shorter than the desired length, it will be returned unchanged.
        /// </summary>
        /// <param name="toShorten">The string to shorten.</param>
        /// <param name="desiredLength">The length the returning string should not exceed.</param>
        /// <returns>A new string shortened to the specified length.</returns>
        public virtual string Shorten(string toShorten, int desiredLength)
        {
            return Shorten(toShorten, desiredLength, null);
        }

        /// <summary>
        /// Removes all newline and return characters from a given string.
        /// </summary>
        /// <param name="toStrip">The string to strip of newline and return characters.</param>
        /// <returns>A new string stripped of all newline and return characters.</returns>
        public virtual string StripReturns(string toStrip)
        {
            if (string.IsNullOrEmpty(toStrip))
                return string.Empty;

            toStrip = toStrip.Replace(Environment.NewLine, " ");
            toStrip = toStrip.Replace("\n\r", " ");
            toStrip = toStrip.Replace("\n", " ");
            toStrip = toStrip.Replace("\r", " ");
            return toStrip.Trim();
        }

        /// <summary>
        /// Removes all characters that are not letters or numbers from a given string.
        /// </summary>
        /// <param name="toStrip">The string to strip of all characters that are not letters or numbers.</param>
        /// <returns>A new string stripped of all characters that are not letters or numbers.</returns>
        public virtual string StripNonAlphaNumeric(string toStrip)
        {
            if (string.IsNullOrEmpty(toStrip))
                return string.Empty;
            
            toStrip = toStrip.Replace("\"", string.Empty);
            return Regex.Replace(toStrip, RegExPattern.NOT_ALPHA_NUMERIC, string.Empty);
        }

        /// <summary>
        /// Removes all HTML markup tags from a given string, leaving only the text in between them.
        /// </summary>
        /// <param name="toStrip">The string to strip of HTML.</param>
        /// <param name="replaceWithSpace">If set to <c>true</c> tags will be replaced with a space instead of the default empty string.</param>
        /// <returns>A new string stripped of HTML tags.</returns>
        public virtual string StripHtmlTags(string toStrip, bool replaceWithSpace)
        {
            if (string.IsNullOrEmpty(toStrip))
                return string.Empty;

            string replacementString = replaceWithSpace ? " " : string.Empty;
            toStrip = toStrip.Replace("…", "...");
            const RegexOptions options = RegexOptions.IgnoreCase;
            return Regex.Replace(toStrip, RegExPattern.HTML_TAG, replacementString, options);
        }

        /// <summary>
        /// Removes accents (more broadly called diacritics) from a given string.
        /// </summary>
        /// <param name="toStrip">The string to strip of diacritics.</param>
        /// <returns>A new string with diacritics removed.</returns>
        public virtual string RemoveDiacritics(string toStrip)
        {
            // Split up characters like é into a marker to for the accent and the real letter e.
            string resultValue = toStrip.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in resultValue)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                // Filter out the markers and leave the spaces and letters.
                if (category != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// Removes all numbers at the beginning of a given string, leaving everything after the first non-numeric character.
        /// </summary>
        /// <param name="toStrip">The string from which to remove leading numbers.</param>
        /// <returns>A new string with its leading numbers removed.</returns>
        public virtual string RemoveLeadingNumbers(string toStrip)
        {
            return Regex.Replace(toStrip, RegExPattern.LEADING_NUMBERS, string.Empty).Trim();
        }

        /// <summary>
        ///  Removes all whitespace from a given string.
        /// </summary>
        /// <param name="toRemoveFrom">The string to remove the whitespace from.</param>
        /// <returns>A new string with its whitespace removed.</returns>
        public virtual string RemoveWhitespace(string toRemoveFrom)
        {
            if (toRemoveFrom == null)
                return null;

            StringBuilder returnVal = new StringBuilder();
            foreach (char character in toRemoveFrom)
                if (character != ' ' && character != '\t')
                    returnVal.Append(character);

            return returnVal.ToString();
        }

        #endregion

        #region Alteration Methods

        #region Title Case Private Methods and Properties

        private enum WordPosition { First, Middle, Last }
        private readonly char[] _weakWordSeparators = new[]{' ', ','};
        private readonly char[] _strongWordSeparators = new[] { '.', '?', '!', '(', ')', '{', '}', '[', ']', '<', '>', '/', '\\', '&' };
        
        /// <summary>
        /// All designated word separators.
        /// </summary>
        private IEnumerable<char> Separators
        {
            get
            {
                return _weakWordSeparators.Concat(_strongWordSeparators);
            }
        }

        /// <summary>
        /// A list of words to case specifically as specified by the assembly resource TitleCaseToCase.
        /// </summary>
        private static IEnumerable<string> ToCase
        {
            get
            {
                if (_toCase == null)
                    _toCase = Resources.TitleCaseToCase.Split(',', ' ');
                
                return _toCase;
            }
        }
        private static string[] _toCase;

        /// <summary>
        /// A list of words to force lowercase when they aren't the first or last word in a title as specified by the assembly resource TitleCaseToLower.
        /// </summary>
        private static IEnumerable<string> ToLower
        {
            get
            {
                if (_toLower == null)
                    _toLower = Resources.TitleCaseToLower.Split(',', ' ');

                return _toLower;
            }
        }
        private static string[] _toLower;

        /// <summary>
        /// Cases a single word.
        /// </summary>
        /// <param name="wordToCase">The word to case.</param>
        /// <param name="wordPosition">The word position: First, Middle or Last, important because it determines capitalization rules.</param>
        /// <param name="preceedingSeparator">The preceeding separator, important because it determines capitalization rules.</param>
        /// <param name="specificallyCasedWords">A list of words to case specifically, like "MySpace".</param>
        /// <returns>A title cased word.</returns>
        private string CaseWord(string wordToCase, WordPosition wordPosition, char preceedingSeparator, string[] specificallyCasedWords)
        {
            if (ToCase.Contains(wordToCase, StringComparer.OrdinalIgnoreCase))
                return ToCase.FirstOrDefault(s => s.Equals(wordToCase, StringComparison.OrdinalIgnoreCase));

            if (specificallyCasedWords != null && specificallyCasedWords.Contains(wordToCase, StringComparer.OrdinalIgnoreCase))
                return specificallyCasedWords.FirstOrDefault(s => s.Equals(wordToCase, StringComparison.OrdinalIgnoreCase));

            if (ToLower.Contains(wordToCase, StringComparer.OrdinalIgnoreCase) && wordPosition == WordPosition.Middle && _weakWordSeparators.Contains(preceedingSeparator))
                return wordToCase.ToLower();

            return UppercaseFirstLetter(wordToCase.ToLower());
        }

        #endregion

        /// <summary>
        /// Creates a copy of a string that is titled cased. This follows the algorithm that both the first and last words of the string are capitalized,
        /// as are most other words in the string, with the exception of a list of excluded words (and, of, etc.) This list is a comma delimited assembly resource.
        /// </summary>
        /// <param name="toTitleCase">The string to title case.</param>
        /// <returns>A new string that is title cased.</returns>
        public virtual string TitleCase(string toTitleCase)
        {
            return TitleCase(toTitleCase, null);
        }

        /// <summary>
        /// Creates a copy of a string that is titled cased. This means that most words in the given string will be all lowercase except for their first letter,
        /// which will be a capital. Expections to this rule include a list of words to lowercase when not the first or last word (and, of, etc.).
        /// This list is an assembly resource. There is another resource that specifies words to be specifically cased (like iPhone). The items in this resource
        /// are supplimented by a provided list of other words in which the casing doesn't follow the general rule.
        /// </summary>
        /// <param name="toTitleCase">The string to title case.</param>
        /// <param name="specificallyCasedWords">A list of words to case specifically, like "MySpace".</param>
        /// <returns>A new string that is title cased.</returns>
        public virtual string TitleCase(string toTitleCase, string[] specificallyCasedWords)
        {
            if (toTitleCase == null)
                return null;

            StringBuilder stringBuilder = new StringBuilder();
            string currentWord = string.Empty;
            string lastWord = string.Empty;
            char lastSeparator = '\0';
            int wordCount = 0;

            foreach (char c in toTitleCase)
            {
                if (Separators.Contains(c)) // The current character is a separator.
                {
                    if (currentWord.Length > 0)
                    {
                        WordPosition position = wordCount == 0 ? WordPosition.First : WordPosition.Middle;
                        stringBuilder.Append(CaseWord(currentWord, position, lastSeparator, specificallyCasedWords));
                        lastWord = currentWord;
                        currentWord = string.Empty;
                        lastSeparator = '\0';
                        wordCount++;
                    }
                    stringBuilder.Append(c);
                    // Set the last separator to the current character, unless it is a space AND the lastSeparator is a strong separator.
                    // This is done so CaseWord will work correctly after strong and weak separators happen in succession, like period-space.
                    if (!(_strongWordSeparators.Contains(lastSeparator) && char.IsWhiteSpace(c)))
                        lastSeparator = c;
                }
                else // The current character is not a separator.
                    currentWord += c;
            }

            if (currentWord.Length > 0) // Add the last word since it won't be written unless the last character in the title is a separator.
                stringBuilder.Append(CaseWord(currentWord, WordPosition.Last, lastSeparator, specificallyCasedWords));
            else // The last character was a separator. If the last word was in the ToLower list, it is incorrect at this point- this code corrects it.
            {
                string title = stringBuilder.ToString();
                int lastWordIndex = title.LastIndexOf(lastWord, StringComparison.OrdinalIgnoreCase);
                string toInsert = CaseWord(lastWord, WordPosition.Last, '\0', specificallyCasedWords);
                return ReplaceAt(title, lastWordIndex, lastWord.Length, toInsert);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Creates a copy of a string in which the first letter is uppercase.
        /// </summary>
        /// <param name="toUppercase">The string to uppercase.</param>
        /// <returns>A new string that is uppercased.</returns>
        public virtual string UppercaseFirstLetter(string toUppercase)
        {
            if (string.IsNullOrEmpty(toUppercase))
                return string.Empty;

            return char.ToUpper(toUppercase[0]) + toUppercase.Substring(1);
        }

        /// <summary>
        /// Standardizes a US phone number string into the format: xxx-xxx-xxxx.  Returns an empty string if the phone number cannot be formatted.
        /// </summary>
        /// <param name="phoneNumber">The phone number string to format.</param>
        /// <returns>A new string that is formatted: xxx-xxx-xxxx.</returns>
        public virtual string FormatPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                phoneNumber = StripNonAlphaNumeric(phoneNumber);
                if (phoneNumber.Length == 10)
                    return phoneNumber.Substring(0, 3) + "-" + phoneNumber.Substring(3, 3) + "-" + phoneNumber.Substring(6, 4);

                return string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Evaluates a given string for tokens in the format: [ClassName.PropertyName]. These can also be nested: [ClassName.PropertyName.SubPropertyName]
        /// All tokens formatted this way are replaced with the corresponding values in the object provided.
        /// </summary>
        /// <param name="tokenizedText">The text that contains tokens to be replaced.</param>
        /// <param name="valueContainer">The object that contains the values to replace the tokens with.</param>
        /// <returns>A new string with tokens replaced by values in the provided object.</returns>
        public virtual string ReplaceTokensWithObjectValues(string tokenizedText, object valueContainer)
        {
            if (tokenizedText == null) return string.Empty;
            if (valueContainer == null) return tokenizedText;
            string replacementString = null;

            // Get all tokens minus the open character.
            string[] tokens = tokenizedText.Split(TOKEN_START.ToCharArray());

            for (int i = 0; i < tokens.Length; i++)
            {
                // Remove the closing character and anything trailing.
                if (tokens[i].IndexOf(TOKEN_END, StringComparison.Ordinal) > 0)
                {
                    tokens[i] = tokens[i].Substring(0, tokens[i].IndexOf(TOKEN_END, StringComparison.Ordinal));
                    replacementString = GetPropertyValue(tokens[i], valueContainer);
                }

                // Do the replacment if a value was returned.
                if (replacementString != null)
                    tokenizedText = tokenizedText.Replace(TOKEN_START + tokens[i] + TOKEN_END, replacementString);
            }
            tokenizedText = Regex.Replace(tokenizedText, "\\[.+\\]", string.Empty);
            return tokenizedText;
        }

        #region Replace Tokens Helpers

        private const string TOKEN_START = "[";
        private const string TOKEN_END = "]";

        /// <summary>
        /// Gets the property value for a given token.
        /// </summary>
        /// <param name="token">The token. A string representing a class structure in the form of 'ClassName.PropertyName.SubPropertyName'</param>
        /// <param name="valueContainer">The object containing the values.</param>
        /// <returns>The value of the right-most property in the token string or null if it's not recognized.</returns>
        private string GetPropertyValue(string token, object valueContainer)
        {
            // Check arguments for null values.
            if (string.IsNullOrEmpty(token) || valueContainer == null)
                return null;

            // Validate that the first part of the string is indeed the root class of the object.
            int firstDotIndex = token.IndexOf(".", StringComparison.Ordinal);
            Type valueContainerType = valueContainer.GetType();

            string tokenClassName = (firstDotIndex > 0) ? token.Substring(0, firstDotIndex) : token;
            string objectClassName = valueContainerType.Name;

            // Check that the arguments passed in are valid.
            if (String.Compare(tokenClassName, objectClassName, StringComparison.OrdinalIgnoreCase) != 0)
                return null;

            // Get the Property name from the token.
            int secondDotIndex = token.IndexOf(".", firstDotIndex + 1, StringComparison.Ordinal);
            int propertyLength = ((secondDotIndex < 0) ? token.Length : secondDotIndex) - (firstDotIndex + 1);
            string tokenPropertyName = token.Substring(firstDotIndex + 1, propertyLength);

            // Pull the property from the value container.
            PropertyInfo property = valueContainerType.GetProperty(tokenPropertyName);
            if (property == null)
                return null;

            // Check for value type or string type.
            if (property.PropertyType.IsValueType || property.PropertyType == typeof(String)) // Found a returnable value.
            {
                if (valueContainer is decimal) // We must format this properly.
                {
                    decimal decimalValue = Convert.ToDecimal(property.GetValue(valueContainer, null));
                    return decimalValue.ToString("d");
                }
                return Convert.ToString(property.GetValue(valueContainer, null));
            }

            // Rewrite the token string and recurse until a value type is found or a null is returned.
            if (secondDotIndex < 0)
                return null;

            string newToken = property.PropertyType.Name + token.Substring(secondDotIndex);
            // Recursive call for nested property strings.
            return GetPropertyValue(newToken, property.GetValue(valueContainer, null));
        }

        #endregion

        /// <summary>
        /// Replaces all special Microsoft Word characters in a string with standard characters.
        /// </summary>
        /// <param name="msWordText">A string on which to replace Microsoft Word characters.</param>
        /// <returns>A new string with Microsoft Word special characters replaced by standard characters.</returns>
        public virtual string ReplaceMicrosoftWordCharacters(string msWordText)
        {
            if (string.IsNullOrEmpty(msWordText))
                return msWordText;

            string returnVal = msWordText;

            // Single quotes and apostrophe.
            returnVal = Regex.Replace(returnVal, "[\u2018|\u2019|\u201A]", "'");
            // Double quotes.
            returnVal = Regex.Replace(returnVal, "[\u201C|\u201D|\u201E]", "\"");
            // Ellipsis.
            returnVal = Regex.Replace(returnVal, "\u2026", "...");
            // Dashes.
            returnVal = Regex.Replace(returnVal, "[\u2013|\u2014]", "-");
            // Circumflex.
            returnVal = Regex.Replace(returnVal, "\u02C6", "^");
            // Open angle bracket.
            returnVal = Regex.Replace(returnVal, "\u2039", "<");
            // Close angle bracket.
            returnVal = Regex.Replace(returnVal, "\u203A", ">");
            // Spaces
            returnVal = Regex.Replace(returnVal, "[\u02DC|\u00A0]", " ");

            return returnVal;
        }

        /// <summary>
        /// Removes a chunk of a string and replaces it with the specified string.
        /// </summary>
        /// <param name="toReplaceAt">A string to replace a chunck of with another string.</param>
        /// <param name="removeStartIndex">Starting index to remove.</param>
        /// <param name="removeCount">The number of characters past the start index to remove.</param>
        /// <param name="toInsert">The string to insert.</param>
        /// <returns>A new string with text removed and new text inserted.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="toReplaceAt" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="removeStartIndex" /> is outsides the bounds of this string.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="removeCount" /> plus <paramref name="removeStartIndex" /> exceeds the size of the string.</exception>
        public string ReplaceAt(string toReplaceAt, int removeStartIndex, int removeCount, string toInsert)
        {
            if (toReplaceAt == null) throw new ArgumentNullException("toReplaceAt");
            if (removeStartIndex >= toReplaceAt.Length) throw new ArgumentOutOfRangeException("removeStartIndex");
            if (removeStartIndex + removeCount >= toReplaceAt.Length) throw new ArgumentOutOfRangeException("removeCount", Resources.IndexPlusCountExceedsSize);

            string removed = toReplaceAt.Remove(removeStartIndex, removeCount);
            return removed.Insert(removeStartIndex, toInsert);
        }

        #endregion

        #region Testing Methods

        /// <summary>
        /// Determines whether the given string is numeric.
        /// </summary>
        /// <param name="toTest">The string to test.</param>
        /// <returns><c>true</c> if the specified string is numeric; otherwise, <c>false</c>.</returns>
        public virtual bool IsNumeric(string toTest)
        {
            if (string.IsNullOrEmpty(toTest))
                return false;
            
            toTest = toTest.TrimStart("-".ToCharArray());
            if (toTest.Trim().Length == 0)
                return false;

            bool hasDecimal = false;
            foreach (char testChar in toTest)
            {
                if (testChar == '.') // Check for decimal.
                {
                    if (hasDecimal) // Second decimal encountered, so not a number.
                        return false;

                    hasDecimal = true;
                    continue;
                }

                if (!char.IsNumber(testChar))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the given string represents a boolean.
        /// </summary>
        /// <param name="toTest">The string to test.</param>
        /// <returns><c>true</c> if the specified string can be converted to a boolean; otherwise, <c>false</c>.</returns>
        public virtual bool IsBoolean(string toTest)
        {
            return (toTest.ToLower() == "true" || toTest.ToLower() == "false");
        }

        #endregion

        #region Encryption Methods

        /// <summary>
        /// Generates an MD5 hash a string.
        /// </summary>
        /// <param name="toHash">The string to hash.</param>
        /// <returns>A new hashed string created by the MD5 algorithm.</returns>
        public virtual string MD5(string toHash)
        {
            if (_sMd5 == null) //creating only when needed
                _sMd5 = new MD5CryptoServiceProvider();

            Byte[] newdata = Encoding.Default.GetBytes(toHash);
            Byte[] encrypted = _sMd5.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", string.Empty).ToLower();
        }
        static MD5CryptoServiceProvider _sMd5;

        #endregion
    }
}