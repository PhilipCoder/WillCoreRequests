using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Builder;
using CodeBuilder.JS.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS
{
    public abstract class JSRenderble 
    {
        public string Name { get; set; }
        private string _processedTemplate { get; set; }

        protected string Template { get; set; }
        protected Dictionary<String, String> tagValues { get; set; }
        protected List<JSRenderble> childRenderbles { get; set; } = new List<JSRenderble>();

        public JSRenderble()
        {
        }
        protected JSRenderble(string name)
        {
            Template = Configuration.Instance.Templates[this.GetType().Name];
            Name = name;
        }

        public string GetText()
        {
            _processedTemplate = String.Copy(Template);
            var tagReplaceValues = tagValues.Keys.Select(x => $"<< {x} >>".KeyValueMap(tagValues[x]));
            var childRenderbleReplaceValues = childRenderbles.Select(a => $"<< {a.Name} >>".KeyValueMap(a.GetText()));
            _processedTemplate = _processedTemplate.ReplaceAll(tagReplaceValues);
            _processedTemplate = _processedTemplate.ReplaceAll(childRenderbleReplaceValues, false);
            return _processedTemplate;
        }

        public void CleanTemplateUp()
        {
            Template = TemplateCleaner.CleanTemplate(Template);
        }

        protected void multiplyTags(int count, string jsRenderbleName)
        {
            if (count > 0)
            {
                var parameterComments = Enumerable.Range(0, count).
                    Select(x => $"<< {jsRenderbleName} >>").
                    Aggregate((a, b) => $"{a}\r\n{b}");

                Template = Template.Replace($"<< {jsRenderbleName} >>", parameterComments);
            }
        }
    }
}
