#region Copyright Ben Cheetham 2010
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in 
// any form or by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner. 
// 
// Filename: StringExtension.cs
// 
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuildfordBoroughCouncil.Linq
{
    public static partial class StringExtensionMethods
    {
        public static string FormatEnum(this string str)
        {
            return str.Replace("___", " - ").Replace("__", "-").Replace("_", " ");
        }

        /// <summary>
        /// Returns a value indicating whether a value in the specified System.String[] object occurs within this string.
        /// <para>Source: http://extensionmethod.net/Details.aspx?ID=243</para>
        /// </summary>
        public static bool ContainsAny(this string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length.Equals(0))
            {
                foreach (string value in values)
                {
                    if (str.Contains(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether a string exists is in the System.String[] object.
        /// </summary>
        public static bool Contains(this string[] str, string value)
        {
            if (!string.IsNullOrEmpty(value) || str.Length.Equals(0))
            {
                foreach (string s in str)
                {
                    if (s.Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool Contains(this string str, string value, StringComparison comparer)
        {
            return str.IndexOf(value, comparer) >= 0;
        }

        /// <summary>
        /// Returns a if the string has a value.
        /// </summary>
        public static bool HasValue(this string str)
        {
            return !String.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Returns a value in title case.
        /// 
        /// Note if the string passed in is ALL CAPS, then it will be returned as ALL CAPS.
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Returns a value with an initial capital.
        /// 
        /// Note if the string passed in is ALL CAPS, then it will be returned as ALL CAPS.
        /// </summary>
        public static string WithCapitalFirst(this string str)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                return str.First().ToString().ToUpper() + String.Join("", str.Skip(1));
            }
            return str;
        }

        /// <summary>
        /// Returns a value with the line breaks replaced with HTML BRs.
        /// </summary>
        public static string nl2br(this string str)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                return str.Replace(System.Environment.NewLine, "<br />");
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Validate a telephone number
        /// </summary>
        /// <param name="telephone">Telephone number to be validated</param>
        /// <returns>Returns validated telephone number.</returns>
        public static string ToValidatedTelephoneNumber(this string telephone)
        {
            if (!String.IsNullOrWhiteSpace(telephone))
            {
                // Remove all non numeric values
                telephone = Regex.Replace(telephone, @"[\D]", String.Empty);

                // Replace 0 at start with 44 (country code)
                if (telephone.StartsWith("0"))
                {
                    telephone = Regex.Replace(telephone, @"\A[0]", "44");
                }

                // Check length to see if it is the right length

                if ((telephone.Length < 10) || (telephone.Length > 12))
                {
                    throw new FormatException("Telephone number is not a valid length.");
                }

                return telephone;
            }
            else
            {
                return telephone;
            }
        }

        /// <summary>
        /// Remove GUILDFORD\ if it exists
        /// </summary>
        /// <param name="username">Telephone number to be validated</param>
        /// <returns>Returns validated telephone number.</returns>
        public static string RemoveDownLevelDomain(this string username)
        {
            if (!String.IsNullOrWhiteSpace(username))
            {
                return username.Replace(@"GUILDFORD\", String.Empty).ToLower();
            }
            else
            {
                return username;
            }
        }

        /// <summary>
        /// Add GUILDFORD\ if it exists
        /// </summary>
        /// <param name="username">Telephone number to be validated</param>
        /// <returns>Returns validated telephone number.</returns>
        public static string AddDownLevelDomain(this string username)
        {
            if (!String.IsNullOrWhiteSpace(username))
            {
                if (!username.StartsWith(@"GUILDFORD\"))
                {
                    username = @"GUILDFORD\" + username;
                }
                return username;
            }
            else
            {
                return username;
            }
        }

        /// <summary>
        /// Returns a Guildford.gov.uk email address
        /// </summary>
        /// <param name="username">Username to be converted</param>
        /// <returns>Returns an email address.</returns>
        public static string ToEmailAddress(this string username)
        {
            if (!String.IsNullOrWhiteSpace(username) && !username.Contains("@"))
            {
                return username.RemoveDownLevelDomain() + "@guildford.gov.uk";
            }
            else
            {
                return username;
            }
        }

        /// <summary>
        /// Returns an SHA-1 hash of the string.
        /// </summary>
        public static string CalculateSha1Hash(this string str)
        {
            var buffer = new System.Text.ASCIIEncoding().GetBytes(str);
            var Hasher = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            return BitConverter.ToString(Hasher.ComputeHash(buffer)).Replace("-", String.Empty);
        }

        /// <summary>
        /// Parse text and replace anything that looks like an HTTP web link with an HTML link
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ParseLinks(this string str)
        {
            Regex urlregex = new Regex(@"(http:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return urlregex.Replace(str, "<a href=\"$1\" target=\"_blank\">$1</a>");
        }
        
        /// <summary>
        /// Escape special characters for exporting to CSV.
        /// <para>Source: http://stackoverflow.com/questions/4685705/good-csv-writer-for-c</para>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CsvEscape(this string str)
        {
            const string QUOTE = "\"";
            const string ESCAPED_QUOTE = "\"\"";
            //char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };

            if (str.Contains(QUOTE))
                str = str.Replace(QUOTE, ESCAPED_QUOTE);

            //if (str.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
            //    str = QUOTE + str + QUOTE;

            return str;
        }

        /// <summary>
        /// Unescape special characters when importing a CSV.
        /// <para>Source: http://stackoverflow.com/questions/4685705/good-csv-writer-for-c</para>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CsvUnescape(this string str)
        {
            const string QUOTE = "\"";
            const string ESCAPED_QUOTE = "\"\"";

            if (str.StartsWith(QUOTE) && str.EndsWith(QUOTE))
            {
                str = str.Substring(1, str.Length - 2);

                if (str.Contains(ESCAPED_QUOTE))
                    str = str.Replace(ESCAPED_QUOTE, QUOTE);
            }

            return str;
        }

        private enum CsvSplitState
        {
            AtBeginningOfToken,
            InNonQuotedToken,
            InQuotedToken,
            ExpectingComma,
            InEscapedCharacter
        };

        /// <summary>
        /// Returns a System.String[] object contained within a CSV delimited string.
        /// <para>Source: http://blogs.msdn.com/ericwhite/archive/2008/09/30/linq-to-text-and-linq-to-csv.aspx</para>
        /// </summary>
        public static string[] CsvSplit(this String source)
        {
            List<string> splitString = new List<string>();
            List<int> slashesToRemove = null;
            CsvSplitState state = CsvSplitState.AtBeginningOfToken;
            char[] sourceCharArray = source.ToCharArray();
            int tokenStart = 0;
            int len = sourceCharArray.Length;
            for (int i = 0; i < len; ++i)
            {
                switch (state)
                {
                    case CsvSplitState.AtBeginningOfToken:
                        if (sourceCharArray[i] == '"')
                        {
                            state = CsvSplitState.InQuotedToken;
                            slashesToRemove = new List<int>();
                            continue;
                        }
                        if (sourceCharArray[i] == ',')
                        {
                            splitString.Add("");
                            tokenStart = i + 1;
                            continue;
                        }
                        state = CsvSplitState.InNonQuotedToken;
                        continue;
                    case CsvSplitState.InNonQuotedToken:
                        if (sourceCharArray[i] == ',')
                        {
                            splitString.Add(source.Substring(tokenStart, i - tokenStart));
                            state = CsvSplitState.AtBeginningOfToken;
                            tokenStart = i + 1;
                        }
                        continue;
                    case CsvSplitState.InQuotedToken:
                        if (sourceCharArray[i] == '"')
                        {
                            state = CsvSplitState.ExpectingComma;
                            continue;
                        }
                        if (sourceCharArray[i] == '\\')
                        {
                            state = CsvSplitState.InEscapedCharacter;
                            slashesToRemove.Add(i - tokenStart);
                            continue;
                        }
                        continue;
                    case CsvSplitState.ExpectingComma:
                        if (sourceCharArray[i] != ',')
                            throw new CsvParseException("Expecting comma");
                        string stringWithSlashes = source.Substring(tokenStart, i - tokenStart);
                        foreach (int item in slashesToRemove.Reverse<int>())
                            stringWithSlashes = stringWithSlashes.Remove(item, 1);
                        splitString.Add(stringWithSlashes.Substring(1, stringWithSlashes.Length - 2));
                        state = CsvSplitState.AtBeginningOfToken;
                        tokenStart = i + 1;
                        continue;
                    case CsvSplitState.InEscapedCharacter:
                        state = CsvSplitState.InQuotedToken;
                        continue;
                }
            }
            switch (state)
            {
                case CsvSplitState.AtBeginningOfToken:
                    splitString.Add("");
                    return splitString.ToArray();
                case CsvSplitState.InNonQuotedToken:
                    splitString.Add(source.Substring(tokenStart, source.Length - tokenStart));
                    return splitString.ToArray();
                case CsvSplitState.InQuotedToken:
                    throw new CsvParseException("Expecting ending quote");
                case CsvSplitState.ExpectingComma:
                    string stringWithSlashes =
                        source.Substring(tokenStart, source.Length - tokenStart);
                    foreach (int item in slashesToRemove.Reverse<int>())
                        stringWithSlashes = stringWithSlashes.Remove(item, 1);
                    splitString.Add(stringWithSlashes.Substring(1, stringWithSlashes.Length - 2));
                    return splitString.ToArray();
                case CsvSplitState.InEscapedCharacter:
                    throw new CsvParseException("Expecting escaped character");
            }
            throw new CsvParseException("Unexpected error");
        }
    }

    /// <summary>
    /// Custom extension for CSV parser.
    /// </summary>
    public class CsvParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the FudgeApps.Linq.CsvParseException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CsvParseException(string message) : base(message)
        { }
    }
}