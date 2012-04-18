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
namespace PlanetTelex.Common
{
    /// <summary>
    /// Common regular expressions.
    /// </summary>
    public static class RegExPattern
    {
        /// <summary>
        /// ^\p{L}+$
        /// </summary>
        public const string ALPHABETIC = @"^\p{L}+$";

        /// <summary>
        /// ^[\p{L}\s]*$
        /// </summary>
        public const string ALPHABETIC_WITH_WHITESPACE = @"^[\p{L}\s]*$";

        /// <summary>
        /// ^[\p{L}\d]+$
        /// </summary>
        public const string ALPHA_NUMERIC = @"^[\p{L}\d]+$";

        /// <summary>
        /// ^[\p{L}\d\s]*$
        /// </summary>
        public const string ALPHA_NUMERIC_WITH_WHITESPACE = @"^[\p{L}\d\s]*$";

        /// <summary>
        /// [\s-]{0,1}[0-9]+/[1-9]+
        /// </summary>
        public const string FRACTION = @"[\s-]{0,1}[0-9]+/[1-9]+";

        /// <summary>
        /// ^\{?[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}\}?$
        /// </summary>
        public const string GUID = @"^\{?[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}\}?$";

        /// <summary>
        /// &lt;[^&gt;]*&gt;
        /// </summary>
        public const string HTML_TAG = "<[^>]*>";

        /// <summary>
        /// ^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$
        /// </summary>
        public const string IP_ADDRESS = @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";

        /// <summary>
        /// ^(ISBN){0,1}[\s-]{0,1}[0-9]{1}[\s-]{0,1}[0-9]{6}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[xX0-9]{1}$
        /// </summary>
        public const string ISBN = @"^(ISBN){0,1}[\s-]{0,1}[0-9]{1}[\s-]{0,1}[0-9]{6}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[xX0-9]{1}$";

        /// <summary>
        /// /^[0-9]*/
        /// </summary>
        public const string LEADING_NUMBERS = "^[0-9]*";

        /// <summary>
        /// [^0-9a-zA-Z\\s] - this does include whitespaces as an alphanumeric character
        /// </summary>
        public const string NOT_ALPHA_NUMERIC = "[^0-9a-zA-Z\\s]";

        /// <summary>
        /// [^0-9a-zA-Z\\s-\xC0-\xFF] - This does include whitespaces and hyphen characters. It also matches special extension ascii to include characters such as ê, ç, ñ.
        /// This regex expression was created specifically for a URL Rewriter.
        /// </summary>
        public const string NOT_ALPHA_NUMERIC_FOR_URL = "[^0-9a-zA-Z\\s-\xC0-\xFF]";

        /// <summary>
        /// [^0-9a-zA-Z] - does not include whitespaces as alphanumeric characters
        /// </summary>
        public const string NOT_ALPHA_NUMERIC_EXCLUDING_WHITE_SPACE = "[^0-9a-zA-Z]";

        /// <summary>
        /// ^[\d]+$
        /// </summary>
        public const string NUMERIC = @"^[\d]+$";

        /// <summary>
        /// ^[\d\s]*$
        /// </summary>
        public const string NUMERIC_WITH_WHITESPACE = @"^[\d\s]*$";

        /// <summary>
        /// ^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]+$
        /// </summary>
        public const string PHONE_NUMBER = @"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]+$";

        /// <summary>
        /// ^[A-Za-z]{2}[0-9]{6}[A-Za-z]{1}$
        /// </summary>
        public const string UK_NATIONAL_INSURANCE_NUMBER = @"^[A-Za-z]{2}[0-9]{6}[A-Za-z]{1}$";

        /// <summary>
        /// ^[a-zA-Z]{1,2}[0-9][0-9A-Za-z]{0,1} {0,1}[0-9][A-Za-z]{2}$
        /// </summary>
        public const string UK_POSTAL_CODE = @"^[a-zA-Z]{1,2}[0-9][0-9A-Za-z]{0,1} {0,1}[0-9][A-Za-z]{2}$";

        /// <summary>
        /// ^(\d{5}-\d{4}|\d{5})$|^([A-Z]\d[A-Z] \d[A-Z]\d)$
        /// </summary>
        public const string US_ZIP_CODE = @"^(\d{5}-\d{4}|\d{5})$|^([A-Z]\d[A-Z] \d[A-Z]\d)$";

        /// <summary>
        /// ^[\s]*$
        /// </summary>
        public const string WHITESPACE = @"^[\s]*$";

        /// <summary>
        /// &lt;\\?xml[^&gt;]*\\?&gt;
        /// </summary>
        public const string XML_DOCTYPE_NODE = "<\\?xml[^>]*\\?>";

        /// <summary>
        /// xmlns=\"[^&gt;\"]*\"
        /// </summary>
        public const string XML_NAMESPACE_ATTRIBUTE = "xmlns=\"[^>\"]*\"";

        /// <summary>
        /// A URI regular expression pattern.
        /// </summary>
        /// <example>
        /// ^((https?|ftp)://)?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?(([0-9]{1,3}\.){3}[0-9]{1,3}|[0-9a-z_!~*'()-]+\.)*([0-9a-z][0-9a-z-]{0,61})?[0-9a-z](\.[a-z]{2,6})?)(:[0-9]{1,5})?((/?)|(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$
        /// </example>
        public const string URI = "^((https?|ftp)://)"
                                    + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
                                    + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
                                    + "|" // allows either IP or domain
                                    + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
                                    + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level domain
                                    + @"(\.[a-z]{2,6})?)" // first level domain- .com or .museum is optional
                                    + "(:[0-9]{1,5})?" // port number- :80
                                    + "((/?)|" // a slash isn't required if there is no file name
                                    + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";

        /// <summary>
        ///^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$
        ///It verifies that: 
        /// * Only letters, numbers and email acceptable symbols (+, _, -, .) are allowed - 
        /// * No two different symbols may follow each other - 
        /// * Cannot begin with a symbol - 
        /// * Ending domain must be at least 2 letters - 
        /// * Supports subdomains - 
        /// * TLD must be between 2 and 6 letters (Ex: .ca, .museum) - 
        /// * Only (-) and (.) symbols are allowed in domain, but not consecutively. 
        ///Matches:
        ///g_s+gav@com.com | gav@gav.com | jim@jim.c.dc.ca
        ///Non-Matches: 
        ///gs_.gs@com.com | gav@gav.c | jim@--c.ca
        /// </summary>
        public const string EMAIL_ADDRESS = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";
        /*Source:http://regexlib.com/DisplayPatterns.aspx?cattabindex=0&categoryId=1*/
    }
}