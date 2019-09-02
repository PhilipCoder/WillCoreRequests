using CodeBuilder.JS.IBuilder;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS.Builder.Objects.ImportsExports
{
    public class RequestContextImport : JSRenderble, IRequestContextImport
    {
        private const string typesTag = "types";
        private const string urlTag = "url";

        public RequestContextImport() : base("RequestContextImport") { }

        public RequestContextImport(TypeStructure typeStructure) : base("RequestContextImport")
        {
            tagValues = new Dictionary<string, string> {
                { typesTag, Configuration.Instance.ModelsNameFactory(typeStructure.TypeName) },
                { urlTag, $"./{Configuration.Instance.ModelsFolder}/{Configuration.Instance.ModelsNameFactory(typeStructure.TypeName)}.js" }
            };
        }
    }
   
}
