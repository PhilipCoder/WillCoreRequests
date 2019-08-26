using CodeBulder.IBuilder;
using CodeBulder.JS;
using CodeBulder.JS.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBulder.JS.Builder
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
