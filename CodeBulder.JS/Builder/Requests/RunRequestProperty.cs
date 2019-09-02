using CodeBuilder.JS.Helpers;
using CodeBuilder.JS.IBuilder.Requests;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBuilder.JS.Builder.Requests
{
    public class RunRequestProperty : JSRenderble, IRunRequestProperty
    {
        private const string nameTag = "name";
        private const string urlTag = "url";
        private const string methodTag = "method";
        private const string parameterSourceBindingTag = "parameterSourceBindings";
        private const string resultTypeTag = "resultType";

        public RunRequestProperty() : base("RunRequestProperty") { }
        public RunRequestProperty(MethodStructure methodStructure) : base("RunRequestProperty")
        {
            tagValues = new Dictionary<string, string> {
                { nameTag, methodStructure.IsRPC ? methodStructure.Name : NamingHelpers.GetRestfullMethodName(methodStructure) },
                { urlTag, methodStructure.URL },
                { methodTag, HttpHelpers.GetHTTPMethod(methodStructure) },
                { parameterSourceBindingTag, HttpHelpers.GetRequestParametersSourceObject(methodStructure) } ,
                { resultTypeTag,  !methodStructure.Result.IsSytemType && methodStructure.Result.TypeName != null ? Configuration.Instance.ModelsNameFactory(methodStructure.Result.TypeName) :  "null" }
            };
        }
    }
}
