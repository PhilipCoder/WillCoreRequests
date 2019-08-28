using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public class Export : JSRenderble, IExport
    {
        private const string ExportTypeNode = "<< exports[types] >>";

        public IEnumerable<String> Modules { get; set; }
        public Export() : base(Resources.exports)
        {
            Modules = new List<string>();
        }

        public override String GetText()
        {
            return Template.Replace(ExportTypeNode, Modules.Aggregate((a, b) => a + ", " + b));
        }
    }
}
