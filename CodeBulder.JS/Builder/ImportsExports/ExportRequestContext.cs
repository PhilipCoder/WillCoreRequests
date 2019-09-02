using CodeBuilder.IBuilder;
using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder.Objects.ImportsExports
{
    public class ExportRequestContext : JSRenderble, IExport
    {
        private const string typesTag = "types";

        public ExportRequestContext(ClassStructure classStructure) : base("ExportRequestContext")
        {
            var exportClasses = TypeExtractor.GetTypes(classStructure).Select(x => Configuration.Instance.ModelsNameFactory(x.TypeName)).ToList();
            exportClasses.Add(classStructure.Name);
            tagValues = new Dictionary<string, string> {
                { typesTag, exportClasses.Aggregate((a,b)=>$"{a},{b}") }
            };
        }
    }
}
