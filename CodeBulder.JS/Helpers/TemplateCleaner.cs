using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public static class TemplateCleaner
    {
        public static string CleanTemplate(string template)
        {
            var tempateLines = new StringReader(template);
            var stringBuilder = new StringBuilder();
            var currentAppend = "";
            string line;
            while ((line = tempateLines.ReadLine()) != null)
            {
                removeUnusedTags(stringBuilder, fixIndenting(line, ref currentAppend));
            }
            return stringBuilder.ToString();
        }

        private static string fixIndenting(string line, ref string currentAppend)
        {
            if (String.Equals(line.TrimStart(), "{"))
            {
                currentAppend = line.Substring(0, line.IndexOf("{"))+"  ";
            }
            else if (String.Equals(line.TrimStart(), "}"))
            {
                currentAppend = line.Substring(0, line.IndexOf("}"));
            }
            else
            {
                line = currentAppend + line.TrimStart();
            }
            return line;
        }

        private static string removeUnusedTags(StringBuilder stringBuilder, string line)
        {
            var foundIndex = line.IndexOf($"<< ");
            if (foundIndex > -1)
            {
                var endIndex = line.IndexOf(">>", foundIndex) + 2;
                var isComment = line.Substring(foundIndex, endIndex - foundIndex).Contains("comment", StringComparison.OrdinalIgnoreCase);
                if (!isComment)
                {
                    line = line.Remove(foundIndex, endIndex - foundIndex);
                    stringBuilder.AppendLine(line);
                }
            }
            else
            {
                stringBuilder.AppendLine(line);
            }

            return line;
        }
    }
}
