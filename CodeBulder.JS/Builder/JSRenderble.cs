using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeBuilder.JS
{
    public abstract class JSRenderble : IRenderble
    {
        public string Template { get; set; }
        public JSRenderble()
        {
        }
        internal JSRenderble(string template)
        {
            Template = template;
        }

        public void CleanTemplateUp()
        {
            var tempateLines = new StringReader(Template);
            var stringBuilder = new StringBuilder();
            var line = tempateLines.ReadLine();
            var currentAppend = "";
            bool isIndenting = false;
            while (line != null)
            {
                if ((line.TrimStart().StartsWith("/**") || line.TrimStart().StartsWith("*") || line.TrimStart().StartsWith("//")) )
                {
                    if (!isIndenting)
                    {
                        currentAppend = line.Substring(0, line.IndexOf("/"));
                        isIndenting = true;
                    }
                    else
                    {
                        line = currentAppend + line.TrimStart();
                    }
                }
                else 
                {
                    isIndenting = false;
                }
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
                line = tempateLines.ReadLine();
            }
            Template = stringBuilder.ToString();
        }

        public virtual string GetText()
        {
            throw new NotImplementedException();
        }
    }
}
