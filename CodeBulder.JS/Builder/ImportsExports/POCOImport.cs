using CodeBuilder.IBuilder;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBuilder.JS.Builder
{
    public class POCOImport : JSRenderble, IImport
    {
        private const string typesTag = "types";
        private const string urlTag = "url";

        public POCOImport(TypeStructure typeStructure) : base("POCOImport")
        {
            tagValues = new Dictionary<string, string> {
                { typesTag, Configuration.Instance.ModelsNameFactory(typeStructure.TypeName) },
                { urlTag, $"./{Configuration.Instance.ModelsNameFactory(typeStructure.TypeName)}.js" }
            };

        }
    }
}
