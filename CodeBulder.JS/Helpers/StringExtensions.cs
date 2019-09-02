using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeBuilder.JS.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Faster replace all implementation
        /// 
        /// Author: Philip Schoeman
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="replaceValues">To replace</param>
        /// <returns></returns>
        public static string ReplaceAll(this string sourceString, IEnumerable<KeyValuePair<string, string>> replaceValues, bool replaceAll = true)
        {
            foreach (var toReplace in replaceValues)
            {
                sourceString = replaceAll ? sourceString.Replace(toReplace.Key, toReplace.Value) : new Regex(Regex.Escape(toReplace.Key)).Replace(sourceString, toReplace.Value, 1);
            }
            return sourceString;
        }

        /// <summary>
        /// Generates the key value combination used by ReplaceAll
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="replaceValue"></param>
        /// <returns></returns>
        public static KeyValuePair<string, string> KeyValueMap(this string sourceString, string replaceValue)
        {
            return new KeyValuePair<string, string>(sourceString, replaceValue);
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

    }
}
