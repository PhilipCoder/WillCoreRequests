using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Helpers
{
    public static class LinqExtensions
    {
        public static string GetCSV<T>(this List<T> values, Func<T, String> getValue ,string defaultValue = "", string seperator = ",")
        {
            if (values.Any())
            {
                return values.Select(x => getValue(x)).Aggregate((a, b) => $"{a}{seperator}{b}");
            }
            return defaultValue;
        }

        public static IEnumerable<T> ObjectDistinct<T>(this IEnumerable<T> values, Func<T, string> equalityValue)
        {
            var distinctValues = new Dictionary<string,T>();
            foreach (var value in values)
            {
                var itemKey = equalityValue(value);
                if (!distinctValues.ContainsKey(itemKey))
                {
                    distinctValues.Add(itemKey, value);
                }
            }
            return distinctValues.Values;
        }
    }
}
