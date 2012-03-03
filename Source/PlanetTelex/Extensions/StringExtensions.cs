using System;
using System.Globalization;
using System.Text.RegularExpressions;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.Extensions
{
    /// <summary>
    /// <see cref="String"/> extension methods.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly StringUtility StringUtility = new StringUtility();

        #region Conversion Methods

        /// <summary>
        /// Converts this string to a byte array. It is faster than System.Text.UnicodeEncoding().GetBytes() and won't convert or drop characters like an Encoding (ASCII or Unicode) would do.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A byte array.</returns>
        public static byte[] ToByteArray(this string s)
        {
            return StringUtility.ConvertToByteArray(s);
        }

        /// <summary>
        /// Converts this string into a 32 bit integer.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>An integer.</returns>
        public static int ToInt(this string s)
        {
            // ReSharper disable PossibleInvalidOperationException
            return (int)s.ToNullableInt(default(int));
            // ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// Converts this string into a 32 bit integer.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>An integer.</returns>
        public static int ToInt(this string s, int? defaultValue)
        {
            // ReSharper disable PossibleInvalidOperationException
            return (int)s.ToNullableInt(defaultValue);
            // ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// Converts this string into a nullable 32 bit integer.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>An integer or null if it can't be converted.</returns>
        public static int? ToNullableInt(this string s)
        {
            return s.ToNullableInt(null);
        }

        /// <summary>
        /// Converts this string into a nullable 32 bit integer.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="defaultValue">A default value to use if conversion fails.</param>
        /// <returns>An integer or the default value if it can't be converted.</returns>
        public static int? ToNullableInt(this string s, int? defaultValue)
        {
            int? returnVal = defaultValue;
            int result;
            if (s.IsNotNullOrEmpty() && int.TryParse(s, out result))
                returnVal = result;

            return returnVal;
        }

        /// <summary>
        /// Converts this string into a 64 bit integer, or long.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A long.</returns>
        public static long ToLong(this string s)
        {
            // ReSharper disable PossibleInvalidOperationException
            return (long)s.ToNullableLong(default(long));
            // ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// Converts this string into a nullable 64 bit integer, or long.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A long or null if it can't be converted.</returns>
        public static long? ToNullableLong(this string s)
        {
            return s.ToNullableLong(null);
        }

        /// <summary>
        /// Converts this string into a nullable 64 bit integer, or long.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="defaultValue">A default value to use if conversion fails.</param>
        /// <returns>A long or the default value if it can't be converted.</returns>
        public static long? ToNullableLong(this string s, long? defaultValue)
        {
            long? returnVal = defaultValue;
            long result;
            if (s.IsNotNullOrEmpty() && long.TryParse(s, out result))
                returnVal = result;

            return returnVal;
        }

        /// <summary>
        /// Converts this string of comma separated integer values into a flags enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="s">This string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A flags enum of the specified type and values.</returns>
        public static TEnum ToFlagsEnum<TEnum>(this string s, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            return StringUtility.ToFlagsEnum(s, defaultValue);
        }

        /// <summary>
        /// Converts this string of comma separated integer values into a flags enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="s">This string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A flags enum of the specified type and values.</returns>
        public static TEnum ToFlagsEnum<TEnum>(this string s, int defaultValue) where TEnum : struct, IConvertible
        {
            return StringUtility.ToFlagsEnum(s, (TEnum)Enum.ToObject(typeof(TEnum), defaultValue));
        }

        /// <summary>
        /// Converts this string of comma separated integer values into a flags enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="s">This string.</param>
        /// <returns>A flags enum of the specified type and values.</returns>
        public static TEnum ToFlagsEnum<TEnum>(this string s) where TEnum : struct, IConvertible
        {
            return StringUtility.ToFlagsEnum(s, (TEnum)Enum.ToObject(typeof(TEnum), 0));
        }

        /// <summary>
        /// Converts this string of comma separated integer values into a nullable enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="s">This string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="ignoreCase">If set to <c>true</c> ignore string casing.</param>
        /// <returns>A new nullable enum of the given type and value.</returns>
        public static TEnum? ToNullableEnum<TEnum>(this string s, TEnum? defaultValue, bool ignoreCase) where TEnum : struct, IConvertible
        {
            return StringUtility.ToNullableEnum(s, defaultValue, ignoreCase);
        }

        /// <summary>
        /// Converts this string of comma separated integer values into a nullable enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="s">This string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A new nullable enum of the given type and value.</returns>
        public static TEnum? ToNullableEnum<TEnum>(this string s, TEnum? defaultValue) where TEnum : struct, IConvertible
        {
            return StringUtility.ToNullableEnum(s, defaultValue, false);
        }

        /// <summary>
        /// Converts this string of comma separated integer values into a nullable enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="s">This string.</param>
        /// <param name="ignoreCase">If set to <c>true</c> ignore string casing.</param>
        /// <returns>A new nullable enum of the given type.</returns>
        public static TEnum? ToNullableEnum<TEnum>(this string s, bool ignoreCase) where TEnum : struct, IConvertible
        {
            return StringUtility.ToNullableEnum<TEnum>(s, null, ignoreCase);
        }

        /// <summary>
        /// Converts this URL string to a Uri.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new <see cref="Uri"/>.</returns>
        public static Uri ToUri(this string s)
        {
            return new Uri(s);
        }

        #endregion

        #region Removal Methods

        /// <summary>
        /// Shortens this string to a specified length. If this string is shorter than the desired length, it will be returned unchanged.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="desiredLength">The length the returning string should not exceed.</param>
        /// <param name="suffix">A string that can be appended to the end of the given string after it is shortened. The resulting string will still not exceed the desired length, more of the original string will be shortened.</param>
        /// <returns>A new string shortened to the specified length.</returns>
        public static string Shorten(this string s, int desiredLength, string suffix)
        {
            return StringUtility.Shorten(s, desiredLength, suffix);
        }

        /// <summary>
        /// Shortens this string to a specified length. If this string is shorter than the desired length, it will be returned unchanged.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="desiredLength">The length the returning string should not exceed.</param>
        /// <returns>A new string shortened to the specified length.</returns>
        public static string Shorten(this string s, int desiredLength)
        {
            return StringUtility.Shorten(s, desiredLength, null);
        }

        /// <summary>
        /// Removes all newline and return characters from this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string stripped of all newline and return characters.</returns>
        public static string StripReturns(this string s)
        {
            return StringUtility.StripReturns(s);
        }

        /// <summary>
        /// Removes all characters that are not letters or numbers from this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string stripped of all characters that are not letters or numbers.</returns>
        public static string StripNonAlphaNumeric(this string s)
        {
            return StringUtility.StripNonAlphaNumeric(s);
        }

        /// <summary>
        /// Removes all HTML markup tags from this string, leaving only the text in between them.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="replaceWithSpace">if set to <c>true</c> [replace with space].</param>
        /// <returns>A new string stripped of HTML tags.</returns>
        public static string StripHtmlTags(this string s, bool replaceWithSpace)
        {
            return StringUtility.StripHtmlTags(s, replaceWithSpace);
        }

        /// <summary>
        /// Removes accents (more broadly called diacritics) from this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string with diacritics removed.</returns>
        public static string RemoveDiacritics(this string s)
        {
            return StringUtility.RemoveDiacritics(s);
        }

        /// <summary>
        /// Removes all numbers at the beginning of this string, leaving everything after the first non-numeric character.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string with its leading numbers removed.</returns>
        public static string RemoveLeadingNumbers(this string s)
        {
            return StringUtility.RemoveLeadingNumbers(s);
        }

        /// <summary>
        /// Removes all whitespace from this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string with its whitespace removed.</returns>
        public static string RemoveWhitespace(this string s)
        {
            return StringUtility.RemoveWhitespace(s);
        }

        #endregion

        #region Alteration Methods

        /// <summary>
        /// Creates a copy of this string that is titled cased. This means that most words in the given string will be all lowercase except for their first letter,
        /// which will be a capital. Expections to this rule include a list of words to lowercase when not the first or last word (and, of, etc.).
        /// This list is an assembly resource. There is another resource that specifies words to be specifically cased (like iPhone). The items in this resource
        /// are supplimented by a provided list of other words in which the casing doesn't follow the general rule.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string that is title cased.</returns>
        public static string TitleCase(this string s)
        {
            return StringUtility.TitleCase(s);
        }

        /// <summary>
        /// Creates a copy of this string that is titled cased. This means that most words in the given string will be all lowercase except for their first letter,
        /// which will be a capital. Expections to this rule include a list of words to lowercase when not the first or last word (and, of, etc.).
        /// This list is an assembly resource. There is another resource that specifies words to be specifically cased (like iPhone). The items in this resource
        /// are supplimented by a provided list of other words in which the casing doesn't follow the general rule.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="specificallyCasedWords">A list of words to case specifically, like "MySpace".</param>
        /// <returns>A new string that is title cased.</returns>
        public static string TitleCase(this string s, string[] specificallyCasedWords)
        {
            return StringUtility.TitleCase(s);
        }

        /// <summary>
        /// Creates a copy of this string in which the first letter is uppercase.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string that is uppercased.</returns>
        public static string UppercaseFirstLetter(this string s)
        {
            return StringUtility.UppercaseFirstLetter(s);
        }

        /// <summary>
        /// Standardizes this US phone number string into the format: xxx-xxx-xxxx.  Returns an empty string if the phone number cannot be formatted.
        /// </summary>
        /// <param name="s">This string.</param>
        public static string FormatPhoneNumber(this string s)
        {
            return StringUtility.FormatPhoneNumber(s);
        }

        /// <summary>
        /// Replaces each format item in this string with the string representation of a corresponding object in a specified array using the current runtime culture.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="args">An array of objects to format. </param>
        /// <returns>A new, formatted string.</returns>
        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, s, args);
        }

        /// <summary>
        /// Evaluates this string for tokens in the format: [ClassName.PropertyName]. These can also be nested: [ClassName.PropertyName.SubPropertyName]
        /// All tokens formatted this way are replaced with the corresponding values in the object provided.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="valueContainer">The object that contains the values to replace the tokens</param>
        /// <returns>A new string with tokens replaced by values in the provided object.</returns>
        public static string ReplaceTokensWithObjectValues(this string s, object valueContainer)
        {
            return StringUtility.ReplaceTokensWithObjectValues(s, valueContainer);
        }

        /// <summary>
        /// Replaces all special Microsoft Word characters in this string with standard characters.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string with Microsoft Word special characters replaced by standard characters.</returns>
        public static string ReplaceMicrosoftWordCharacters(this string s)
        {
            return StringUtility.ReplaceMicrosoftWordCharacters(s);
        }

        /// <summary>
        /// Reverses the characters of this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string with the characters reversed.</returns>
        public static string Reverse(this string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        /// <summary>
        /// Returns a new string in which all matches of a specified regular expression in this string are replaced with another specified string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
        public static string RegexReplace(this string s, string pattern, string replacement)
        {
            if (s == null) throw new ArgumentNullException("s");
            return Regex.Replace(s, pattern, replacement);
        }

        /// <summary>
        /// Returns a new string in which all matches of a specified regular expression in this string are replaced with another specified string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <param name="options">Regex Options (ex: ignorecase) </param>
        /// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
        public static string RegexReplace(this string s, string pattern, string replacement, RegexOptions options)
        {
            if (s == null) throw new ArgumentNullException("s");
            return Regex.Replace(s, pattern, replacement, options);
        }

        /// <summary>
        /// Removes a chunk of this string and replaces it with the specified string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="removeStartIndex">Starting index to remove.</param>
        /// <param name="removeCount">The number of characters past the start index to remove.</param>
        /// <param name="toInsert">The string to insert.</param>
        /// <returns>A new string with text removed and new text inserted.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="removeStartIndex" /> is outsides the bounds of this string.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="removeCount" /> plus <paramref name="removeStartIndex" /> exceeds the size of the string.</exception>
        public static string ReplaceAt(this string s, int removeStartIndex, int removeCount, string toInsert)
        {
            return StringUtility.ReplaceAt(s, removeStartIndex, removeCount, toInsert);
        }

        #endregion

        #region Testing Methods

        /// <summary>
        /// Determines whether a given string contains HTML markup.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>
        ///   <c>true</c> if this string contains HTML; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsHtml(this string s)
        {
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
            return Regex.IsMatch(s, RegExPattern.HTML_TAG, options);
        }

        /// <summary>
        /// Determines whether this string is null or an empty string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>
        /// 	<c>true</c> if this string is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Determines whether this string is NOT null or NOT an empty string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>
        /// 	<c>true</c> if this string is NOT null or NOT empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNullOrEmpty(this string s)
        {
            return !s.IsNullOrEmpty();
        }

        /// <summary>
        /// Determines whether this string is numeric.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>
        ///   <c>true</c> if this string is numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumeric(this string s)
        {
            return StringUtility.IsNumeric(s);
        }

        /// <summary>
        /// Determines whether this string represents a boolean.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>
        ///   <c>true</c> if this string can be converted to a boolean; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBoolean(this string s)
        {
            return StringUtility.IsBoolean(s);
        }

        /// <summary>
        /// Determines whether this string is a valid email address.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>
        ///   <c>true</c> if the string is a valid email address; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
        public static bool IsValidEmailAddress(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");
            return new Regex(RegExPattern.EMAIL_ADDRESS).IsMatch(s);
        }

        /// <summary>
        /// Determines whether this string is a valid email URI.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>
        ///   <c>true</c> if the string is a valid URI; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
        public static bool IsValidUri(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");
            return new Regex(RegExPattern.URI).IsMatch(s);
        }

        /// <summary>
        /// Gets a Regex match within this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>A new string with matched characters.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s" /> is <c>null</c>.</exception>
        public static string RegexMatch(this string s, string pattern)
        {
            if (s == null) throw new ArgumentNullException("s");
            return Regex.Match(s, pattern).ToString();
        }

        #endregion

        #region Encryption Methods

        /// <summary>
        /// Generates an MD5 hash of this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new hashed string created by the MD5 algorithm.</returns>
        public static string MD5(this string s)
        {
            return StringUtility.MD5(s);
        }

        #endregion
    }
}