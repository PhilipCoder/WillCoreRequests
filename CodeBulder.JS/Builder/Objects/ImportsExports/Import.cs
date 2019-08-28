using CodeBuilder.IBuilder;
using CodeBuilder.JS.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBuilder.JS.Builder
{
    public class Import : JSRenderble, IImport
    {
        private const string ImportTypeNode = "<< import[types] >>";
        private const string ImportURLNode = "<< import[url] >>";

        public IEnumerable<String> Modules { get; set; } = new List<String>();
        public String URL { get; set; }
        public Import() : base(Resources.import)
        {
        }

        public override String GetText()
        {
            return Template.Replace(ImportTypeNode, Modules.Aggregate((a, b) => a + ", " + b)).Replace(ImportURLNode, URL);
        }
    }
}
