using CodeBuilder.IBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public class ExportPOCO : JSRenderble, IExport
    {
        private const string typesTag = "types";

        public ExportPOCO(TypeStructure typeStructure) : base("ExportPOCO")
        {
            tagValues = new Dictionary<string, string> {
                { typesTag, Configuration.Instance.ModelsNameFactory(typeStructure.TypeName) }
            };
        }
    }
}
